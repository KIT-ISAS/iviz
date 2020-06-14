using UnityEngine;
using System.Collections;

namespace Iviz.Displays
{
    public class ARMarkerResource : MeshMarkerResource
    {
        //[SerializeField] MeshMarkerResource back = null;

        [SerializeField] float scale_;
        public float Scale
        {
            get => scale_;
            set
            {
                scale_ = value;
                transform.localScale = 0.11f * scale_ * Vector3.one;
            }
        }

        [SerializeField] int angle_;
        public int Angle
        {
            get => angle_;
            set
            {
                angle_ = value;
                UpdateRotation();
            }
        }

        void UpdateRotation()
        {
            Quaternion rotation = Quaternion.AngleAxis(-angle_, Vector3.up);
            if (!horizontal_)
            {
                rotation *= Quaternion.AngleAxis(90, Vector3.forward);
            }
            transform.rotation = rotation;
        }

        [SerializeField] bool horizontal_;
        public bool Horizontal
        {
            get => horizontal_;
            set
            {
                horizontal_ = value;
                UpdateRotation();
            }
        }

        [SerializeField] Vector3 offset_;
        public Vector3 Offset
        {
            get => offset_;
            set
            {
                offset_ = value;
                transform.localPosition = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Scale = 0.19f;
        }
    }
}
