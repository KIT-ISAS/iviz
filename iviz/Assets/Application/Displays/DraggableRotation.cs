using System;
using Iviz;
using Iviz.App;
using Iviz.App.Listeners;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Iviz.Displays
{
    public sealed class DraggableRotation : MonoBehaviour, IPointerDownHandler, IDraggable
    {
        public Vector3 normal;
        public Transform TargetTransform { get; set; }

        bool needsStart;
        Vector3 startIntersection;
        
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

        public event InteractiveControl.MovedAction Moved;

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public bool DoesRotationReset { get; set; }
        
        public void OnPointerMove(in Vector2 cursorPos)
        {
            Transform mTransform = transform;
            Transform mTarget = TargetTransform;
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
                needsStart = false;
                startIntersection = localIntersection;
            }
            else
            {
                Matrix4x4 m = Matrix4x4.identity;
                m.SetColumn(0, startIntersection.normalized);
                m.SetColumn(1, localIntersection.normalized);
                m.SetColumn(2, normal);

                float angle = Mathf.Asin(m.determinant) * Mathf.Rad2Deg; 

                //Debug.Log(startIntersection.normalized + " " + localIntersection.normalized + " " + angle);
                Quaternion q = Quaternion.AngleAxis(angle, mTarget.InverseTransformDirection(ray.direction));
                mTarget.localRotation *= q;
                Moved?.Invoke(mTarget.AsPose());
                
                //startIntersection = mTarget.transform.position + Quaternion.Inverse(q) * (localIntersection - mTarget.transform.position));
            }

            if (DoesRotationReset)
            {
                startIntersection = localIntersection;
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