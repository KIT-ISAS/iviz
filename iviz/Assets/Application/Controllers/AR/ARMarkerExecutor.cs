using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.App.ARDialogs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.MarkerDetection;
using Iviz.Msgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Msgs.IvizMsgs;
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
        class ExecutorBaseMarker
        {
            [DataMember] public ExecutorMarkerType IvizT { get; set; }
        }

        [DataContract]
        class ExecutorTfMarker
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

                    if (!frames.TryGetValue(tfExecutor.Tf, out ARTfFrame tfFrame))
                    {
                        tfFrame = ResourcePool.RentDisplay<ARTfFrame>();
                        tfFrame.Caption = tfExecutor.Tf;
                        frames[tfExecutor.Tf] = tfFrame;
                    }

                    Msgs.GeometryMsgs.Transform rosPoseToFixedFrame;
                    try
                    {
                        rosPoseToFixedFrame = SolvePnp(marker, tfExecutor.Size * 0.001f);
                    }
                    catch (CvMarkerException)
                    {
                        Logger.Info($"{this}: OpenCV error while processing image");
                        break;
                    }

                    Pose absoluteUnityPose = TfListener.FixedFramePose.Multiply(rosPoseToFixedFrame.Ros2Unity());
                    tfFrame.transform.SetPose(absoluteUnityPose);
                    
                    TfListener.Publish(TfListener.FixedFrameId, tfExecutor.Tf, rosPoseToFixedFrame);
                    
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