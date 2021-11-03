using System;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Displays
{
    public sealed class DraggableRotation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDraggable
    {
        [SerializeField] Vector3 normal;
        [SerializeField, CanBeNull] Transform targetTransform;

        bool needsStart;
        Vector3 startIntersection;

        public bool DoesRotationReset { get; set; }
        
        [CanBeNull]
        public Transform TargetTransform
        {
            get => targetTransform;
            set => targetTransform = value;
        }

        public event Action Moved;
        public event Action PointerDown;
        public event Action PointerUp;

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void OnPointerMove(in Vector2 cursorPos)
        {
            Ray pointerRay = Settings.MainCamera.ScreenPointToRay(cursorPos);
            OnPointerMove(pointerRay);
        }

        public void OnStartDragging()
        {
            needsStart = true;
        }

        public void OnEndDragging()
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (ModuleListPanel.GuiInputModule != null)
            {
                ModuleListPanel.GuiInputModule.TrySetDraggedObject(this);
            }

            PointerDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke();
        }

        static (Vector3, float) PlaneIntersection(in Ray ray, in Ray other)
        {
            float t = Vector3.Dot(other.origin - ray.origin, ray.direction) /
                      Vector3.Dot(-other.direction, ray.direction);
            Vector3 p = other.origin + t * other.direction;

            return (p, t);
        }

        void IDraggable.OnPointerMove(in Ray pointerRay)
        {
            OnPointerMove(pointerRay);
        }

        void OnPointerMove(in Ray pointerRay)
        {
            Transform ownTransform = transform;
            Transform parentTransform = ownTransform.parent;

            if (parentTransform == null)
            {
                Debug.LogWarning("The object with the DraggableRotation must have a parent!");
                return;
            }

            if (targetTransform == null)
            {
                Debug.LogWarning("Target transform of DraggableRotation must be nonnull!");
                return;
            }

            Ray normalRay = new Ray(ownTransform.position, parentTransform.TransformDirection(normal));

            (Vector3 intersection, float cameraDistance) = PlaneIntersection(normalRay, pointerRay);
            if (cameraDistance < 0)
            {
                return;
            }

            Vector3 localIntersection = parentTransform.InverseTransformPoint(intersection);
            if (needsStart)
            {
                needsStart = false;
                startIntersection = localIntersection;
            }
            else
            {
                Matrix4x4 m = Matrix4x4.identity;
                m.SetColumn(0, startIntersection.Normalized());
                m.SetColumn(1, localIntersection.Normalized());
                m.SetColumn(2, normal);

                float angle = Mathf.Asin(m.determinant) * Mathf.Rad2Deg;

                Quaternion q = Quaternion.AngleAxis(angle, targetTransform.InverseTransformDirection(normalRay.direction));
                targetTransform.SetPose(new Pose(targetTransform.position, targetTransform.rotation * q));
                Moved?.Invoke();
            }

            if (DoesRotationReset)
            {
                startIntersection = localIntersection;
            }
        }
    }
}