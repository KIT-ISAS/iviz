#nullable enable

using Iviz.Core;
using Iviz.Displays.Helpers;
using Iviz.Msgs.SensorMsgs;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class ContainerBoundary
    {
        readonly SimpleBoundary boundary;
        PointCloudProcessor? pointCloud;

        public Transform Transform { get; }
        
        public Vector3 Scale
        {
            set
            {
                boundary.Scale = value;
                boundary.Visible = !value.ApproximatelyZero();
            }
        }

        public Pose Pose
        {
            set => boundary.Transform.SetLocalPose(value);
        }

        public ContainerBoundary()
        {
            Transform = new GameObject("Detection Node").transform;
            boundary = ResourcePool.RentDisplay<SimpleBoundary>(Transform);
        }

        public void HandlePointCloud(PointCloud2 msg)
        {
            pointCloud ??= new PointCloudProcessor(Transform);

            if (msg.Header.FrameId.Length == 0)
            {
                msg.Header = default; // sets header.FrameId to null
            }

            pointCloud.Handle(msg);
        }

        public void Dispose()
        {
            pointCloud?.Dispose();
        }
    }
}