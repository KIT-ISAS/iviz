using System;
using Iviz;
using Iviz.App;
using Iviz.App.Listeners;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Application.Displays
{
    public class DraggablePlane : MonoBehaviour, IPointerDownHandler, IDraggable
    {
        public Vector3 normal;

        bool needsStart;
        Vector3 startIntersection;
        
        public event Action<Pose> Moved;

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
        
        static (Vector3, float) PlaneIntersection(in Ray ray, in Ray other)
        {
            float t = Vector3.Dot(other.origin - ray.origin, ray.direction) / Vector3.Dot(-other.direction, ray.direction);
            Vector3 p = other.origin + t * other.direction;

            return (p, t);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TFListener.GuiManager.DraggedObject = this;
        }

        public void OnPointerMove(in Vector2 cursorPos)
        {
            Transform mTransform = transform;
            Transform mParent = mTransform.parent;

            Ray ray = new Ray(mTransform.position, mParent.TransformDirection(normal));
            Ray other = TFListener.MainCamera.ScreenPointToRay(cursorPos);

            (Vector3 intersection, float cameraDistance) = PlaneIntersection(ray, other);
            if (cameraDistance < 0)
            {
                return;
            }

            Vector3 localIntersection = mParent.InverseTransformPoint(intersection);
            if (needsStart)
            {
                startIntersection = localIntersection;
                needsStart = false;
            }
            else
            {
                Vector3 deltaPosition = localIntersection - startIntersection;
                float deltaDistance = deltaPosition.magnitude;
                if (deltaDistance > 0.5f)
                {
                    deltaPosition *= 0.5f / deltaDistance;
                }
                mParent.position += mParent.TransformVector(deltaPosition);
                Moved?.Invoke(mParent.AsPose());
            }
        }

        public void OnStartDragging()
        {
            needsStart = true;
        }

        public void OnEndDragging()
        {
        }
    }
}