#nullable enable

using System;
using System.Threading;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.XR
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class SpringDisc : MonoBehaviour, IWidgetWithColor, IWidgetCanBeMoved
    {
        [SerializeField] MeshMarkerDisplay? anchor;
        [SerializeField] MeshMarkerDisplay? link;
        [SerializeField] XRScreenDraggable? draggable;
        [SerializeField] MeshMarkerDisplay? outerDisc;
        [SerializeField] MeshMarkerDisplay? innerDisc;
        [SerializeField] MeshMarkerDisplay? glow;
        [SerializeField] Color color = new(0, 0.6f, 1f);
        [SerializeField] Color secondaryColor = Color.white;

        CancellationTokenSource? tokenSource;

        MeshMarkerDisplay Anchor => anchor.AssertNotNull(nameof(anchor));
        MeshMarkerDisplay Link => link.AssertNotNull(nameof(link));
        MeshMarkerDisplay OuterDisc => outerDisc.AssertNotNull(nameof(outerDisc));
        MeshMarkerDisplay InnerDisc => innerDisc.AssertNotNull(nameof(innerDisc));
        MeshMarkerDisplay Glow => glow.AssertNotNull(nameof(glow));
        XRScreenDraggable Draggable => draggable.AssertNotNull(nameof(draggable));

        public event Action<Vector3>? Moved;
        public event Action? PointerUp;
        
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                OuterDisc.Color = value.ScaledBy(0.5f);
                Link.Color = value.WithAlpha(0.8f);
                Link.EmissiveColor = value.ScaledBy(0.7f);
                Glow.Color = value.WithAlpha(0.8f);
                Glow.EmissiveColor = value;
            }
        }

        public Color SecondColor
        {
            get => secondaryColor;
            set
            {
                secondaryColor = value;
                InnerDisc.Color = value;
                Anchor.Color = value;
            }
        }

        public bool Interactable
        {
            set => Draggable.Interactable = value;
        }

        void Awake()
        {
            Color = Color;
            SecondColor = SecondColor;
            Glow.Visible = false;

            Draggable.StartDragging += () =>
            {
                tokenSource?.Cancel();
                InnerDisc.EmissiveColor = SecondColor;
                OuterDisc.EmissiveColor = Color;
                Glow.Visible = true;
            };
            Draggable.Moved += () => OnDiscMoved(true);
            Draggable.EndDragging += () =>
            {
                InnerDisc.EmissiveColor = Color.black;
                OuterDisc.EmissiveColor = Color.black;
                Glow.Visible = false;

                Moved?.Invoke(Vector3.zero);
                PointerUp?.Invoke();

                tokenSource?.Cancel();
                tokenSource = new CancellationTokenSource();

                var startPosition = Draggable.Transform.localPosition;
                FAnimator.Spawn(tokenSource.Token, 0.1f, t =>
                {
                    Draggable.Transform.localPosition = (1 - Mathf.Sqrt(t)) * startPosition;
                    OnDiscMoved(false);
                });
            };

            Draggable.StartDragging += () => tokenSource?.Cancel();
            Draggable.Damping = null;
        }

        void OnDiscMoved(bool raiseOnMoved)
        {
            var discPosition = Draggable.Transform.localPosition;
            float discDistance = discPosition.Magnitude();

            Link.Transform.localScale = new Vector3(0.2f, 0.002f, discDistance);
            Link.Transform.localPosition = discPosition / 2;
            Link.Transform.localRotation = discPosition.sqrMagnitude < 0.01f 
                ? Quaternion.identity 
                : Quaternion.LookRotation(discPosition);

            if (raiseOnMoved)
            {
                Moved?.Invoke(discPosition);
            }
        }

        public void Suspend()
        {
            Moved = null;
            PointerUp = null;
            OuterDisc.Transform.localPosition = Vector3.zero;
            OnDiscMoved(false);
            tokenSource?.Cancel();
        }
    }
}