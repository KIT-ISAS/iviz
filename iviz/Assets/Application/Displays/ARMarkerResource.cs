using UnityEngine;
using System.Collections;

namespace Iviz.Displays
{
    public class ARMarkerResource : MeshMarkerResource
    {
        float scale;
        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                transform.localScale = 0.11f * scale * Vector3.one;
            }
        }

        int angle;
        public int Angle
        {
            get => angle;
            set
            {
                angle = value;
                UpdateRotation();
            }
        }

        void UpdateRotation()
        {
            Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.up);
            if (!horizontal)
            {
                rotation = rotation * Quaternion.AngleAxis(90, Vector3.forward);
            }
            transform.rotation = rotation;
        }

        bool horizontal;
        public bool Horizontal
        {
            get => horizontal;
            set
            {
                horizontal = value;
                UpdateRotation();
            }
        }

        Vector3 offset;
        public Vector3 Offset
        {
            get => offset;
            set
            {
                offset = value;
                transform.localPosition = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Color = new Color(0, 1, 0, 0.25f);
            Scale = 0.19f;
        }
    }
}
