#nullable enable

using System;
using System.Threading;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.ARDialogs
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class SpringDisc3D : MonoBehaviour, IWidgetWithColor
    {
        [SerializeField] MeshMarkerResource? anchor;
        [SerializeField] MeshMarkerResource? link;
        [SerializeField] XRScreenDraggable? draggable;
        [SerializeField] MeshMarkerResource? disc;
        [SerializeField] MeshMarkerResource? glow;
        CancellationTokenSource? tokenSource;
        
        MeshMarkerResource Anchor => anchor.AssertNotNull(nameof(anchor));
        MeshMarkerResource Link => link.AssertNotNull(nameof(link));
        MeshMarkerResource Disc => disc.AssertNotNull(nameof(disc));
        MeshMarkerResource Glow => glow.AssertNotNull(nameof(glow));
        XRScreenDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        
        readonly NativeList<LineWithColor> lineBuffer = new();
        Color color = new Color(0, 0.6f, 1f);
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
        
        public Color SecondaryColor
        {
            get => secondaryColor;
            set
            {
                secondaryColor = value;
                Anchor.Color = value;
            }
        }    
        
        public Bounds? Bounds => Disc.Bounds;

        public int Layer
        {
            set => gameObject.layer = value;
        }

        void Awake()
        {
            Color = Color;
            SecondaryColor = SecondaryColor;
            Glow.Visible = false;

            Draggable.StartDragging += () =>
            {
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
            Link.Transform.localRotation = Quaternion.LookRotation(discPosition);

            if (raiseOnMoved)
            {
                Moved?.Invoke(discPosition);
            }
        }

        public void Suspend()
        {
            Moved = null;
            Disc.Transform.localPosition = Vector3.zero;
            OnDiscMoved(false);
            tokenSource?.Cancel();
        }
    }
}