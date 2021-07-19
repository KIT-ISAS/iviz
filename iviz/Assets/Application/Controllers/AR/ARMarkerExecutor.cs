using System.Collections.Generic;
using System.Linq;
using Iviz.App;
using Iviz.App.ARDialogs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.MarkerDetection;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public class ARMarkerExecutor
    {
        readonly Dictionary<(ARMarkerType, string), ARExecutableMarker> markers =
            new Dictionary<(ARMarkerType, string), ARExecutableMarker>();

        readonly Dictionary<(ARMarkerType, string), ARTfFrame> frames =
            new Dictionary<(ARMarkerType, string), ARTfFrame>();

        [CanBeNull] ARExecutableMarker lastSeen;
        float maxMarkerDistanceInM = 0.5f;

        [NotNull]
        public string Description
        {
            get
            {
                string markerCount;
                switch (markers.Count)
                {
                    case 0:
                        markerCount = "No markers registered";
                        break;
                    case 1:
                        markerCount = "1 marker registered";
                        break;
                    default:
                        markerCount = markers.Count.ToString() + " markers registered";
                        break;
                }

                const int maxLastSeenLength = 8;
                string lastSeenStr;
                if (lastSeen == null)
                {
                    lastSeenStr = "Last seen: None";
                }
                else if (lastSeen.Code.Length < maxLastSeenLength)
                {
                    lastSeenStr = $"Last seen: {lastSeen.Type} [{lastSeen.Code}]";
                }
                else
                {
                    lastSeenStr = $"Last seen: {lastSeen.Type} [{lastSeen.Code.Substring(0, maxLastSeenLength)}...]";
                }

                return "<b>AR Markers</b>\n" +
                       markerCount + "\n" +
                       lastSeenStr;
            }
        }

        [NotNull]
        public ARMarkersConfiguration Configuration
        {
            get => new ARMarkersConfiguration
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

        public void Process([NotNull] DetectedARMarker detectedMarker)
        {
            var key = ((ARMarkerType) detectedMarker.Type, detectedMarker.Code);
            if (!markers.TryGetValue(key, out var executableMarker))
            {
                return;
            }

            detectedMarker.MarkerSize = executableMarker.SizeInMm;

            Msgs.GeometryMsgs.Pose rosMarkerPose;
            try
            {
                rosMarkerPose = SolvePnp(detectedMarker);
            }
            catch (CvMarkerException)
            {
                Logger.Info($"{this}: OpenCV error while processing image");
                return;
            }

            detectedMarker.HasExtrinsicPose = true;
            detectedMarker.PoseRelativeToCamera = rosMarkerPose;

            Msgs.GeometryMsgs.Vector3 rosCameraPosition = detectedMarker.CameraPose.Position;
            double distanceMarkerToCamera = (rosCameraPosition - rosMarkerPose.Position).Norm;
            if (distanceMarkerToCamera > maxMarkerDistanceInM)
            {
                Logger.Debug($"{this}: Detected origin marker at distance " +
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
                    if (ARController.Instance == null)
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
                        Logger.Info("Adding " + detectedMarker.Code);
                    }


                    tfFrame.TargetPose = unityPose;
                    TfListener.Publish(TfListener.FixedFrameId, $"~{detectedMarker.Code}", rosMarkerPose);
                    break;
            }
        }

        public void Reset()
        {
            foreach (var frame in frames.Values)
            {
                frame.ReturnToPool();
            }

            frames.Clear();
        }

        static Msgs.GeometryMsgs.Pose SolvePnp([NotNull] DetectedARMarker marker)
        {
            float size = (float) marker.MarkerSize;
            var intrinsic = new Intrinsic(marker.CameraIntrinsic);
            var imageCorners = marker.Corners
                .Select(corner => new Vector2f((float) corner.X, (float) corner.Y))
                .ToArray();
            return MarkerDetector.SolvePnp(imageCorners, intrinsic, size);
        }

        [NotNull]
        public override string ToString() => "[ARMarkerExecutor]";
    }
}