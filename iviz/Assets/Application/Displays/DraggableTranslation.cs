using System;
using Iviz;
using Iviz.App;
using Iviz.App.Listeners;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Iviz.Displays
{
    public class DraggableTranslation : MonoBehaviour, IPointerDownHandler, IDraggable
    {
        public Vector3 line;
        public Transform ParentTransform { get; set; }
        
        bool needsStart;
        Vector3 startOffset;
        
        public event Action<Pose> Moved;

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
        
        static (float, float) ClosestPointDelta(in Ray ray, in Ray other)
        {
            Matrix4x4 m = Matrix4x4.identity;
            m.SetColumn(0, ray.direction);
            m.SetColumn(1, Vector3.Cross(ray.direction, other.direction));
            m.SetColumn(2, -other.direction);

            Matrix4x4 mInv = Matrix4x4.identity;
            Matrix4x4.Inverse3DAffine(m, ref mInv);
            Vector3 t = mInv * (other.origin - ray.origin);

            return (t.x, t.z);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TFListener.GuiManager.DraggedObject = this;
        }

        public void OnPointerMove(in Vector2 cursorPos)
        {
            Transform mTransform = transform;
            Transform mParent = ParentTransform;
            Ray ray = new Ray(mTransform.position, mParent.TransformDirection(line));
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
                mParent.position += deltaPosition - startOffset;
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