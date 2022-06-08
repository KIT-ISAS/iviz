#nullable enable

using System;
using System.Threading;
using Iviz.Core;
using Iviz.Tools;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class RotationDisc : MonoBehaviour,
        IWidgetWithColor, IWidgetWithSecondaryScale, IWidgetCanBeRotated, IRecyclable
    {
        [SerializeField] CirclePlaneDraggable? planeCircle;
        [SerializeField] CircleFixedDistanceDraggable? fixedCircle;
        [SerializeField] Transform? disc;
        [SerializeField] MeshMarkerDisplay? outer;
        [SerializeField] MeshMarkerDisplay? glow;
        LineDisplay? partialLines;
        LineDisplay? ringLines;

        const int RingElements = 64;

        float currentAngle;
        Color color = new(0, 0.6f, 1f);
        Color secondaryColor = Color.white;
        CancellationTokenSource? tokenSource;

        MeshMarkerDisplay Glow => glow.AssertNotNull(nameof(glow));
        MeshMarkerDisplay Outer => outer.AssertNotNull(nameof(outer));
        Transform Disc => disc.AssertNotNull(nameof(disc));
        LineDisplay PartialLines => ResourcePool.RentChecked(ref partialLines, transform);
        LineDisplay RingLines => ResourcePool.RentChecked(ref ringLines, transform);

        XRScreenDraggable Draggable => Settings.IsXR
            ? fixedCircle.AssertNotNull(nameof(fixedCircle))
            : planeCircle.AssertNotNull(nameof(planeCircle));

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                Glow.Color = value.WithAlpha(0.8f);
                Glow.EmissiveColor = value;
                PartialLines.Tint = value;
            }
        }

        public Color SecondaryColor
        {
            get => secondaryColor;
            set
            {
                secondaryColor = value;
                Outer.Color = value;
                RingLines.Tint = value;
            }
        }

        public float SecondaryScale
        {
            set => Disc.localScale = value * 0.5f * Vector3.one;
        }

        public bool Interactable
        {
            set => Draggable.Interactable = value;
        }

        public event Action<float>? Moved;

        void Awake()
        {
            Color = Color;
            SecondaryColor = SecondaryColor;
            Glow.Visible = false;

            PartialLines.ElementScale = 0.02f;
            PartialLines.RenderType = LineDisplay.LineRenderType.AlwaysCapsule;

            RingLines.ElementScale = 0.005f;
            RingLines.RenderType = LineDisplay.LineRenderType.AlwaysCapsule;

            {
                Span<LineWithColor> ringBuffer = stackalloc LineWithColor[RingElements];
                foreach (int i in ..RingElements)
                {
                    float a0 = 2 * Mathf.PI * i / RingElements;
                    float a1 = 2 * Mathf.PI * (i + 1) / RingElements;
                    var v0 = new Vector3(Mathf.Cos(a0), 0, Mathf.Sin(a0));
                    var v1 = new Vector3(Mathf.Cos(a1), 0, Mathf.Sin(a1));
                    ringBuffer[i] = new LineWithColor(v0, v1);
                }

                RingLines.Set(ringBuffer, false);
            }

            var draggable = Draggable;

            draggable.Interactable = true;
            draggable.StartDragging += () =>
            {
                tokenSource?.Cancel();
                Outer.Color = Color;
                Outer.EmissiveColor = Color;
                Glow.Visible = true;
                currentAngle = 0;
            };

            draggable.Moved += () => OnDiscMoved(draggable, true);

            draggable.EndDragging += () =>
            {
                Outer.Color = SecondaryColor;
                Outer.EmissiveColor = Color.black;
                Glow.Visible = false;

                Moved?.Invoke(0);

                tokenSource?.Cancel();
                tokenSource = new CancellationTokenSource();

                {
                    float startAngle = currentAngle;
                    FAnimator.Spawn(tokenSource.Token, 0.1f, t =>
                    {
                        float angle = (1 - Mathf.Sqrt(t)) * startAngle;
                        currentAngle = angle;
                        draggable.Transform.localPosition = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                        OnDiscMoved(draggable, false);
                    });
                }
            };
        }

        void OnDiscMoved(ScreenDraggable draggable, bool raiseOnMoved)
        {
            // update angle
            var (discX, _, discZ) = draggable.Transform.localPosition;
            float discAngle = Mathf.Atan2(discX, discZ);
            currentAngle += AngleDifference(discAngle, currentAngle);

            // update lines
            var line = new float4x2();
            ref var a = ref line.c0;
            ref var b = ref line.c1;

            Span<float4x2> lineBuffer = stackalloc float4x2[RingElements];
            
            (a.x, a.z) = (0, 1);
            foreach (int i in ..RingElements)
            {
                float angle = currentAngle * (i + 1) / RingElements;
                float scale = 1 - Mathf.Abs(angle) * 0.01f;
                (b.x, b.z) = (Mathf.Sin(angle) * scale, Mathf.Cos(angle) * scale);
                lineBuffer[i] = line;
                a = b;
            }

            PartialLines.Set(lineBuffer, false);

            if (raiseOnMoved)
            {
                Moved?.Invoke(-currentAngle);
            }
        }

        static float AngleDifference(float angle2, float angle1)
        {
            float diff = (angle2 - angle1 + Mathf.PI) % (2 * Mathf.PI) - Mathf.PI;
            return diff < -Mathf.PI ? diff + 2 * Mathf.PI : diff;
        }

        public void SplitForRecycle()
        {
            partialLines.ReturnToPool();
            ringLines.ReturnToPool();
        }

        public void Suspend()
        {
            Moved = null;
            Disc.localPosition = Vector3.forward;
            PartialLines.Reset();

            Color = Color;
            SecondaryColor = SecondaryColor;
            SecondaryScale = 1;
            Glow.Visible = false;
        }
    }
}