#nullable enable

using System;
using System.Threading;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class PositionDisc3D : MonoBehaviour, IRecyclable, IWidgetWithColor, IWidgetCanBeMoved,
        IWidgetWithCaption
    {
        [SerializeField] XRButton? button;
        [SerializeField] MeshMarkerDisplay? anchor;
        [SerializeField] XRScreenDraggable? draggable;
        [SerializeField] MeshMarkerDisplay? disc;
        [SerializeField] MeshMarkerDisplay? glow;
        [SerializeField] float linkWidth = 0.04f;
        [SerializeField] float buttonDistance = 1f;

        LineDisplay? line;
        Transform? mTransform;

        CancellationTokenSource? tokenSource;
        string mainButtonCaption = "Send!";

        Color color = new(0, 1f, 0.6f);
        Color secondaryColor = Color.white;

        MeshMarkerDisplay Anchor => anchor.AssertNotNull(nameof(anchor));
        MeshMarkerDisplay Disc => disc.AssertNotNull(nameof(disc));
        MeshMarkerDisplay Glow => glow.AssertNotNull(nameof(glow));
        XRScreenDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        LineDisplay Line => ResourcePool.RentChecked(ref line, Transform);
        Transform Transform => this.EnsureHasTransform(ref mTransform);
        XRButton Button => button.AssertNotNull(nameof(button));

        public event Action<Vector3>? Moved;

        public string Caption
        {
            get => mainButtonCaption;
            set
            {
                mainButtonCaption = value;
                Button.Caption = value;
            }
        }

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                Disc.Color = value.WithValue(0.5f);
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
                Anchor.Color = value;
            }
        }

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

            Draggable.StartDragging += () =>
            {
                Button.Visible = false;
                tokenSource?.Cancel();
                Line.Visible = true;
                Disc.EmissiveColor = Color;
                Glow.Visible = true;
            };
            Draggable.Moved += () =>
            {
                var discPosition = Draggable.Transform.localPosition;

                var p0 = Vector3.zero;
                var p1 = new Vector3(0, discPosition.y, 0);
                var p2 = new Vector3(0, discPosition.y, discPosition.z);
                var p3 = discPosition;

                ReadOnlySpan<LineWithColor> lineBuffer = stackalloc LineWithColor[]
                {
                    new LineWithColor(p0, p1, Color.blue.WithAlpha(0.5f)),
                    new LineWithColor(p1, p2, Color.red.WithAlpha(0.5f)),
                    new LineWithColor(p2, p3, Color.green.WithAlpha(0.5f))
                };

                Line.Set(lineBuffer, true);
            };
            Draggable.EndDragging += () =>
            {
                Disc.EmissiveColor = Color.black;
                Glow.Visible = false;

                Button.Transform.localPosition = Draggable.Transform.localPosition + buttonDistance * Vector3.up;
                Button.Visible = true;
            };
            Button.Clicked += () =>
            {
                Button.Visible = false;
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
            Draggable.Transform.localPosition = Vector3.zero;
            Button.Visible = false;
            Line.Visible = false;
            Moved = null;
        }

        public void SplitForRecycle()
        {
            line.ReturnToPool();
        }
    }
}