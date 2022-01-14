#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Iviz.Displays.Highlighters
{
    public sealed class RotationWrapperBoundsControl : WrapperBoundsControl
    {
        readonly MeshMarkerResource[] markers;
        readonly GameObject node;
        readonly BoxCollider collider;
        readonly RotationDraggable draggable;
        float sizeX;
        float sizeY;
        float markerScale;

        MeshMarkerResource? ring;

        public override bool Interactable
        {
            set => node.SetActive(value);
        }

        public override float MarkerScale
        {
            set
            {
                markerScale = value * 0.2f;
                var scaleVector = new Vector3(markerScale, 0.01f, markerScale);
                foreach (var marker in markers)
                {
                    marker.Transform.localScale = scaleVector;
                }

                UpdateColliderSize();
            }
        }

        float SizeX
        {
            set
            {
                if (Mathf.Approximately(sizeX, value))
                {
                    return;
                }

                sizeX = value;
                UpdateSize();
            }
        }

        float SizeY
        {
            set
            {
                if (Mathf.Approximately(sizeY, value))
                {
                    return;
                }

                sizeY = value;
                UpdateSize();
            }
        }

        public override Bounds Bounds
        {
            set
            {
                node.transform.localPosition = value.center;

                var rotation = node.transform.localRotation;
                var bounds = value.TransformBound(Pose.identity.WithRotation(rotation), Vector3.one);

                SizeX = bounds.size.x + markerScale * 0.707f;
                SizeY = bounds.size.y + markerScale * 0.707f;
            }
        }

        public RotationWrapperBoundsControl(Transform parent, Transform target)
        {
            node = new GameObject("[Rotation Wrapper]");
            node.layer = LayerType.Clickable;

            var nodeTransform = node.transform;
            nodeTransform.SetParentLocal(parent);

            collider = node.AddComponent<BoxCollider>();

            draggable = node.AddComponent<RotationDraggable>();
            draggable.TargetTransform = target;
            draggable.Normal = Vector3.forward;
            draggable.RayCollider = collider;
            RegisterDraggable(draggable);

            markers = new[]
            {
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cylinder, nodeTransform),
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cylinder, nodeTransform),
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cylinder, nodeTransform),
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cylinder, nodeTransform),
            };

            foreach (var marker in markers)
            {
                marker.Color = Resource.Colors.DraggableDefaultColor;
                marker.Layer = LayerType.Clickable;
                marker.ShadowsEnabled = false;
                marker.Transform.localRotation =
                    new Quaternion(-0.707106769f, 0, 0, 0.707106769f); // Quaternion.AngleAxis(90, Vector3.left);
            }

            draggable.StateChanged += () =>
            {
                if (draggable.IsDragging || draggable.IsHovering)
                {
                    foreach (var marker in markers)
                    {
                        marker.EmissiveColor = draggable.IsDragging
                            ? Resource.Colors.DraggableSelectedEmissive
                            : Color.black;
                        marker.Color = draggable.IsDragging
                            ? Resource.Colors.DraggableSelectedColor
                            : Resource.Colors.DraggableHoverColor;
                    }
                }
                else
                {
                    foreach (var marker in markers)
                    {
                        marker.EmissiveColor = Color.black;
                        marker.Color = Resource.Colors.DraggableDefaultColor;
                    }
                }

                if (draggable.IsDragging)
                {
                    if (ring == null)
                    {
                        RentRing(nodeTransform);
                    }
                }
                else if (ring != null)
                {
                    ring.ReturnToPool(Resource.Displays.Ring);
                    ring = null;
                }
            };

            SizeX = 1;
            SizeY = 1;
            MarkerScale = 1;
        }

        public override Quaternion BaseOrientation
        {
            set => node.transform.localRotation = value;
        }

        void RentRing(Transform nodeTransform)
        {
            ring = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Ring, nodeTransform);
            ring.Layer = LayerType.IgnoreRaycast;

            float radius = Mathf.Sqrt(sizeX * sizeX + sizeY * sizeY) * 1.1f;
            ring.Transform.localScale = new Vector3(radius, radius, 0.001f);
            ring.EmissiveColor = Resource.Colors.DraggableSelectedEmissive;
            ring.Color = Resource.Colors.DraggableSelectedColor.WithAlpha(0.25f);
            ring.ShadowsEnabled = false;
        }

        void UpdateSize()
        {
            float halfSizeX = sizeX / 2;
            float halfSizeY = sizeY / 2;
            markers[0].Transform.localPosition = new Vector3(-halfSizeX, halfSizeY, 0);
            markers[1].Transform.localPosition = new Vector3(halfSizeX, halfSizeY, 0);
            markers[2].Transform.localPosition = new Vector3(halfSizeX, -halfSizeY, 0);
            markers[3].Transform.localPosition = new Vector3(-halfSizeX, -halfSizeY, 0);
            UpdateColliderSize();
        }

        void UpdateColliderSize()
        {
            collider.size = new Vector3(sizeX + markerScale, sizeY + markerScale, 0.01f);
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (var marker in markers)
            {
                marker.ReturnToPool(Resource.Displays.Cylinder);
            }

            draggable.ClearSubscribers();
            ring.ReturnToPool(Resource.Displays.Ring);
            Object.Destroy(node);
        }
    }
}