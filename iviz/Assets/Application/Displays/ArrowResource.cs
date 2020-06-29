using UnityEngine;
using System.Collections;

namespace Iviz.Displays
{
    public sealed class ArrowResource : MeshMarkerResource
    {
        public float Scale { get; set; } = 1;

        protected override void Awake()
        {
            base.Awake();
            Set(Vector3.zero, Vector3.zero);
        }

        public void Set(in Vector3 a, in Vector3 b)
        {
            Vector3 diff = (a - b) * Scale; // arrow model is flipped
            float scale = diff.magnitude;

            transform.localScale = scale * Vector3.one;
            transform.localPosition = a;

            if (scale == 0)
            {
                return;
            }
            
            Vector3 x = diff / scale;

            Vector3 up = new Vector3(0, 0, 1);
            if (x == up)
            {
                up = new Vector3(0, 1, 0);
            }

            Vector3 y = Vector3.Cross(x, up).normalized;
            Vector3 z = Vector3.Cross(x, y).normalized;

            Matrix4x4 M = Matrix4x4.identity;
            M.SetColumn(0, x);
            M.SetColumn(1, y);
            M.SetColumn(2, z);

            transform.localRotation = M.rotation;
        }
    }
}
