#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Iviz.Displays.Highlighters
{
    public class RotationWrapperBoundsControl : IBoundsControl
    {
        readonly MeshMarkerResource[] markers;
        readonly GameObject node;
        readonly BoxCollider collider;
        readonly RotationDraggable draggable;
        float sizeX;
        float sizeY;
        float markerScale;

        MeshMarkerResource? ring;

        public event Action? PointerDown;
        public event Action? PointerUp;
        public event Action? Moved;

        public bool Interactable
        {
            set => node.SetActive(value);
        }

        public float MarkerScale
        {
            set
            {
                markerScale = value;
                var scaleVector = new Vector3(value, 0.01f, value);
                foreach (var marker in markers)
                {
                    marker.Transform.localScale = scaleVector;
                }

                UpdateColliderSize();
            }
        }

        public float SizeX
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

        public float SizeY
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

        public RotationWrapperBoundsControl(Transform parent, Transform target, in Quaternion orientation)
        {
            node = new GameObject("[Rotation Wrapper]");

            var nodeTransform = node.transform;
            nodeTransform.SetParentLocal(parent);
            nodeTransform.localRotation = orientation;

            collider = node.AddComponent<BoxCollider>();

            draggable = node.AddComponent<RotationDraggable>();
            draggable.TargetTransform = target;
            draggable.Normal = Vector3.forward;
            draggable.RayCollider = collider;
            draggable.PointerDown += () => PointerDown?.Invoke();
            draggable.PointerUp += () => PointerUp?.Invoke();
            draggable.Moved += () => Moved?.Invoke();

            markers = new[]
            {
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cylinder, nodeTransform),
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cylinder, nodeTransform),
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cylinder, nodeTransform),
                ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cylinder, nodeTransform),
            };

            foreach (var marker in markers)
            {
                marker.Color = Color.white;
                marker.Layer = LayerType.Clickable;
                marker.ShadowsEnabled = false;
                marker.Transform.localRotation = Quaternion.AngleAxis(90, Vector3.left);
            }

            draggable.StateChanged += () =>
            {
                if (draggable.IsDragging || draggable.IsHovering)
                {
                    foreach (var marker in markers)
                    {
                        marker.EmissiveColor = draggable.IsDragging ? Color.blue : Color.black;
                        marker.Color = draggable.IsDragging ? Color.cyan : Color.white;
                    }
                }
                else
                {
                    foreach (var marker in markers)
                    {
                        marker.EmissiveColor = Color.black;
                        marker.Color = Color.white;
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
            MarkerScale = 0.25f;
        }

        void RentRing(Transform nodeTransform)
        {
            ring = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Ring, nodeTransform);
            ring.Layer = LayerType.IgnoreRaycast;

            float radius = Mathf.Sqrt(sizeX * sizeX + sizeY * sizeY) * 1.1f;
            ring.Transform.localScale = new Vector3(radius, radius, 0.001f);
            ring.EmissiveColor = Color.blue;
            ring.Color = Color.cyan.WithAlpha(0.25f);
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

        public void Stop()
        {
            foreach (var marker in markers)
            {
                marker.ReturnToPool(Resource.Displays.Cylinder);
            }

            draggable.ClearSubscribers();
            ring.ReturnToPool(Resource.Displays.Ring);
            Object.Destroy(node);

            PointerDown = null;
            PointerUp = null;
            Moved = null;
        }
    }
}