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
    public sealed class DraggableRotation : MonoBehaviour, 
        IPointerDownHandler, IPointerUpHandler, IDraggable
#if UNITY_WSA
        , IMixedRealityPointerHandler
#endif
    {
        public Vector3 normal;
        public Transform TargetTransform { get; set; }

        bool needsStart;
        Vector3 startIntersection;

        public event Action DoubleTap;
        public event InteractiveControl.MovedAction Moved;
        public event Action PointerDown;
        public event Action PointerUp;

        static (Vector3, float) PlaneIntersection(in Ray ray, in Ray other)
        {
            float t = Vector3.Dot(other.origin - ray.origin, ray.direction) / Vector3.Dot(-other.direction, ray.direction);
            Vector3 p = other.origin + t * other.direction;

            return (p, t);
        }

        
        public void OnPointerDown(PointerEventData eventData)
        {
            TFListener.GuiCamera.DraggedObject = this;
            PointerDown?.Invoke();
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public bool DoesRotationReset { get; set; }
        
        public void OnPointerMove(in Vector2 cursorPos) 
        {
            Ray pointerRay = TFListener.MainCamera.ScreenPointToRay(cursorPos);
            OnPointerMove(pointerRay);
        }

        void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = transform;
            Transform mTarget = TargetTransform;
            Transform mParent = mTransform.parent;
            Ray normalRay = new Ray(mTransform.position, mParent.TransformDirection(normal));

            (Vector3 intersection, float cameraDistance) = PlaneIntersection(normalRay, pointerRay);
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
                Quaternion q = Quaternion.AngleAxis(angle, mTarget.InverseTransformDirection(normalRay.direction));
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

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke();
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