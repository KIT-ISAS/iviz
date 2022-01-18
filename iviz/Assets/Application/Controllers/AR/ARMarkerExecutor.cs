#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.App;
using Iviz.App.ARDialogs;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.MarkerDetection;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class ARMarkerExecutor
    {
        readonly Dictionary<(ARMarkerType, string), ARExecutableMarker> markers = new();

        readonly Dictionary<(ARMarkerType, string), ARTfFrame> frames = new();

        ARExecutableMarker? lastSeen;
        float maxMarkerDistanceInM = 0.5f;

        public string Description
        {
            get
            {
                const int maxLastSeenLength = 10;
                string lastSeenStr;
                if (lastSeen == null)
                {
                    lastSeenStr = "Last seen: None";
                }
                else
                {
                    string typeStr;
                    switch (lastSeen.Type)
                    {
                        case ARMarkerType.Aruco:
                            typeStr = "Aruco";
                            break;
                        case ARMarkerType.QrCode:
                            typeStr = "QR";
                            break;
                        default:
                            typeStr = "(Unknown)";
                            break;
                    }

                    lastSeenStr = lastSeen.Code.Length < maxLastSeenLength
                        ? $"Last seen: {typeStr} \"{lastSeen.Code}\""
                        : $"Last seen: {typeStr} \"{lastSeen.Code[..maxLastSeenLength]}...\"";
                }

                return "<b>AR Markers</b>\n" +
                       //markerCount + "\n" +
                       lastSeenStr;
            }
        }

        public ARMarkersConfiguration Configuration
        {
            get => new()
            {
                MaxMarkerDistanceInM = maxMarkerDistanceInM, Markers = markers.Values.ToArray()
            };
            set
            {
                maxMarkerDistanceInM = value.MaxMarkerDistanceInM;
                markers.Clear();
                foreach (var marker in value.Markers)
                {
                    markers.Add((marker.Type, marker.Code), marker);
                }
            }
        }

        public void Process(ARMarker marker)
        {
            var key = ((ARMarkerType)marker.Type, marker.Code);
            if (!markers.TryGetValue(key, out var executableMarker))
            {
                ProcessUnknownSize(marker);
                return;
            }

            marker.MarkerSizeInMm = executableMarker.SizeInMm;
            if (marker.MarkerSizeInMm == 0 && !TryDetectMarkerSize(marker))
            {
                Debug.Log($"{this}: Marker {key.Code} has size 0 and its size could not be estimated. Ignoring.");
                return;
            }
            //Debug.Log("Detected " +  key.Code + " with size " + detectedMarker.MarkerSize);

            Msgs.GeometryMsgs.Pose rosMarkerPose;
            try
            {
                rosMarkerPose = SolvePnp(marker);
            }
            catch (CvMarkerException)
            {
                RosLogger.Info($"{this}: OpenCV error while processing image");
                return;
            }

            marker.HasReliablePose = true;
            marker.PoseRelativeToCamera = rosMarkerPose;

            Msgs.GeometryMsgs.Vector3 rosCameraPosition = marker.CameraPose.Position;
            double distanceMarkerToCamera = (rosCameraPosition - rosMarkerPose.Position).Norm;
            if (distanceMarkerToCamera > maxMarkerDistanceInM)
            {
                RosLogger.Debug($"{this}: Detected marker at distance " +
                                $"{distanceMarkerToCamera.ToString(BuiltIns.Culture)}, " +
                                "discarding.");
                return;
            }

            lastSeen = executableMarker;

            var unityPoseAbsolute = ARController.GetAbsoluteMarkerPose(marker);

            switch (executableMarker.Action)
            {
                case ARMarkerAction.Origin:
                    if (ARController.Instance is not { Visible: true })
                    {
                        break;
                    }

                    bool isUp = Vector3.Dot(unityPoseAbsolute.up, Vector3.up) > 1 - 0.05f;
                    if (isUp)
                    {
                        ARController.Instance.SetWorldPose(unityPoseAbsolute, ARController.RootMover.Executor);
                    }

                    break;
                case ARMarkerAction.PublishTf:
                    var frameDisplay = frames.TryGetValue(key, out ARTfFrame existingDisplay)
                        ? existingDisplay
                        : RentFrame();

                    var unityPoseToFixed = TfListener.RelativeToFixedFrame(unityPoseAbsolute);

                    frameDisplay.ParentFrame = TfListener.FixedFrameId;
                    frameDisplay.TargetPose = unityPoseToFixed;

                    var tfFrame = TfPublisher.Instance.GetOrCreate(marker.Code);
                    tfFrame.AttachToFixed();
                    tfFrame.LocalPose = unityPoseToFixed;
                    break;
            }

            ARTfFrame RentFrame()
            {
                var tfFrame = ResourcePool.RentDisplay<ARTfFrame>();
                tfFrame.Caption = marker.Code;
                frames[key] = tfFrame;
                return tfFrame;
            }
        }

        void ProcessUnknownSize(ARMarker marker)
        {
            if (!TryDetectMarkerSize(marker))
            {
                return;
            }

            try
            {
                marker.PoseRelativeToCamera = SolvePnp(marker);
            }
            catch (CvMarkerException)
            {
                RosLogger.Info($"{this}: OpenCV error while processing image");
            }
        }


        static bool TryDetectMarkerSize(ARMarker marker)
        {
            const float distanceMultiplier = 0.15f;

            var relativeCameraPose = marker.CameraPose.FromCameraFrame().Ros2Unity();
            var (cameraPosition, cameraRotation) = TfListener.FixedFrameToAbsolute(relativeCameraPose);
            var intrinsic = new Intrinsic(marker.CameraIntrinsic);

            Span<Vector3> dirs = stackalloc Vector3[4];
            foreach (int i in ..4)
            {
                var (cornerX, cornerY, _) = marker.Corners[i];
                var (dirX, dirY, dirZ) = intrinsic.Unproject(cornerX, cornerY);
                dirs[i] = new Vector3(dirX, -dirY, dirZ); // ros y-down to unity y-up
            }

            var centerDir = (dirs[0] + dirs[1] + dirs[2] + dirs[3]) / 4;
            var hitInfo = new ClickHitInfo(new Ray(cameraPosition, cameraRotation * centerDir.normalized));
            if (!hitInfo.TryGetARRaycastResults(out var arHits))
            {
                return false;
            }

            Span<Ray> rays = stackalloc Ray[4];
            foreach (int i in ..4)
            {
                rays[i] = new Ray(cameraPosition, cameraRotation * dirs[i].normalized);
            }

            Span<Vector3> hitPoints = stackalloc Vector3[4];
            Span<float> sizes = stackalloc float[4];
            foreach (var (hitPosition, hitNormal) in arHits)
            {
                var planeAsRay = new Ray(hitPosition, hitNormal);

                foreach (int i in ..4)
                {
                    UnityUtils.PlaneIntersection(planeAsRay, rays[i], out hitPoints[i], out _);
                }

                foreach (int i in ..4)
                {
                    sizes[i] = Vector3.Distance(hitPoints[(i + 1) % 4], hitPoints[i]);
                }

                float averageSize = (sizes[0] + sizes[1] + sizes[2] + sizes[3]) / 4;
                float distanceThreshold = averageSize * distanceMultiplier;
                if (Math.Abs(sizes[0] - averageSize) > distanceThreshold ||
                    Math.Abs(sizes[1] - averageSize) > distanceThreshold ||
                    Math.Abs(sizes[2] - averageSize) > distanceThreshold ||
                    Math.Abs(sizes[3] - averageSize) > distanceThreshold)
                {
                    continue;
                }

                marker.MarkerSizeInMm = averageSize * 1000;
                return true;
            }

            return false;
        }


        public void Dispose()
        {
            foreach (var frame in frames.Values)
            {
                frame.ReturnToPool();
            }

            frames.Clear();
        }

        static Msgs.GeometryMsgs.Pose SolvePnp(ARMarker marker)
        {
            float sizeInMm = (float)marker.MarkerSizeInMm;
            float sizeInM = sizeInMm / 1000f;
            var intrinsic = new Intrinsic(marker.CameraIntrinsic);
            var corners = marker.Corners;
            ReadOnlySpan<Vector2f> imageCorners = stackalloc[]
            {
                ToVector2f(corners[0]),
                ToVector2f(corners[1]),
                ToVector2f(corners[2]),
                ToVector2f(corners[3])
            };

            return MarkerDetector.SolvePnp(imageCorners, intrinsic, sizeInM);

            static Vector2f ToVector2f(in Msgs.GeometryMsgs.Vector3 v) => new((float)v.X, (float)v.Y);
        }

        public override string ToString() => "[ARMarkerExecutor]";
    }
}