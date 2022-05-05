#nullable enable

using System;
using Iviz.App.ARDialogs;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using UnityEngine;
using UnityEngine.UIElements;

namespace Iviz.Displays.XR
{
    public sealed class TargetWidget : MonoBehaviour, IWidgetCanBeResized, IWidgetCanBeClicked, IWidgetWithCaption,
        IWidgetWithColor, IRecyclable
    {
        public enum ModeType
        {
            Square,
            Circle,
        }

        [SerializeField] MeshMarkerDisplay? outer;
        [SerializeField] MeshMarkerDisplay? inner;
        [SerializeField] MeshMarkerDisplay? glow;
        [SerializeField] MeshMarkerDisplay? arrow;
        [SerializeField] PolyGlowDisplay? poly;
        [SerializeField] PlaneDraggable? draggable;
        [SerializeField] PlaneDraggable? corner;
        [SerializeField] XRButton? okButton;
        [SerializeField] XRButton? cancelButton;
        [SerializeField] Transform? pivotTransform;
        LineDisplay? lines;

        Transform? mTransform;
        Vector2 targetScale;
        ModeType mode;

        Color color = new(0.6f, 0, 1f);
        Color secondaryColor = Color.white;

        string caption = "Send!";

        MeshMarkerDisplay Arrow => arrow.AssertNotNull(nameof(arrow));
        MeshMarkerDisplay Outer => outer.AssertNotNull(nameof(outer));
        MeshMarkerDisplay Inner => inner.AssertNotNull(nameof(inner));
        MeshMarkerDisplay Glow => glow.AssertNotNull(nameof(glow));
        PolyGlowDisplay Poly => poly.AssertNotNull(nameof(poly));
        ScreenDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        ScreenDraggable Corner => corner.AssertNotNull(nameof(corner));
        LineDisplay Lines => ResourcePool.RentChecked(ref lines, PivotTransform);
        Transform PivotTransform => pivotTransform.AssertNotNull(nameof(pivotTransform));
        Transform Transform => this.EnsureHasTransform(ref mTransform);
        XRButton OkButton => okButton.AssertNotNull(nameof(okButton));
        XRButton CancelButton => cancelButton.AssertNotNull(nameof(cancelButton));

        public event Action<Bounds>? Resized;
        public event Action<int>? Clicked;

        Vector2 TargetScale
        {
            get => targetScale;
            set
            {
                targetScale = value;
                var scaleVector = new Vector3(value.x, 0.5f * value.x, value.y);
                PivotTransform.localScale = Mode switch
                {
                    ModeType.Circle => 0.5f * scaleVector,
                    ModeType.Square => 1.4142135f / 2 * scaleVector,
                    _ => Vector3.one
                };
            }
        }

        public ModeType Mode
        {
            get => mode;
            set
            {
                mode = value;
                switch (value)
                {
                    case ModeType.Square:
                        SetLines(4);
                        Poly.SetToSquare();
                        var rotation = Quaternion.AngleAxis(45, Vector3.up);
                        Lines.Transform.localRotation = rotation;
                        Poly.Transform.localRotation = rotation;
                        break;
                    case ModeType.Circle:
                        SetLines(40);
                        Poly.SetToCircle();
                        Lines.Transform.localRotation = Quaternion.identity;
                        Poly.Transform.localRotation = Quaternion.identity;
                        break;
                }

                TargetScale = TargetScale;
            }
        }

        public string Caption
        {
            set
            {
                caption = value;
                OkButton.Caption = value;
            }
        }

        public bool Interactable
        {
            set
            {
                Draggable.enabled = value;
                Corner.enabled = value;
            }
        }

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                Outer.Color = value.WithValue(0.5f);
                Poly.Color = value.WithAlpha(0.8f);
                Poly.EmissiveColor = value;
                Glow.Color = value.WithAlpha(0.8f);
                Glow.EmissiveColor = value;
                Arrow.Color = value.WithAlpha(0.8f);
                Arrow.EmissiveColor = value;
            }
        }

        public Color SecondaryColor
        {
            get => secondaryColor;
            set
            {
                secondaryColor = value;
                Inner.Color = value;
                Lines.Tint = value;
            }
        }

        void Awake()
        {
            Lines.ElementScale = 0.05f;
            Color = Color;
            SecondaryColor = SecondaryColor;

            TargetScale = Vector2.one;
            Mode = ModeType.Square;

            Corner.Moved += OnMove;
            Corner.EndDragging += OnMove;

            OkButton.Icon = XRIcon.Ok;
            Caption = caption;

            CancelButton.Caption = "Cancel";
            CancelButton.Icon = XRIcon.Cross;

            OkButton.Clicked += () =>
            {
                Debug.Log($"{this}: Sending scale");
                //float totalScale = transform.parent.lossyScale.x;
                //Debug.Log(TargetScale / Scale / totalScale);
                var bounds = new Bounds(Transform.localPosition, new Vector3(TargetScale.x, 0, TargetScale.y));
                Resized?.Invoke(bounds);
            };
            CancelButton.Clicked += () => Clicked?.Invoke(0);
        }

        void OnMove()
        {
            (float x, _, float z) = Corner.Transform.localPosition;
            TargetScale = 2 * new Vector2(x, z).Abs();
        }


        /*
        protected override void Update()
        {
            base.Update();

            var camPosition = Settings.MainCameraTransform.position;
            buttonPivotTransform.LookAt(2 * buttonPivotTransform.position - camPosition, Vector3.up);

            if (scaling)
            {
                (float x, _, float z) = corner.Transform.localPosition;
                TargetScale = 2 * new Vector2(x, z).Abs();
            }
        }
        */

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

        public void SplitForRecycle()
        {
            lines.ReturnToPool();
        }

        public void Suspend()
        {
            TargetScale = Vector2.one;
            Corner.Transform.localPosition = new Vector3(0.5f, 0, 0.5f);
            Clicked = null;
            Resized = null;
        }
    }
}