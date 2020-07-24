using System;
using Iviz.App;
using Iviz.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Displays
{
    public sealed class DraggableTranslation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDraggable, IPointerClickHandler
    {
        public Vector3 line;
        public Transform TargetTransform { get; set; }
        
        bool needsStart;
        Vector3 startOffset;

        public event Action DoubleTap;
        public event InteractiveControl.MovedAction Moved;
        public event Action PointerDown;
        public event Action PointerUp;

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
            PointerDown?.Invoke();
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke();
        }

        public void OnPointerMove(in Vector2 cursorPos)
        {
            Transform mTransform = transform;
            Transform mParent = mTransform.parent;
            Transform mTarget = TargetTransform;
            Ray ray = new Ray(mTransform.position, mParent.TransformDirection(line));
            Ray other = TFListener.MainCamera.ScreenPointToRay(cursorPos);

            (float deltaDistance, float cameraDistance) = ClosestPointDelta(ray, other);
            //Debug.Log((deltaDistance, cameraDistance));
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
                mTarget.position += deltaPosition - startOffset;
                Moved?.Invoke(mTarget.AsPose());
            }
        }

        public void OnStartDragging()
        {
            needsStart = true;
        }

        public void OnEndDragging()
        {
        }

        static int GetClickCount(PointerEventData eventData)
        {
            return GuiCamera.IsMobile ? Input.GetTouch(0).tapCount : eventData.clickCount;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (GetClickCount(eventData) == 2)
            {
                DoubleTap?.Invoke();    
            }
        }
    }
}