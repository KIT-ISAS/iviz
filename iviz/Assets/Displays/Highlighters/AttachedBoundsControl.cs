#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.Highlighters
{
    public class AttachedBoundsControl : IBoundsControl
    {
        readonly Transform nodeTransform;
        readonly BoxCollider collider;

        readonly IHasBounds source;
        readonly bool useTransformFromSource;
        SelectionFrame? frame;

        protected readonly ScreenDraggable draggable;

        public float FrameColumnWidth { get; set; } = 0.001f;
        public event Action? PointerDown;
        public event Action? PointerUp;
        public event Action? Moved;
        public event Action? StartDragging;
        public event Action? EndDragging;

        public bool Interactable
        {
            set => collider.enabled = value;
        }

        public bool Visible
        {
            set => nodeTransform.gameObject.SetActive(value);
        }

        public Quaternion BaseOrientation
        {
            set
            {
                if (draggable != null)
                {
                    draggable.BaseOrientation = value;
                }
            }
        }

        protected AttachedBoundsControl(IHasBounds newSource, Transform? target, Type type)
        {
            var node = new GameObject("[Clickable Control]")
            {
                layer = LayerType.Clickable
            };

            collider = node.gameObject.AddComponent<BoxCollider>();
            nodeTransform = node.transform;

            source = newSource;
            if (source.BoundsTransform is { } boundsTransform)
            {
                nodeTransform.SetParentLocal(boundsTransform.parent);
                nodeTransform.SetLocalPose(boundsTransform.AsLocalPose());
                nodeTransform.localScale = boundsTransform.localScale;
            }
            else
            {
                nodeTransform.localScale = Vector3.zero;
            }

            draggable = (ScreenDraggable)nodeTransform.gameObject.AddComponent(type);
            draggable.RayCollider = collider;

            useTransformFromSource = target == null;
            if (target != null)
            {
                draggable.TargetTransform = target;
            }
            else if (source.BoundsTransform != null)
            {
                draggable.TargetTransform = source.BoundsTransform;
            }

            draggable.PointerDown += () => PointerDown.TryRaise(this);
            draggable.PointerUp += () => PointerUp.TryRaise(this);
            draggable.Moved += () => Moved.TryRaise(this);
            draggable.StartDragging += () => StartDragging.TryRaise(this);
            draggable.EndDragging += () => EndDragging.TryRaise(this);

            draggable.StateChanged += () =>
            {
                if ((draggable.IsDragging || draggable.IsHovering))
                {
                    if (source.VisibleBounds is not { } visibleBounds)
                    {
                        Reset();
                        return;
                    }
                    
                    if (frame == null)
                    {
                        frame = ResourcePool.RentDisplay<SelectionFrame>(nodeTransform);
                        frame.Size = visibleBounds.size;
                        frame.Transform.localPosition = visibleBounds.center;
                    }

                    frame.Visible = true;
                    frame.EmissiveColor = draggable.IsDragging
                        ? Resource.Colors.DraggableSelectedEmissive
                        : Resource.Colors.DraggableHoverEmissive;
                    frame.ColumnWidth = draggable.IsDragging ? 2 * FrameColumnWidth : FrameColumnWidth;
                    frame.Color = draggable.IsDragging
                        ? Resource.Colors.DraggableSelectedColor
                        : Resource.Colors.DraggableHoverColor;

                    frame.UpdateColumnWidth();
                }
                else if (frame != null)
                {
                    GameThread.Post(frame.ReturnToPool); // stores copy of frame
                    frame = null;
                }
            };

            source.BoundsChanged += OnBoundsChanged;
            OnBoundsChanged();
        }

        void OnBoundsChanged()
        {
            if (source.BoundsTransform is { } validTransform)
            {
                nodeTransform.SetParentLocal(validTransform.parent);
                nodeTransform.SetLocalPose(validTransform.AsLocalPose());
                nodeTransform.localScale = validTransform.localScale;
                if (useTransformFromSource)
                {
                    draggable.TargetTransform = validTransform;
                }
            }
            else
            {
                nodeTransform.localScale = Vector3.zero;
            }

            if (source.Bounds is not { } validBounds)
            {
                Reset();
                return;
            }

            collider.SetLocalBounds(validBounds);

            if (frame == null)
            {
                return;
            }

            if (source.VisibleBounds is not { } visibleBounds)
            {
                Reset();
                return;
            }

            frame.Size = visibleBounds.size;
            frame.Transform.localPosition = visibleBounds.center;
            frame.Visible = draggable.IsDragging || draggable.IsHovering;
            frame.UpdateColumnWidth();
        }

        public void Reset()
        {
            frame.ReturnToPool();
            frame = null;
        }

        public void Dispose()
        {
            PointerDown = null;
            PointerUp = null;
            Moved = null;
            StartDragging = null;
            EndDragging = null;

            Reset();

            source.BoundsChanged -= OnBoundsChanged;

            UnityEngine.Object.Destroy(nodeTransform.gameObject);
        }
    }

    public sealed class StaticBoundsControl : AttachedBoundsControl
    {
        public StaticBoundsControl(IHasBounds source) :
            base(source, null, typeof(StaticDraggable))
        {
        }
    }

    public sealed class LineBoundsControl : AttachedBoundsControl
    {
        public LineBoundsControl(IHasBounds source, Transform target) :
            base(source, target, typeof(LineDraggable))
        {
        }
    }

    public sealed class PlaneBoundsControl : AttachedBoundsControl
    {
        public PlaneBoundsControl(IHasBounds source, Transform target) :
            base(source, target, typeof(PlaneDraggable))
        {
        }
    }

    public sealed class RotationBoundsControl : AttachedBoundsControl
    {
        public RotationBoundsControl(IHasBounds source, Transform target) :
            base(source, target, typeof(RotationDraggable))
        {
        }
    }

    public sealed class FixedDistanceBoundsControl : AttachedBoundsControl
    {
        public FixedDistanceBoundsControl(IHasBounds source, Transform target) :
            base(source, target, typeof(FixedDistanceDraggable))
        {
            if (Settings.IsXR)
            {
                ((FixedDistanceDraggable)draggable).ForwardScale = 5f;
            }
        }
    }
}