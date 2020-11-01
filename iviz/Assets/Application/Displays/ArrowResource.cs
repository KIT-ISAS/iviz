using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ArrowResource : MeshMarkerResource
    {
        const float MaxArrowWidth = 5.0f;
        
        public float Scale { get; set; } = 1;

        protected override void Awake()
        {
            base.Awake();
            Set(Vector3.zero, Vector3.zero);
        }

        public void Set(in Vector3 a, in Vector3 b, float? overrideScaleYZ = null)
        {
            Vector3 diff = (a - b) * Scale; // arrow model is flipped
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

        public void Reset()
        {
            Set(Vector3.zero, Vector3.zero);
        }
    }
}
