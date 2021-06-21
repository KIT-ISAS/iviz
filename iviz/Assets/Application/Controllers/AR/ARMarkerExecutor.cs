using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.App.ARDialogs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.MarkerDetection;
using Iviz.Msgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public class ARMarkerExecutor
    {
        public const string Prefix = "{\"IvizE\":";

        readonly Dictionary<string, ARTfFrame> frames = new Dictionary<string, ARTfFrame>();

        public enum ExecutorMarkerCommand
        {
            PublishTfFrame = 0,
            SetOrigin = 1
        }

        [DataContract]
        sealed class ExecutorBaseMarker : JsonToString
        {
            [DataMember] public ExecutorMarkerCommand IvizE { get; set; }
        }

        [DataContract]
        sealed class ExecutorTfMarker : JsonToString
        {
            [DataMember] public ExecutorMarkerCommand IvizE => ExecutorMarkerCommand.PublishTfFrame;
            [DataMember] public float Size { get; set; }
            [DataMember] public string Tf { get; set; }
        }

        [DataContract]
        sealed class ExecutorOriginMarker : JsonToString
        {
            [DataMember] public ExecutorMarkerCommand IvizE => ExecutorMarkerCommand.SetOrigin;
            [DataMember] public float Size { get; set; }
        }

        public void Execute([NotNull] DetectedARMarker marker)
        {
            ExecutorBaseMarker baseMarker;

            try
            {
                baseMarker = JsonConvert.DeserializeObject<ExecutorBaseMarker>(marker.QrCode);
            }
            catch (JsonException)
            {
                Logger.Info($"{this}: Failed to deserialize code '{marker.QrCode}'");
                return;
            }

            switch (baseMarker.IvizE)
            {
                case ExecutorMarkerCommand.PublishTfFrame:
                    ExecuteTfMarker(marker);
                    break;
                case ExecutorMarkerCommand.SetOrigin:
                    ExecuteOriginMarker(marker);
                    break;
                default:
                    Logger.Info($"{this}: Detected unknown code {baseMarker.IvizE}");
                    break;
            }
        }

        void ExecuteTfMarker([NotNull] DetectedARMarker marker)
        {
            ExecutorTfMarker tfExecutor;
            try
            {
                tfExecutor = JsonConvert.DeserializeObject<ExecutorTfMarker>(marker.QrCode);
                Logger.Debug($"{this}: Obtained tf marker {tfExecutor}");
            }
            catch (JsonException)
            {
                Logger.Info($"{this}: Failed to deserialize code '{marker.QrCode}'");
                return;
            }

            if (string.IsNullOrEmpty(tfExecutor.Tf) || tfExecutor.Size <= 0)
            {
                Logger.Info($"{this}: Invalid data Tf='{tfExecutor.Tf}' Size=" +
                            tfExecutor.Size.ToString(BuiltIns.Culture));
                return;
            }

            Msgs.GeometryMsgs.Transform rosMarkerPose;
            try
            {
                rosMarkerPose = SolvePnp(marker, tfExecutor.Size * 0.001f);
            }
            catch (CvMarkerException)
            {
                Logger.Info($"{this}: OpenCV error while processing image");
                return;
            }

            const double maxMarkerDistance = 0.5;

            Msgs.GeometryMsgs.Vector3 rosCameraPosition = marker.CameraPose.Position;
            double distanceMarkerToCamera = (rosCameraPosition - rosMarkerPose.Translation).Norm;
            if (distanceMarkerToCamera > maxMarkerDistance)
            {
                Logger.Debug($"{this}: Detected tf marker at distance " +
                             $"{distanceMarkerToCamera.ToString(BuiltIns.Culture)}, " +
                             "discarding.");
                return;
            }

            if (!frames.TryGetValue(tfExecutor.Tf, out ARTfFrame tfFrame))
            {
                tfFrame = ResourcePool.RentDisplay<ARTfFrame>();
                tfFrame.Caption = tfExecutor.Tf;
                frames[tfExecutor.Tf] = tfFrame;
                Logger.Info("Adding " + tfExecutor.Tf);
            }


            Pose unityPose = TfListener.FixedFramePose.Multiply(rosMarkerPose.Ros2Unity());
            tfFrame.TargetPose = unityPose;

            TfListener.Publish(TfListener.FixedFrameId, tfExecutor.Tf, rosMarkerPose);
        }

        void ExecuteOriginMarker(DetectedARMarker marker)
        {
            if (ARController.Instance == null)
            {
                return;
            }

            ExecutorOriginMarker originExecutor;
            try
            {
                originExecutor = JsonConvert.DeserializeObject<ExecutorOriginMarker>(marker.QrCode);
                Logger.Debug($"{this}: Obtained origin marker {originExecutor}");
            }
            catch (JsonException)
            {
                Logger.Info($"{this}: Failed to deserialize code '{marker.QrCode}'");
                return;
            }

            Msgs.GeometryMsgs.Transform rosMarkerPose;
            try
            {
                rosMarkerPose = SolvePnp(marker, originExecutor.Size * 0.001f);
            }
            catch (CvMarkerException)
            {
                Logger.Info($"{this}: OpenCV error while processing image");
                return;
            }

            const double maxMarkerDistance = 0.5;

            Msgs.GeometryMsgs.Vector3 rosCameraPosition = marker.CameraPose.Position;
            double distanceMarkerToCamera = (rosCameraPosition - rosMarkerPose.Translation).Norm;
            if (distanceMarkerToCamera > maxMarkerDistance)
            {
                Logger.Debug($"{this}: Detected origin marker at distance " +
                             $"{distanceMarkerToCamera.ToString(BuiltIns.Culture)}, " +
                             "discarding.");
                return;
            }

            if (!frames.TryGetValue(TfListener.OriginFrameId, out ARTfFrame originFrame))
            {
                originFrame = ResourcePool.RentDisplay<ARTfFrame>();
                originFrame.Caption = "[origin]";
                frames[TfListener.OriginFrameId] = originFrame;
            }

            Pose unityPose = TfListener.FixedFramePose.Multiply(rosMarkerPose.Ros2Unity());
            originFrame.TargetPose = unityPose;

            Vector3 up = unityPose.up;
            if (Vector3.Dot(up, Vector3.up) > 1 - 0.05f)
            {
                ARController.Instance.SetWorldPose(unityPose, ARController.RootMover.Executor);
            }
        }

        static Msgs.GeometryMsgs.Transform SolvePnp([NotNull] DetectedARMarker marker, float size)
        {
            var imageCorners = marker.Corners;
            var objectCorners = new Vector3f[]
            {
                (-size / 2, size / 2, 0),
                (size / 2, size / 2, 0),
                (size / 2, -size / 2, 0),
                (-size / 2, -size / 2, 0),
            };

            var localPoseInRos = CvContext.SolvePnp(imageCorners, objectCorners, 
                (float) marker.Intrinsic.Fx, (float) marker.Intrinsic.Cx, 
                (float) marker.Intrinsic.Fy, (float) marker.Intrinsic.Cy,
                SolvePnPMethod.IPPESquare);

            var absolutePoseInRos = (Msgs.GeometryMsgs.Transform) marker.CameraPose * localPoseInRos;
            return absolutePoseInRos;
        }

        [NotNull] public override string ToString() => "[ARMarkerExecutor]";
    }
}