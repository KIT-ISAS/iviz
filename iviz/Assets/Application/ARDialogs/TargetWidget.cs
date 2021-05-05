using System;
using Iviz.App.ARDialogs;
using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Application.ARDialogs
{
    public class TargetWidget : ARWidget
    {
        public enum ModeType
        {
            Square,
            Circle,
        }

        [SerializeField] PolyGlowDisplay poly;
        [SerializeField] LineResource lines;
        [SerializeField] DraggablePlane disc;

        float scale;
        ModeType mode;
        bool moving;
        
        public event Action<TargetWidget, Vector3> Moved; 

        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                var scaleVector = new Vector3(value, 0.2f * value, value); 
                switch (Mode)
                {
                    case ModeType.Circle:
                        lines.Transform.localScale = 0.5f * scaleVector;
                        poly.Transform.localScale = 0.5f * scaleVector;
                        break;
                    case ModeType.Square:
                        lines.Transform.localScale = 1.4142135f / 2 * scaleVector;
                        poly.Transform.localScale = 1.4142135f / 2 * scaleVector;
                        break;
                }
            }
        }

        public ModeType Mode
        {
            get => mode;
            set
            {
                switch (value)
                {
                    case ModeType.Square:
                        SetLines(4);
                        poly.SetToSquare();
                        var rotation = Quaternion.AngleAxis(45, Vector3.up);
                        lines.Transform.localRotation = rotation;
                        poly.Transform.localRotation = rotation;
                        break;
                    case ModeType.Circle:
                        SetLines(40);
                        poly.SetToCircle();
                        lines.Transform.localRotation = Quaternion.identity;
                        poly.Transform.localRotation = Quaternion.identity;
                        break;
                }

                Scale = Scale;
            }
        }


        void Awake()
        {
            lines = ResourcePool.RentDisplay<LineResource>(transform);
            poly.EmissiveColor = poly.Color.WithAlpha(1);
            lines.ElementScale = 0.05f;
            disc.TargetTransform = Transform;
            
            Scale = 1;
            Mode = ModeType.Square;

            disc.PointerDown += () => moving = true;
            disc.PointerUp += () => moving = false;
        }

        void Update()
        {
            if (moving)
            {
                Moved?.Invoke(this, Transform.localPosition);
            }
        }

        void SetLines(int numVertices)
        {
            using (var segments = new NativeList<LineWithColor>())
            {
                for (int i = 0; i < numVertices; i++)
                {
                    float a0 = Mathf.PI * 2 / numVertices * i;
                    float a1 = Mathf.PI * 2 / numVertices * (i + 1);

                    Vector3 dirA0 = new Vector3(Mathf.Cos(a0), 0, Mathf.Sin(a0));
                    Vector3 dirA1 = new Vector3(Mathf.Cos(a1), 0, Mathf.Sin(a1));

                    segments.Add(new LineWithColor(dirA0, dirA1));
                }

                lines.Set(segments);
            }
        }
    }
}