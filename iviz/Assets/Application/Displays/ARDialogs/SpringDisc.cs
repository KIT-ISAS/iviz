#nullable enable

using System;
using System.Threading;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.ARDialogs
{
    public interface IWidgetWithColor : IDisplay
    {
        Color Color { set; }
        Color SecondaryColor { set; }
    }

    public interface IWidgetWithCaption : IDisplay
    {
        string Caption { set; }
    }
    
    [RequireComponent(typeof(BoxCollider))]
    public sealed class SpringDisc : MonoBehaviour, IWidgetWithColor
    {
        [SerializeField] MeshMarkerResource? anchor;
        [SerializeField] MeshMarkerResource? link;
        [SerializeField] XRScreenDraggable? draggable;
        [SerializeField] MeshMarkerResource? disc;
        [SerializeField] Color color = Color.white;
        [SerializeField] Color secondaryColor = Color.cyan;
        CancellationTokenSource? tokenSource;

        MeshMarkerResource Anchor => anchor.AssertNotNull(nameof(anchor));
        MeshMarkerResource Link => link.AssertNotNull(nameof(link));
        MeshMarkerResource Disc => disc.AssertNotNull(nameof(disc));
        XRScreenDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        
        public event Action<Vector3>? Moved;

        public Color Color
        {
            set
            {
                color = value;
                Disc.Color = value;
                Anchor.Color = value;
            }
        }

        public Color SecondaryColor
        {
            set
            {
                secondaryColor = value;
                Link.Color = value.WithAlpha(0.8f);
            }
        }

        void Awake()
        {
            Color = color;
            SecondaryColor = secondaryColor;

            Draggable.Moved += () => OnDiscMoved(true);
            Draggable.EndDragging += () =>
            {
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
            //.cyan.WithAlpha(0.8f);
        }

        void OnDiscMoved(bool raiseOnMoved)
        {
            var discPosition = Draggable.Transform.localPosition;
            float discDistance = discPosition.Magnitude();
            float angle = -Mathf.Atan2(discPosition.z, discPosition.x) * Mathf.Rad2Deg;
            Link.Transform.localScale = new Vector3(discDistance, 0.002f, 0.2f);
            Link.Transform.SetLocalPose(new Pose(discPosition / 2, Quaternion.AngleAxis(angle, Vector3.up)));

            if (raiseOnMoved)
            {
                Moved?.Invoke(discPosition);
            }
        }

        public Bounds? Bounds => Disc.Bounds;

        public int Layer
        {
            set
            {
                Disc.Layer = value;
                Link.Layer = value;
                Anchor.Layer = value;
            }
        }

        public void Suspend()
        {
            Moved = null;
            Disc.Transform.localPosition = Vector3.zero;
            tokenSource?.Cancel();
        }
    }
}