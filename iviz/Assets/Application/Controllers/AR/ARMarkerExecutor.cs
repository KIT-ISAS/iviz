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

        public void Process(ARMarker detectedMarker)
        {
            var key = ((ARMarkerType) detectedMarker.Type, detectedMarker.Code);
            if (!markers.TryGetValue(key, out var executableMarker))
            {
                return;
            }

            detectedMarker.MarkerSizeInMm = executableMarker.SizeInMm;
            //Debug.Log("Detected " +  key.Code + " with size " + detectedMarker.MarkerSize);

            Msgs.GeometryMsgs.Pose rosMarkerPose;
            try
            {
                rosMarkerPose = SolvePnp(detectedMarker);
            }
            catch (CvMarkerException)
            {
                RosLogger.Info($"{this}: OpenCV error while processing image");
                return;
            }

            detectedMarker.HasExtrinsicPose = true;
            detectedMarker.PoseRelativeToCamera = rosMarkerPose;

            Msgs.GeometryMsgs.Vector3 rosCameraPosition = detectedMarker.CameraPose.Position;
            double distanceMarkerToCamera = (rosCameraPosition - rosMarkerPose.Position).Norm;
            if (distanceMarkerToCamera > maxMarkerDistanceInM)
            {
                RosLogger.Debug($"{this}: Detected origin marker at distance " +
                             $"{distanceMarkerToCamera.ToString(BuiltIns.Culture)}, " +
                             "discarding.");
                return;
            }

            lastSeen = executableMarker;
            Msgs.GeometryMsgs.Pose rosAbsolutePose =
                (Msgs.GeometryMsgs.Transform) detectedMarker.CameraPose * rosMarkerPose;
            Pose unityPose = TfListener.FixedFramePose.Multiply(rosAbsolutePose.Ros2Unity());

            switch (executableMarker.Action)
            {
                case ARMarkerAction.Origin:
                    if (ARController.Instance == null || !ARController.IsVisible)
                    {
                        break;
                    }

                    bool isUp = Vector3.Dot(unityPose.up, Vector3.up) > 1 - 0.05f;
                    if (isUp)
                    {
                        ARController.Instance.SetWorldPose(unityPose, ARController.RootMover.Executor);
                    }

                    break;
                case ARMarkerAction.Publish:
                    if (!frames.TryGetValue(key, out ARTfFrame tfFrame))
                    {
                        tfFrame = ResourcePool.RentDisplay<ARTfFrame>();
                        tfFrame.Caption = detectedMarker.Code;
                        frames[key] = tfFrame;
                        //Logger.Info("Adding " + detectedMarker.Code);
                    }

                    tfFrame.ParentFrame = TfListener.FixedFrameId;
                    tfFrame.TargetPose = rosAbsolutePose.Ros2Unity();
                    TfListener.Publish(TfListener.FixedFrameId, $"~{detectedMarker.Code}", rosAbsolutePose);
                    break;
            }
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
            float sizeInMm = (float) marker.MarkerSizeInMm;
            float sizeInM = sizeInMm / 1000f;
            var intrinsic = new Intrinsic(marker.CameraIntrinsic);
            var corners = marker.Corners;
            ReadOnlySpan<Vector2f> imageCorners = stackalloc Vector2f[]
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