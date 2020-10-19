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
    public sealed class DraggablePlane : MonoBehaviour, 
        IPointerDownHandler, IPointerUpHandler, IDraggable
#if UNITY_WSA
        , IMixedRealityPointerHandler
#endif
    {
        [SerializeField] Vector3 normal = default;
        public Transform TargetTransform { get; set; }
        
        bool needsStart;
        Vector3 startIntersection;

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

            //Ray ray = new Ray(mTransform.position, mParent.TransformDirection(normal));/
            Ray normalRay = new Ray(mParent.position, mParent.TransformDirection(normal));

            (Vector3 intersection, float cameraDistance) = PlaneIntersection(normalRay, pointerRay);
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
                float deltaDistance = deltaPosition.Magnitude();
                if (deltaDistance > 0.5f)
                {
                    deltaPosition *= 0.5f / deltaDistance;
                }

                Vector3 deltaPositionWorld = mParent.TransformVector(deltaPosition);
                SetTargetPose(new Pose(mTarget.position + deltaPositionWorld, mTarget.rotation));
                //mTarget.position += deltaPositionWorld;

                Moved?.Invoke(mTarget.AsPose());
                //startIntersection = localIntersection;
            }
        }

        public void OnStartDragging()
        {
            needsStart = true;
        }

        public void OnEndDragging()
        {
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