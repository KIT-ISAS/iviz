using System;
using Iviz.App;
using Iviz.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_WSA
using Microsoft.MixedReality.Toolkit.Input;
#endif

namespace Iviz.Displays
{
    public sealed class DraggableTranslation : MonoBehaviour, 
        IPointerDownHandler, IPointerUpHandler, IDraggable, IPointerClickHandler
#if UNITY_WSA
        , IMixedRealityPointerHandler
#endif
    {
        [SerializeField] Vector3 line = default;
        public Transform TargetTransform { get; set; }
        
        bool needsStart;
        Vector3 startOffset;

        public event Action DoubleTap;
        public event InteractiveControl.MovedAction Moved;
        public event Action PointerDown;
        public event Action PointerUp;

        public Action<Pose> SetTargetPose { get; set; }
        
        void Awake()
        {
            SetTargetPose = pose => TargetTransform.SetPose(pose);            
        }
        
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
            TFListener.GuiCamera.DraggedObject = this;
            PointerDown?.Invoke();
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke();
        }

        public void OnPointerMove(in Vector2 cursorPos)
        {
            Ray pointerRay = TFListener.MainCamera.ScreenPointToRay(cursorPos);
            OnPointerMove(pointerRay);
        }

        void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = transform;
            Transform mParent = mTransform.parent;
            Transform mTarget = TargetTransform;
            Ray forwardRay = new Ray(mTransform.position, mParent.TransformDirection(line));

            (float deltaDistance, float cameraDistance) = ClosestPointDelta(forwardRay, pointerRay);
            //Debug.Log((deltaDistance, cameraDistance));
            if (cameraDistance < 0)
            {
                return;
            }
            deltaDistance = Mathf.Max(Mathf.Min(deltaDistance, 0.5f), -0.5f);
            Vector3 deltaPosition = deltaDistance * forwardRay.direction;
            if (needsStart)
            {
                startOffset = deltaPosition;
                needsStart = false;
            }
            else
            {
                //mTarget.position += deltaPosition - startOffset;
                SetTargetPose(new Pose(mTarget.position + deltaPosition - startOffset, mTarget.rotation));
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
            return Settings.IsMobile ? Input.GetTouch(0).tapCount : eventData.clickCount;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (GetClickCount(eventData) == 2)
            {
                DoubleTap?.Invoke();
            }
        }

#if UNITY_WSA
        public void OnPointerDown(MixedRealityPointerEventData _)
        {
            TFListener.GuiManager.DraggedObject = this;
            PointerDown?.Invoke();
        }

        public void OnPointerDragged(MixedRealityPointerEventData eventData)
        {
            Vector3 cameraPosition = TFListener.MainCamera.transform.position;
            Vector3 pointerPosition = ((GGVPointer)eventData.Pointer).Position;

            Ray pointerRay = new Ray(cameraPosition, pointerPosition - cameraPosition);
            OnPointerMove(pointerRay);
        }

        public void OnPointerUp(MixedRealityPointerEventData _)
        {
            PointerUp?.Invoke();
        }

        public void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            if (eventData.Count == 2)
            {
                DoubleTap?.Invoke();
            }
        }
#endif
    }
}