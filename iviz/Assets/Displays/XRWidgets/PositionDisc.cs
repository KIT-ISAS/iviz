#nullable enable

using System;
using System.Threading;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class PositionDisc : MonoBehaviour, IRecyclable, IWidgetWithColor, IWidgetCanBeMoved,
        IWidgetWithCaption, IWidgetWithScale
    {
        [SerializeField] XRButton? button;
        [SerializeField] MeshMarkerDisplay? anchor;
        [SerializeField] XRScreenDraggable? draggable;
        [SerializeField] MeshMarkerDisplay? outerDisc;
        [SerializeField] MeshMarkerDisplay? innerDisc;
        [SerializeField] MeshMarkerDisplay? glow;
        [SerializeField] float linkWidth = 0.04f;
        [SerializeField] float buttonDistance = 1f;

        LineDisplay? line;
        Tooltip? tooltipX;
        Tooltip? tooltipY;
        Transform? mTransform;

        CancellationTokenSource? tokenSource;
        string mainButtonCaption = "Send!";
        Color color = new(0, 1f, 0.6f);
        Color secondaryColor = Color.white;

        MeshMarkerDisplay Anchor => anchor.AssertNotNull(nameof(anchor));
        MeshMarkerDisplay OuterDisc => outerDisc.AssertNotNull(nameof(outerDisc));
        MeshMarkerDisplay InnerDisc => innerDisc.AssertNotNull(nameof(innerDisc));
        MeshMarkerDisplay Glow => glow.AssertNotNull(nameof(glow));
        XRScreenDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        LineDisplay Line => ResourcePool.RentChecked(ref line, Transform);
        Tooltip TooltipX => ResourcePool.RentChecked(ref tooltipX, Transform);
        Tooltip TooltipY => ResourcePool.RentChecked(ref tooltipY, Transform);
        Transform Transform => this.EnsureHasTransform(ref mTransform);
        XRButton Button => button.AssertNotNull(nameof(button));

        public event Action<Vector3>? Moved;

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                OuterDisc.Color = value.WithValue(0.5f);
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
                InnerDisc.Color = value;
                Anchor.Color = value;
            }
        }

        public string Caption
        {
            get => mainButtonCaption;
            set
            {
                mainButtonCaption = value;
                Button.Caption = value;
            }
        }

        public float Scale { get; set; } = 1;

        public bool Interactable
        {
            set
            {
                Draggable.Interactable = value;
                Button.Interactable = value;
            }
        }

        void Awake()
        {
            Line.ElementScale = linkWidth;
            Line.Visible = false;

            Color = Color;
            SecondaryColor = SecondaryColor;
            Glow.Visible = false;

            Button.Icon = XRIcon.Ok;
            Button.Caption = "Send!";
            Button.Visible = false;
            Button.Transform.SetParentLocal(Transform);
            Caption = Caption;

            TooltipX.Color = Color.red.WithAlpha(0.5f);
            TooltipY.Color = Color.green.WithAlpha(0.5f);
            TooltipX.Scale = 0.03f;
            TooltipY.Scale = 0.03f;
            TooltipX.Visible = false;
            TooltipY.Visible = false;

            Draggable.StartDragging += () =>
            {
                Button.Visible = false;
                tokenSource?.Cancel();
                Line.Visible = true;
                TooltipX.Visible = true;
                TooltipY.Visible = true;
                InnerDisc.EmissiveColor = SecondaryColor;
                OuterDisc.EmissiveColor = Color;
                Glow.Visible = true;
            };
            Draggable.Moved += () =>
            {
                var discPosition = Draggable.Transform.localPosition;

                var p0 = Vector3.zero;
                var p1 = new Vector3(0, 0, discPosition.z);
                var p2 = discPosition;

                ReadOnlySpan<LineWithColor> lineBuffer = stackalloc LineWithColor[]
                {
                    new LineWithColor(p0, p1, Color.red.WithAlpha(0.5f)),
                    new LineWithColor(p1, p2, Color.green.WithAlpha(0.5f))
                };

                TooltipX.Transform.localPosition = ((p1 + p0) / 2).WithY(0.5f);
                TooltipY.Transform.localPosition = ((p2 + p1) / 2).WithY(0.5f);

                TooltipX.Caption = "X: " + ((p1 - p0) / Scale).WithY(0).Magnitude().ToString("0.###") + " m";
                TooltipY.Caption = "Y: " + ((p2 - p1) / Scale).WithY(0).Magnitude().ToString("0.###") + " m";

                Line.Set(lineBuffer, true);
            };
            Draggable.EndDragging += () =>
            {
                InnerDisc.EmissiveColor = Color.black;
                OuterDisc.EmissiveColor = Color.black;
                Glow.Visible = false;

                Button.Transform.localPosition = Draggable.Transform.localPosition + buttonDistance * Vector3.up;
                Button.Visible = true;
            };

            Button.Clicked += () =>
            {
                Button.Visible = false;
                TooltipX.Visible = false;
                TooltipY.Visible = false;
                Line.Visible = false;

                tokenSource?.Cancel();
                tokenSource = new CancellationTokenSource();

                var startPosition = Draggable.Transform.localPosition;
                FAnimator.Spawn(tokenSource.Token, 0.1f,
                    t => { Draggable.Transform.localPosition = (1 - Mathf.Sqrt(t)) * startPosition; });

                Moved?.Invoke(startPosition);
            };
        }

        public void Suspend()
        {
            Button.Transform.parent = Transform;
            Button.Visible = false;
            TooltipX.Visible = false;
            TooltipY.Visible = false;
            Draggable.Transform.localPosition = Vector3.zero;
            Line.Visible = false;
            Moved = null;
        }

        public void SplitForRecycle()
        {
            line.ReturnToPool();
        }
    }
}