#nullable enable

using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays.Helpers;
using Iviz.Msgs.SensorMsgs;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class ContainerBoundary : MonoBehaviour, IDisplay, IRecyclable
    {
        SimpleBoundary? boundary;
        PointCloudProcessor? pointCloud;
        Transform? mTransform;

        Transform Transform => this.EnsureHasTransform(ref mTransform);
        SimpleBoundary Boundary => ResourcePool.RentChecked(ref boundary, Transform);

        public Vector3 Scale
        {
            set
            {
                Boundary.Scale = value;
                Boundary.Visible = !value.ApproximatelyZero();
            }
        }

        public Pose Pose
        {
            set => Boundary.Transform.SetLocalPose(value);
        }


        void HandlePointCloud(PointCloud2 msg)
        {
            pointCloud ??= new PointCloudProcessor();
            pointCloud.Handle(msg);
        }

        public void Suspend()
        {
        }
        
        public void SplitForRecycle()
        {
            pointCloud?.Dispose();
        }
    }
}