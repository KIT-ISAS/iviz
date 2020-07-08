using Iviz.App;
using Iviz.App.Listeners;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Application.Displays
{
    public class DraggableTranslation : MonoBehaviour, IPointerDownHandler, IDraggable
    {
        public Vector3 line;

        bool needsStart;
        Vector3 startOffset;
        
        static (float, float) ClosestPointDelta(in Ray ray, in Ray other)
        {
            Matrix4x4 M = Matrix4x4.identity;
            M.SetColumn(0, ray.direction);
            M.SetColumn(1, Vector3.Cross(ray.direction, other.direction));
            M.SetColumn(2, -other.direction);

            Vector3 t = M.inverse * (other.origin - ray.origin);

            return (t.x, t.z);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TFListener.GuiManager.DraggedObject = this;
        }

        public void OnPointerMove(in Vector2 cursorPos)
        {
            Transform mTransform = transform;
            Ray ray = new Ray(mTransform.position, mTransform.parent.TransformDirection(line));
            Ray other = TFListener.MainCamera.ScreenPointToRay(cursorPos);

            (float deltaDistance, float cameraDistance) = ClosestPointDelta(ray, other);
            if (cameraDistance < 0)
            {
                return;
            }
            deltaDistance = Mathf.Max(Mathf.Min(deltaDistance, 0.5f), -0.5f);
            Vector3 deltaPosition = deltaDistance * ray.direction;
            if (needsStart)
            {
                startOffset = deltaPosition;
                needsStart = false;
            }
            else
            {
                mTransform.parent.transform.position += deltaPosition - startOffset;
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