#nullable enable

using System;
using System.Threading;
using Iviz.Core;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Displays.XR
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class RotationDisc : MonoBehaviour,
        IWidget, IWidgetWithColor, IWidgetWithScale, IWidgetCanBeRotated, IRecyclable
    {
        [SerializeField] CirclePlaneDraggable? planeCircle;
        [SerializeField] CircleFixedDistanceDraggable? fixedCircle;
        [SerializeField] Transform? disc;
        [SerializeField] MeshMarkerDisplay? outer;
        [SerializeField] MeshMarkerDisplay? ring;
        [SerializeField] MeshMarkerDisplay? glow;
        LineDisplay? lines;

        const int RingElements = 64;

        readonly LineWithColor[] lineBuffer = new LineWithColor[RingElements];

        float currentAngle;
        Color color = new(0, 0.6f, 1f);
        Color secondaryColor = Color.white;
        CancellationTokenSource? tokenSource;

        MeshMarkerDisplay Glow => glow.AssertNotNull(nameof(glow));
        MeshMarkerDisplay Outer => outer.AssertNotNull(nameof(outer));
        MeshMarkerDisplay Ring => ring.AssertNotNull(nameof(ring));
        Transform Disc => disc.AssertNotNull(nameof(disc));
        LineDisplay Lines => lines != null ? lines : (lines = ResourcePool.RentDisplay<LineDisplay>(transform));

        XRScreenDraggable Draggable => Settings.IsXR
            ? fixedCircle.AssertNotNull(nameof(fixedCircle))
            : planeCircle.AssertNotNull(nameof(planeCircle));

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                Ring.Color = value.WithAlpha(0.2f);
                Ring.EmissiveColor = value;
                Glow.Color = value.WithAlpha(0.8f);
                Glow.EmissiveColor = value;
            }
        }

        public Color SecondaryColor
        {
            get => secondaryColor;
            set
            {
                secondaryColor = value;
                Outer.Color = value;
                Lines.Tint = value;
            }
        }

        public float SecondaryScale
        {
            set => Disc.localScale = value * Vector3.one;
        }

        public bool Interactable
        {
            set => Draggable.enabled = value;
        }

        public event Action<float>? Moved;

        void Awake()
        {
            Color = Color;
            SecondaryColor = SecondaryColor;
            Glow.Visible = false;

            Lines.ElementScale = 0.02f;
            Lines.RenderType = LineDisplay.LineRenderType.AlwaysCapsule;

            var draggable = Draggable;

            draggable.enabled = true;
            draggable.StartDragging += () =>
            {
                tokenSource?.Cancel();
                Ring.Color = Color.WithAlpha(0.8f);
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
                Ring.Color = Color.WithAlpha(0.2f);
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
            var line = new LineWithColor(Vector3.zero, Vector3.zero);
            ref var a = ref line.f.c0;
            ref var b = ref line.f.c1;

            (a.x, a.z) = (0, 1);
            foreach (int i in ..RingElements)
            {
                float angle = currentAngle * (i + 1) / RingElements;
                float scale = 1 - Mathf.Abs(angle) * 0.01f;
                (b.x, b.z) = (Mathf.Sin(angle) * scale, Mathf.Cos(angle) * scale);
                lineBuffer[i] = line;
                a = b;
            }

            Lines.Set(lineBuffer, false);

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
            lines.ReturnToPool();
        }

        public void Suspend()
        {
            Moved = null;
            Disc.localPosition = Vector3.forward;
            Lines.Reset();

            Color = Color;
            SecondaryColor = SecondaryColor;
            SecondaryScale = 0.4f;
            Glow.Visible = false;
        }
    }
}