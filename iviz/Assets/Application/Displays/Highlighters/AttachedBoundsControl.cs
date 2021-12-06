#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.Highlighters
{
    public abstract class AttachedBoundsControl : IBoundsControl
    {
        readonly Transform nodeTransform;
        readonly BoxCollider collider;

        SelectionFrame? frame;

        public event Action? PointerDown;
        public event Action? PointerUp;
        public event Action? Moved;
        public event Action? StartDragging;
        public event Action? EndDragging;

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

        protected T InitializeDraggable<T>(IHasBounds source, Transform? target)
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
            draggable.TargetTransform = target.CheckedNull() ??
                                        source.BoundsTransform.CheckedNull() ??
                                        throw new ArgumentNullException(nameof(target));
            draggable.PointerDown += () => PointerDown?.Invoke();
            draggable.PointerUp += () => PointerUp?.Invoke();
            draggable.Moved += () => Moved?.Invoke();
            draggable.StartDragging += () => StartDragging?.Invoke();
            draggable.EndDragging += () => EndDragging?.Invoke();

            draggable.StateChanged += () =>
            {
                if ((draggable.IsDragging || draggable.IsHovering) && source.Bounds is { } validBounds)
                {
                    if (frame == null)
                    {
                        frame = ResourcePool.RentDisplay<SelectionFrame>(nodeTransform);
                        frame.Size = validBounds.size;
                        frame.Transform.localPosition = validBounds.center;
                    }

                    frame.Visible = true;
                    frame.EmissiveColor = draggable.IsDragging
                        ? Resource.Colors.DraggableSelectedEmissive
                        : Color.black;
                    frame.ColumnWidth = draggable.IsDragging ? 0.01f : 0.005f;
                    frame.Color = draggable.IsDragging
                        ? Resource.Colors.DraggableSelectedColor
                        : Resource.Colors.DraggableHoverColor;
                }
                else if (frame != null)
                {
                    frame.ReturnToPool();
                    frame = null;
                }
            };

            void OnBoundsChanged()
            {
                if (source.BoundsTransform is { } validTransform)
                {
                    nodeTransform.SetParentLocal(validTransform.parent);
                    nodeTransform.SetLocalPose(validTransform.AsLocalPose());
                    nodeTransform.localScale = validTransform.localScale;
                }

                if (source.Bounds is { } validBounds)
                {
                    if (frame != null)
                    {
                        frame.Size = validBounds.size;
                        frame.Transform.localPosition = validBounds.center;
                        frame.Visible = draggable.IsDragging || draggable.IsHovering;
                    }

                    collider.center = validBounds.center;
                    collider.size = validBounds.size;
                }
                else
                {
                    frame.ReturnToPool();
                    frame = null;
                }
            }

            source.BoundsChanged += OnBoundsChanged;
            OnBoundsChanged();

            return draggable;
        }

        public void Dispose()
        {
            PointerDown = null;
            PointerUp = null;
            Moved = null;
            StartDragging = null;
            EndDragging = null;

            if (frame != null)
            {
                frame.ReturnToPool();
            }

            UnityEngine.Object.Destroy(nodeTransform.gameObject);
        }
    }

    public sealed class StaticBoundsControl : AttachedBoundsControl
    {
        public StaticBoundsControl(IHasBounds source)
        {
            InitializeDraggable<StaticDraggable>(source, null);
        }
    }

    public sealed class LineBoundsControl : AttachedBoundsControl
    {
        public LineBoundsControl(IHasBounds source, Transform target, in Quaternion orientation)
        {
            var draggable = InitializeDraggable<LineDraggable>(source, target);
            draggable.Line = orientation * Vector3.forward;
        }
    }

    public sealed class PlaneBoundsControl : AttachedBoundsControl
    {
        public PlaneBoundsControl(IHasBounds source, Transform target, in Quaternion orientation)
        {
            var draggable = InitializeDraggable<PlaneDraggable>(source, target);
            draggable.Normal = orientation * Vector3.forward;
        }
    }

    public sealed class RotationBoundsControl : AttachedBoundsControl
    {
        public RotationBoundsControl(IHasBounds source, Transform target, in Quaternion orientation)
        {
            var draggable = InitializeDraggable<RotationDraggable>(source, target);
            draggable.Normal = orientation * Vector3.forward;
        }
    }

    public sealed class FixedDistanceBoundsControl : AttachedBoundsControl
    {
        public FixedDistanceBoundsControl(IHasBounds source, Transform target)
        {
            InitializeDraggable<FixedDistanceDraggable>(source, target);
        }
    }
}