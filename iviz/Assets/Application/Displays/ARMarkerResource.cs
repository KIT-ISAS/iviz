using UnityEngine;
using System.Collections;

namespace Iviz.Displays
{
    public sealed class ARMarkerResource : MeshMarkerResource
    {
        //[SerializeField] MeshMarkerResource back = null;

        [SerializeField] float scale;
        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                transform.localScale = 0.11f * scale * Vector3.one;
            }
        }

        [SerializeField] int angle;
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
            Quaternion rotation = Quaternion.AngleAxis(-Angle, Vector3.up);
            if (!Horizontal)
            {
                rotation *= Quaternion.AngleAxis(90, Vector3.forward);
            }
            transform.localRotation = rotation;
        }

        [SerializeField] bool horizontal;
        public bool Horizontal
        {
            get => horizontal;
            set
            {
                horizontal = value;
                UpdateRotation();
            }
        }

        [SerializeField] Vector3 offset;
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
            Scale = 0.105f;
        }
    }
}
