#nullable enable

using System;
using System.Threading;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class SpringDisc3D : MonoBehaviour, IWidgetWithColor, IWidgetCanBeMoved
    {
        [SerializeField] MeshMarkerDisplay? anchor;
        [SerializeField] MeshMarkerDisplay? link;
        [SerializeField] XRScreenDraggable? draggable;
        [SerializeField] MeshMarkerDisplay? disc;
        [SerializeField] MeshMarkerDisplay? glow;
        CancellationTokenSource? tokenSource;
        
        MeshMarkerDisplay Anchor => anchor.AssertNotNull(nameof(anchor));
        MeshMarkerDisplay Link => link.AssertNotNull(nameof(link));
        MeshMarkerDisplay Disc => disc.AssertNotNull(nameof(disc));
        MeshMarkerDisplay Glow => glow.AssertNotNull(nameof(glow));
        XRScreenDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        
        Color color = new(0, 0.6f, 1f);
        Color secondaryColor = Color.white;

        public event Action<Vector3>? Moved;
        
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                Disc.Color = value.WithValue(0.5f);
                Link.Color = value.WithAlpha(0.8f);
                Link.EmissiveColor = value;
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
                Disc.EmissiveColor = Color;
                Glow.Visible = true;
            };
            Draggable.Moved += () => OnDiscMoved(true);
            Draggable.EndDragging += () =>
            {
                Disc.EmissiveColor = Color.black;
                Glow.Visible = false;

                Moved?.Invoke(Vector3.zero);

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
            
            Link.Transform.localScale = new Vector3(0.2f, 0.2f, discDistance);
            Link.Transform.localPosition = discPosition / 2;
            Link.Transform.localRotation = discDistance < 0.001f 
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
            Disc.Transform.localPosition = Vector3.zero;
            Disc.EmissiveColor = Color.black;
            Glow.Visible = false;

            OnDiscMoved(false);
            tokenSource?.Cancel();
        }
    }
}