using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ArrowResource : MeshMarkerResource
    {
        const float MaxArrowWidth = 5.0f;

        static readonly Quaternion PointToX = Quaternion.AngleAxis(90, Vector3.up);

        protected override void Awake()
        {
            base.Awake();
            Reset();
        }

        public void Set(in Vector3 a, in Vector3 b, float? overrideScaleYZ = null)
        {
            var diff = a - b; // arrow model is flipped
            float scaleX = diff.Magnitude();
            float scaleYZ = overrideScaleYZ ?? Mathf.Min(scaleX * 0.15f, MaxArrowWidth);

            Transform.localScale = new Vector3(scaleX, scaleYZ, scaleYZ);
            Transform.localPosition = a;

            if (Mathf.Approximately(scaleX, 0))
            {
                return;
            }

            var x = diff / scaleX;

            var notX = x.WithZ(0).MagnitudeSq() < 1e-6
                ? Vector3.up
                : Vector3.forward;

            var y = x.Cross(notX).Normalized();
            var z = x.Cross(y).Normalized();

            var m = Matrix4x4.identity;
            m.SetColumn(0, x);
            m.SetColumn(1, y);
            m.SetColumn(2, z);

            Transform.localRotation = m.rotation;
        }

        public void Set(in Vector3 scale)
        {
            Transform.localScale = new Vector3(scale.z, scale.y, scale.x);
            Transform.SetLocalPose(Pose.identity.WithRotation(PointToX));
        }

        public void Reset()
        {
            Transform.localScale = Vector3.zero;
            Transform.SetLocalPose(Pose.identity);
        }

        public override void Suspend()
        {
            base.Suspend();
            Reset();
        }
    }
}