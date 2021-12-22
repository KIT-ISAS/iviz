#nullable enable

using System;
using System.Threading;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.ARDialogs
{
    /*
    [RequireComponent(typeof(BoxCollider))]
    public sealed class SpringDisc3D : MonoBehaviour, IWidgetWithColor
    {
        [SerializeField] MeshMarkerResource? anchor;
        [SerializeField] MeshMarkerResource? link;
        [SerializeField] XRScreenDraggable? draggable;
        [SerializeField] MeshMarkerResource? disc;
        [SerializeField] Color color = Color.white;
        [SerializeField] Color secondaryColor = Color.cyan;
        CancellationTokenSource? tokenSource;
        LineResource line;
        bool dragBack;
        
        MeshMarkerResource Anchor => anchor.AssertNotNull(nameof(anchor));
        MeshMarkerResource Link => link.AssertNotNull(nameof(link));
        MeshMarkerResource Disc => disc.AssertNotNull(nameof(disc));
        XRScreenDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        
        readonly NativeList<LineWithColor> lineBuffer = new();

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

        void Awake()
        {
            Draggable.EndDragging += () =>
            {
                dragBack = true;
                Moved?.Invoke(Vector3.zero);
            };

            disc.StartDragging += () =>
            {
                line.Visible = true;
                dragBack = false;
            };

            line = ResourcePool.RentDisplay<LineResource>(transform);
            line.Tint = Color.cyan.WithAlpha(0.8f);
            line.ElementScale = linkWidth / 2;
            line.Visible = false;
            
            lineBuffer.Add(new LineWithColor());
        }

        
        protected override void Update()
        {
            base.Update();
            
            anchor.transform.localRotation = disc.Transform.localRotation; // copy billboard
            
            var discPosition = disc.Transform.localPosition;
            float discDistance = discPosition.Magnitude();
            if (discDistance < 0.005f)
            {
                if (dragBack)
                {
                    disc.Transform.localPosition = Vector3.zero;
                    dragBack = false;
                    line.Visible = false;
                }

                return;
            }

            lineBuffer[0] = new LineWithColor(
                Vector3.zero, Color.white.WithAlpha(0), 
                disc.Transform.localPosition, Color.white);
            line.Set(lineBuffer);

            if (dragBack)
            {
                disc.Transform.localPosition = 0.9f * discPosition;
            }
            else
            {
                Moved?.Invoke(discPosition);
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            Moved = null;
            disc.Transform.localPosition = Vector3.zero;
            dragBack = false;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            lineBuffer.Dispose();
        }
        
        void IRecyclable.SplitForRecycle()
        {
            line.ReturnToPool();
        }
    }
    */
}