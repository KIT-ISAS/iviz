#nullable enable

using System;
using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Resources;
using UnityEngine;


namespace Iviz.Displays.XR
{
    public sealed class AreaHighlightBoundary : BaseBoundary
    {
        [SerializeField] PolyGlowDisplay? poly;
        LineDisplay? lines;

        PolyGlowDisplay Poly => poly.AssertNotNull(nameof(poly));
        LineDisplay Lines => ResourcePool.RentChecked(ref lines, Transform);

        public override Vector3 Scale
        {
            set
            {
                base.Scale = value;

                const float scaleFactor = 1.4142135f / 2;
                
                Poly.Transform.localScale = new Vector3(value.x * scaleFactor, value.y, value.z * scaleFactor);
                
                var bottomPosition = new Vector3(0, -value.y / 2, 0);
                Poly.Transform.localPosition = bottomPosition;
                Lines.Transform.localPosition = bottomPosition;
                
                Lines.Transform.localScale = value * scaleFactor;
            }
        }

        public PolyGlowModeType Mode
        {
            set
            {
                switch (value)
                {
                    case PolyGlowModeType.Square:
                        SetLines(4);
                        Poly.SetToSquare();
                        var rotation = Quaternion.AngleAxis(45, Vector3.up);
                        Lines.Transform.localRotation = rotation;
                        Poly.Transform.localRotation = rotation;
                        break;
                    case PolyGlowModeType.Circle:
                        SetLines(40);
                        Poly.SetToCircle();
                        Lines.Transform.localRotation = Quaternion.identity;
                        Poly.Transform.localRotation = Quaternion.identity;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public override Color Color
        {
            set
            {
                if (value.a == 0)
                {
                    Lines.Visible = false;
                    return;
                }

                Lines.Visible = true;
                Lines.Tint = value;
            }
        }

        public override Color SecondColor
        {
            set
            {
                if (value.a == 0)
                {
                    Poly.Visible = false;
                    return;
                }

                Poly.Visible = true;
                Poly.EmissiveColor = value;
                Poly.Color = value.WithAlpha(Mathf.Min(value.a, 0.95f));
            }
        }

        void Awake()
        {
            Lines.ElementScale = 0.02f;
            Lines.Transform.localPosition = new Vector3(0, -0.5f, 0);
            Mode = PolyGlowModeType.Square;
        }

        void SetLines(int numVertices)
        {
            Span<LineWithColor> segments = stackalloc LineWithColor[numVertices];
            for (int i = 0; i < numVertices; i++)
            {
                float a0 = Mathf.PI * 2 / numVertices * i;
                float a1 = Mathf.PI * 2 / numVertices * (i + 1);

                var dirA0 = new Vector3(Mathf.Cos(a0), 0, Mathf.Sin(a0));
                var dirA1 = new Vector3(Mathf.Cos(a1), 0, Mathf.Sin(a1));

                segments[i] = new LineWithColor(dirA0, dirA1);
            }

            Lines.Set(segments, false);
        }
    }
}