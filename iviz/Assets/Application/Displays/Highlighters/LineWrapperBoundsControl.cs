#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays.Highlighters
{
    public class LineWrapperBoundsControl : IBoundsControl
    {
        static readonly Quaternion BaseRotationLeft = Quaternion.AngleAxis(-90, Vector3.up);
        static readonly Quaternion BaseRotationRight = Quaternion.AngleAxis(+90, Vector3.up);

        readonly MeshMarkerResource left;
        readonly MeshMarkerResource right;
        readonly GameObject node;
        readonly LineDraggable leftDraggable;
        readonly LineDraggable rightDraggable;

        public event Action? PointerDown;
        public event Action? PointerUp;
        public event Action? Moved;
        
        public float Width
        {
            set
            {
                leftDraggable.Transform.localPosition = (value / 2) * Vector3.back;
                rightDraggable.Transform.localPosition = (value / 2) * Vector3.forward;
            }
        }

        public bool Interactable
        {
            set
            {
                left.Visible = value;
                right.Visible = value;
            }
        }
        
        public LineWrapperBoundsControl(Transform parent, Transform target)
        {
            node = new GameObject("[Axis Wrapper]");
            node.transform.SetParentLocal(parent);

            left = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Pyramid, node.transform);
            left.Transform.localRotation = BaseRotationLeft;
            left.Color = Color.white;
            left.Layer = LayerType.Clickable;
            left.ShadowsEnabled = false;

            right = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Pyramid, node.transform);
            right.Transform.localRotation = BaseRotationRight;
            right.Color = Color.white;
            right.Layer = LayerType.Clickable;
            right.ShadowsEnabled = false;

            leftDraggable = left.gameObject.AddComponent<LineDraggable>();
            leftDraggable.TargetTransform = target;
            leftDraggable.Line = Vector3.left;
            leftDraggable.RayCollider = left.Collider;
            leftDraggable.PointerDown += () => PointerDown?.Invoke();
            leftDraggable.PointerUp += () => PointerUp?.Invoke();
            leftDraggable.Moved += () => Moved?.Invoke();            
            
            rightDraggable = right.gameObject.AddComponent<LineDraggable>();
            rightDraggable.TargetTransform = target;
            rightDraggable.Line = Vector3.left;
            rightDraggable.RayCollider = right.Collider;
            rightDraggable.PointerDown += () => PointerDown?.Invoke();
            rightDraggable.PointerUp += () => PointerUp?.Invoke();
            rightDraggable.Moved += () => Moved?.Invoke();        
            
            Width = 1;
            
            void OnStateChanged()
            {
                bool anyDragging = leftDraggable.IsDragging || rightDraggable.IsDragging;
                bool anyHovering = leftDraggable.IsHovering || rightDraggable.IsHovering;
                if (anyDragging || anyHovering)
                {
                    left.EmissiveColor = right.EmissiveColor = anyDragging ? Color.blue : Color.black;
                    left.Color = right.Color = anyDragging ? Color.cyan : Color.white;
                }
                else
                {
                    left.EmissiveColor = right.EmissiveColor = Color.black;
                    left.Color = right.Color = Color.white;
                }
            }

            leftDraggable.StateChanged += OnStateChanged;
            rightDraggable.StateChanged += OnStateChanged;
        }

        public void Stop()
        {
            leftDraggable.ClearSubscribers();
            rightDraggable.ClearSubscribers();
            UnityEngine.Object.Destroy(leftDraggable);
            UnityEngine.Object.Destroy(rightDraggable);
            
            left.ReturnToPool(Resource.Displays.Pyramid);
            right.ReturnToPool(Resource.Displays.Pyramid);
            
            UnityEngine.Object.Destroy(node.gameObject);
            
            PointerDown = null;
            PointerUp = null;
            Moved = null;
        }
        

    }



}