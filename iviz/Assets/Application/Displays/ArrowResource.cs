using Iviz.Core;
using UnityEditor.UI;
using UnityEditorInternal;
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
            Vector3 diff = a - b; // arrow model is flipped
            float scaleX = diff.Magnitude();
            float scaleYZ = overrideScaleYZ ?? Mathf.Min(scaleX, MaxArrowWidth);

            Transform mTransform = transform;
            mTransform.localScale = new Vector3(scaleX, scaleYZ, scaleYZ);
            mTransform.localPosition = a;

            if (Mathf.Approximately(scaleX, 0))
            {
                return;
            }
            
            Vector3 x = diff / scaleX;

            Vector3 notX = Vector3.forward;
            if (Mathf.Approximately(x.Cross(notX).MagnitudeSq(), 0))
            {
                notX = new Vector3(0, 1, 0);
            }

            Vector3 y = x.Cross(notX).Normalized();
            Vector3 z = x.Cross(y).Normalized();

            Matrix4x4 m = Matrix4x4.identity;
            m.SetColumn(0, x);
            m.SetColumn(1, y);
            m.SetColumn(2, z);

            mTransform.localRotation = m.rotation;
        }

        public void Set(in Vector3 scale)
        {
            transform.localScale = new Vector3(scale.z, scale.y, scale.x);
            transform.SetPositionAndRotation(Vector3.zero, PointToX);
        }

        public void Reset()
        {
            transform.localScale = Vector3.one;
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public override void Suspend()
        {
            base.Suspend();
            Reset();
        }
    }
}
