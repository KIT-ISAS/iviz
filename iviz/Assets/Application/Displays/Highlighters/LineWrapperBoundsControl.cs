#nullable enable

using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays.Highlighters
{
    public sealed class LineWrapperBoundsControl : WrapperBoundsControl
    {
        static readonly Quaternion BaseRotationLeft = Quaternion.AngleAxis(-90, Vector3.up);
        static readonly Quaternion BaseRotationRight = Quaternion.AngleAxis(+90, Vector3.up);

        readonly MeshMarkerResource left;
        readonly MeshMarkerResource right;
        readonly GameObject node;
        readonly LineDraggable leftDraggable;
        readonly LineDraggable rightDraggable;
        
        float SizeZ
        {
            set
            {
                left.Transform.localPosition = (value / 2) * Vector3.back;
                right.Transform.localPosition = (value / 2) * Vector3.forward;
            }
        }
        
        public override float MarkerScale
        {
            set
            {
                left.Transform.localScale = value * Vector3.one;
                right.Transform.localScale = value * Vector3.one;
            }
        }        

        public override bool Interactable
        {
            set
            {
                left.Visible = value;
                right.Visible = value;
            }
        }
        
        public override Bounds Bounds
        {
            set
            {
                node.transform.localPosition = value.center;

                var rotation = node.transform.localRotation;
                var bounds = value.TransformBound(Pose.identity.WithRotation(rotation), Vector3.one);

                SizeZ = bounds.size.z;   
            }
        }        
        
        public LineWrapperBoundsControl(Transform parent, Transform target, in Quaternion orientation)
        {
            node = new GameObject("[Line Wrapper]");
            node.transform.SetParentLocal(parent);
            node.transform.localRotation = orientation;

            left = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Pyramid, node.transform);
            left.Transform.localRotation = BaseRotationLeft;
            left.Color = Color.grey;
            left.Layer = LayerType.Clickable;
            left.ShadowsEnabled = false;

            right = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Pyramid, node.transform);
            right.Transform.localRotation = BaseRotationRight;
            right.Color = Color.grey;
            right.Layer = LayerType.Clickable;
            right.ShadowsEnabled = false;

            leftDraggable = left.gameObject.AddComponent<LineDraggable>();
            leftDraggable.TargetTransform = target;
            leftDraggable.Line = Vector3.left;
            leftDraggable.RayCollider = left.Collider;
            
            rightDraggable = right.gameObject.AddComponent<LineDraggable>();
            rightDraggable.TargetTransform = target;
            rightDraggable.Line = Vector3.left;
            rightDraggable.RayCollider = right.Collider;

            RegisterDraggable(leftDraggable);
            RegisterDraggable(rightDraggable);
            
            SizeZ = 1;
            
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
                    left.Color = right.Color = Color.grey;
                }
            }

            leftDraggable.StateChanged += OnStateChanged;
            rightDraggable.StateChanged += OnStateChanged;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            leftDraggable.ClearSubscribers();
            rightDraggable.ClearSubscribers();
            UnityEngine.Object.Destroy(leftDraggable);
            UnityEngine.Object.Destroy(rightDraggable);
            
            left.ReturnToPool(Resource.Displays.Pyramid);
            right.ReturnToPool(Resource.Displays.Pyramid);
            
            UnityEngine.Object.Destroy(node.gameObject);
        }
    }
}