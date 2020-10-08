using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ARMarkerResource : DisplayWrapperResource
    {
        [SerializeField] LineResource resource = null;
        protected override IDisplay Display => resource;
        
        [SerializeField] float scale;
        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                transform.localScale = scale * Vector3.one;
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

        void Awake()
        {
            Scale = 0.105f;

            resource = ResourcePool.GetOrCreateDisplay<LineResource>(transform);
            resource.ElementScale = 0.05f;

            Vector3 a = new Vector3(1, 0, 1) / 2;
            Vector3 b = new Vector3(-1, 0, 1) / 2;
            Vector3 c = new Vector3(-1, 0, -1) / 2;
            Vector3 d = new Vector3(1, 0, -1) / 2;
            resource.LinesWithColor = new []
            {
                new LineWithColor(a, b, Color.green), 
                new LineWithColor(b,  c, Color.green), 
                new LineWithColor(c, d, Color.green), 
                new LineWithColor(d, a, Color.green), 
            };
        }
    }
}
