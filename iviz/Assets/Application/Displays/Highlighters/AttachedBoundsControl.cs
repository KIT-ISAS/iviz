using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays.Highlighters
{
    public abstract class AttachedBoundsControl : IBoundsControl
    {
        readonly Transform nodeTransform;
        readonly BoxCollider collider;

        [CanBeNull] SelectionFrame frame;

        public event Action PointerDown;
        public event Action PointerUp;
        public event Action Moved;

        public bool Interactable
        {
            set => collider.enabled = value;
        }
        
        protected AttachedBoundsControl()
        {
            var node = new GameObject("[Clickable Control]")
            {
                layer = LayerType.Clickable
            };
            
            collider = node.gameObject.AddComponent<BoxCollider>();
            nodeTransform = node.transform;
        }

        protected void InitializeDraggable<T>([NotNull] IHasBounds source, [NotNull] Transform target)
            where T : ScreenDraggable
        {
            if (source.BoundsTransform is { } boundsTransform)
            {
                nodeTransform.SetParentLocal(boundsTransform.parent);
                nodeTransform.SetLocalPose(boundsTransform.AsLocalPose());
                nodeTransform.localScale = boundsTransform.localScale;
            }

            var draggable = nodeTransform.gameObject.AddComponent<T>();
            draggable.RayCollider = collider;
            draggable.TargetTransform = target;
            draggable.PointerDown += () => PointerDown?.Invoke();
            draggable.PointerUp += () => PointerUp?.Invoke();
            draggable.Moved += () => Moved?.Invoke();
            draggable.StateChanged += () =>
            {
                if ((draggable.IsDragging || draggable.IsHovering) && source.Bounds is { } validBounds)
                {
                    if (frame == null)
                    {
                        frame = ResourcePool.RentDisplay<SelectionFrame>(nodeTransform);
                        frame.Color = Color.white;
                        frame.Size = validBounds.size;
                        frame.Transform.localPosition = validBounds.center;
                    }

                    frame.Visible = true;
                    frame.EmissiveColor = draggable.IsDragging ? Color.blue : Color.black;
                    frame.ColumnWidth = draggable.IsDragging ? 0.01f : 0.005f;
                    frame.Color = draggable.IsDragging ? Color.cyan : Color.white;
                }
                else if (frame != null)
                {
                    frame.ReturnToPool();
                    frame = null;
                }
            };

            source.BoundsChanged += () =>
            {
                if (frame == null)
                {
                    return;
                }

                if (source.BoundsTransform is { } validTransform)
                {
                    nodeTransform.SetParentLocal(validTransform.parent);
                    nodeTransform.SetLocalPose(validTransform.AsLocalPose());
                    nodeTransform.localScale = validTransform.localScale;
                }

                if (source.Bounds is { } validBounds)
                {
                    frame.Size = validBounds.size;
                    frame.Transform.localPosition = validBounds.center;
                    frame.Visible = draggable.IsDragging || draggable.IsHovering;
                }
                else
                {
                    frame.ReturnToPool();
                    frame = null;
                }
            };
        }

        public void Stop()
        {
            PointerDown = null;
            PointerUp = null;
            Moved = null;
            if (frame != null)
            {
                frame.ReturnToPool();
            }

            UnityEngine.Object.Destroy(nodeTransform.gameObject);
        }
    }
}