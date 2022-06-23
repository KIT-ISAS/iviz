#nullable enable

using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.Highlighters
{
    public sealed class LineWrapperBoundsControl : WrapperBoundsControl
    {
        readonly MeshMarkerDisplay left;
        readonly MeshMarkerDisplay right;
        readonly GameObject node;
        readonly LineDraggable leftDraggable;
        readonly LineDraggable rightDraggable;

        bool interactable = true;
        bool visible = true;

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
                interactable = value;
                left.Visible = interactable && visible;
                right.Visible = interactable && visible;
            }
        }

        public override bool Visible
        {
            set
            {
                visible = value;
                left.Visible = interactable && visible;
                right.Visible = interactable && visible;
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

        public LineWrapperBoundsControl(Transform parent, Transform target)
        {
            node = new GameObject("[Line Wrapper]");
            node.transform.SetParentLocal(parent);

            var baseRotationLeft = Quaternions.Rotate270AroundY;
            left = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Pyramid, node.transform);
            left.Transform.localRotation = baseRotationLeft;
            left.Color = Resource.Colors.DraggableDefaultColor;
            left.Layer = LayerType.Clickable;
            left.EnableShadows = false;

            var baseRotationRight = Quaternions.Rotate90AroundY;
            right = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Pyramid, node.transform);
            right.Transform.localRotation = baseRotationRight;
            right.Color = Resource.Colors.DraggableDefaultColor;
            right.Layer = LayerType.Clickable;
            right.EnableShadows = false;

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
                    var emissiveColor = anyDragging
                        ? Resource.Colors.DraggableSelectedEmissive
                        : Color.black;
                    left.EmissiveColor = emissiveColor;
                    right.EmissiveColor = emissiveColor;

                    var diffuseColor = anyDragging
                        ? Resource.Colors.DraggableSelectedColor
                        : Resource.Colors.DraggableHoverColor;
                    left.Color = diffuseColor;
                    right.Color = diffuseColor;
                }
                else
                {
                    var emissiveColor = Color.black;
                    left.EmissiveColor = emissiveColor;
                    right.EmissiveColor = emissiveColor;

                    var diffuseColor = Resource.Colors.DraggableDefaultColor;
                    left.Color = diffuseColor;
                    right.Color = diffuseColor;
                }
            }

            leftDraggable.StateChanged += OnStateChanged;
            rightDraggable.StateChanged += OnStateChanged;
        }

        public override Quaternion BaseOrientation
        {
            set => node.transform.localRotation = value;
        }

        public override void Dispose()
        {
            base.Dispose();

            leftDraggable.ClearSubscribers();
            rightDraggable.ClearSubscribers();
            Object.Destroy(leftDraggable);
            Object.Destroy(rightDraggable);

            left.ReturnToPool(Resource.Displays.Pyramid);
            right.ReturnToPool(Resource.Displays.Pyramid);

            Object.Destroy(node.gameObject);
        }
    }
}