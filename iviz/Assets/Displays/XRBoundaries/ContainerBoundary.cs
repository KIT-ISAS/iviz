#nullable enable

using Iviz.Core;
using Iviz.Displays.Helpers;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.StdMsgs;
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

        public string Caption
        {
            set => boundary.Caption = value;
        }
        
        public Color Color
        {
            set
            {
                boundary.FrameColor = value;
                boundary.InteriorColor = value.WithAlpha(0.75f);
            } 
        }

        public bool Visible
        {
            set => Transform.gameObject.SetActive(value);
        }

        public ContainerBoundary()
        {
            Transform = new GameObject("Detection Node").transform;
            boundary = ResourcePool.RentDisplay<SimpleBoundary>(Transform);
            boundary.UseFresnelLighting = true;
            boundary.FrameWidth = 0.01f;
            boundary.EnableShadows = false;
        }

        public void HandlePointCloud(PointCloud2 msg)
        {
            if (msg.Data.Length == 0) // empty cloud?
            {
                pointCloud?.Reset();
                return; 
            }
            
            pointCloud ??= new PointCloudProcessor(Transform);

            if (msg.Header.FrameId.Length == 0)
            {
                msg.Header = default; // hack! sets header.FrameId to null
            }

            pointCloud.Handle(msg);
        }

        public void Dispose()
        {
            pointCloud?.Dispose();
        }
    }
}