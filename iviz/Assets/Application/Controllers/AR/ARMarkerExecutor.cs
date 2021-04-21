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
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public class ARMarkerExecutor
    {
        public const string Prefix = "{\"IvizT\":";

        readonly Dictionary<string, ARTfFrame> frames = new Dictionary<string, ARTfFrame>();

        public enum ExecutorMarkerType
        {
            TfPublisher,
        }

        [DataContract]
        sealed class ExecutorBaseMarker : JsonToString
        {
            [DataMember] public ExecutorMarkerType IvizT { get; set; }
        }

        [DataContract]
        sealed class ExecutorTfMarker : JsonToString
        {
            [DataMember] public ExecutorMarkerType IvizT => ExecutorMarkerType.TfPublisher;
            [DataMember] public float Size { get; set; }
            [DataMember] public string Tf { get; set; }
        }

        public void Execute(DetectedARMarker marker)
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

            switch (baseMarker.IvizT)
            {
                case ExecutorMarkerType.TfPublisher:
                    ExecutorTfMarker tfExecutor;
                    try
                    {
                        tfExecutor = JsonConvert.DeserializeObject<ExecutorTfMarker>(marker.QrCode);
                        Logger.Debug($"{this}: Obtained executable marker {tfExecutor}");
                    }
                    catch (JsonException)
                    {
                        Logger.Info($"{this}: Failed to deserialize code '{marker.QrCode}'");
                        break;
                    }

                    if (string.IsNullOrEmpty(tfExecutor.Tf) || tfExecutor.Size <= 0)
                    {
                        Logger.Info($"{this}: Invalid data Tf='{tfExecutor.Tf}' Size=" +
                                    tfExecutor.Size.ToString(BuiltIns.Culture));
                        break;
                    }

                    Msgs.GeometryMsgs.Transform rosMarkerPose;
                    try
                    {
                        rosMarkerPose = SolvePnp(marker, tfExecutor.Size * 0.001f);
                    }
                    catch (CvMarkerException)
                    {
                        Logger.Info($"{this}: OpenCV error while processing image");
                        break;
                    }

                    const double maxMarkerDistance = 0.5;

                    Msgs.GeometryMsgs.Vector3 cameraPosition = marker.CameraPose.Position;
                    double distanceMarkerToCamera = (cameraPosition - rosMarkerPose.Translation).Norm;
                    if (distanceMarkerToCamera > maxMarkerDistance)
                    {
                        Logger.Debug($"{this}: Detected marker at distance " +
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


                    Pose absoluteUnityPose = TfListener.FixedFramePose.Multiply(rosMarkerPose.Ros2Unity());
                    tfFrame.transform.SetPose(absoluteUnityPose);
                    tfFrame.CheckOrientationBillboard();

                    TfListener.Publish(TfListener.FixedFrameId, tfExecutor.Tf, rosMarkerPose);

                    break;
                default:
                    Logger.Info($"{this}: Detected unknown code {baseMarker.IvizT}");
                    break;
            }
        }

        static Msgs.GeometryMsgs.Transform SolvePnp(DetectedARMarker marker, float size)
        {
            var imageCorners = marker.Corners;
            var objectCorners = new Vector3f[]
            {
                (-size / 2, size / 2, 0),
                (size / 2, size / 2, 0),
                (size / 2, -size / 2, 0),
                (-size / 2, -size / 2, 0),
            };

            var localPoseInRos = CvContext.SolvePnp(imageCorners, objectCorners, marker.Intrinsic.Fx,
                marker.Intrinsic.Cx, marker.Intrinsic.Fy, marker.Intrinsic.Cy);

            var absolutePoseInRos = (Msgs.GeometryMsgs.Transform) marker.CameraPose * localPoseInRos;
            return absolutePoseInRos;
        }

        public override string ToString() => "[ARMarkerExecutor]";
    }
}