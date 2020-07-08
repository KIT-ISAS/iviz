using Iviz.App;
using Iviz.App.Listeners;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Application.Displays
{
    public class DraggableRotation : MonoBehaviour, IPointerDownHandler, IDraggable
    {
        public Vector3 normal;

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

        public void OnPointerMove(in Vector2 cursorPos)
        {
            Transform mTransform = transform;
            Ray ray = new Ray(mTransform.position, mTransform.parent.TransformDirection(normal));
            Ray other = TFListener.MainCamera.ScreenPointToRay(cursorPos);

            (Vector3 intersection, float cameraDistance) = PlaneIntersection(ray, other);
            if (cameraDistance < 0)
            {
                return;
            }

            Vector3 localIntersection = mTransform.parent.InverseTransformPoint(intersection);
            if (needsStart)
            {
                startIntersection = localIntersection;
                needsStart = false;
            }
            else
            {
                Matrix4x4 M = Matrix4x4.identity;
                M.SetColumn(0, startIntersection);
                M.SetColumn(1, localIntersection);
                M.SetColumn(2, normal);

                float angle = Mathf.Asin(M.determinant) * Mathf.Rad2Deg; 

                Quaternion q = Quaternion.AngleAxis(angle, normal);
                mTransform.parent.transform.localRotation *= q;
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