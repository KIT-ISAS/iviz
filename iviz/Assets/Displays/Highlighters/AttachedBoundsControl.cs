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
        IHasBounds? source;
        ScreenDraggable? draggable;

        public float FrameColumnWidth { get; set; } = 0.005f;
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

        protected T InitializeDraggable<T>(IHasBounds newSource, Transform? target) where T : ScreenDraggable
        {
            source = newSource;
            if (source.BoundsTransform is { } boundsTransform)
            {
                nodeTransform.SetParentLocal(boundsTransform.parent);
                nodeTransform.SetLocalPose(boundsTransform.AsLocalPose());
                nodeTransform.localScale = boundsTransform.localScale;
            }

            draggable = nodeTransform.gameObject.AddComponent<T>();
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
                        : Resource.Colors.DraggableHoverEmissive;
                    frame.ColumnWidth = draggable.IsDragging ? 2 * FrameColumnWidth : FrameColumnWidth;
                    frame.Color = draggable.IsDragging
                        ? Resource.Colors.DraggableSelectedColor
                        : Resource.Colors.DraggableHoverColor;
                }
                else if (frame != null)
                {
                    var tmpFrame = frame;
                    GameThread.Post(() => tmpFrame.ReturnToPool());
                    frame = null;
                }
            };

            source.BoundsChanged += OnBoundsChanged;
            OnBoundsChanged();

            return (T)draggable;
        }

        void OnBoundsChanged()
        {
            if (source == null || draggable == null)
            {
                Debug.LogError($"{this}: OnBoundsChanged() called on uninitialized attached bounds");
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
                if (frame != null)
                {
                    frame.Size = validBounds.size;
                    frame.Transform.localPosition = validBounds.center;
                    frame.Visible = draggable.IsDragging || draggable.IsHovering;
                }

                collider.SetLocalBounds(validBounds);
            }
            else
            {
                frame.ReturnToPool();
                frame = null;
            }
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

            if (frame != null)
            {
                frame.ReturnToPool();
            }

            if (source != null)
            {
                source.BoundsChanged -= OnBoundsChanged;
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
        public LineBoundsControl(IHasBounds source, Transform target)
        {
            InitializeDraggable<LineDraggable>(source, target);
        }
    }

    public sealed class PlaneBoundsControl : AttachedBoundsControl
    {
        public PlaneBoundsControl(IHasBounds source, Transform target)
        {
            InitializeDraggable<PlaneDraggable>(source, target);
        }
    }

    public sealed class RotationBoundsControl : AttachedBoundsControl
    {
        public RotationBoundsControl(IHasBounds source, Transform target)
        {
            InitializeDraggable<RotationDraggable>(source, target);
        }
    }

    public sealed class FixedDistanceBoundsControl : AttachedBoundsControl
    {
        public FixedDistanceBoundsControl(IHasBounds source, Transform target)
        {
            var draggable = InitializeDraggable<FixedDistanceDraggable>(source, target);
            if (Settings.IsXR)
            {
                draggable.ForwardScale = 5f;
            }
        }
    }
}