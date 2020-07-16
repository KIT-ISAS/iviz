using System;
using Iviz;
using Iviz.App;
using Iviz.App.Listeners;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Iviz.Displays
{
    public sealed class DraggablePlane : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDraggable
    {
        public Vector3 normal;
        public Transform TargetTransform { get; set; }
        
        bool needsStart;
        Vector3 startIntersection;

        public event Action DoubleTap;
        public event InteractiveControl.MovedAction Moved;
        public event Action PointerDown;
        public event Action PointerUp;

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
            PointerDown?.Invoke();
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke();
        }

        bool enabed = true;
        public void OnPointerMove(in Vector2 cursorPos)
        {
            Transform mTransform = transform;
            Transform mParent = mTransform.parent;
            Transform mTarget = TargetTransform;

            //Debug.Log(mParent.TransformDirection(normal));
            //Debug.Log(mParent.rotation);
            
            //Ray ray = new Ray(mTransform.position, mParent.TransformDirection(normal));/
            Ray ray = new Ray(mParent.position, mParent.TransformDirection(normal));
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
                /*
                Debug.Log("Parent: " + mParent.gameObject + " -> " + mParent.position + " " + mParent.rotation);
                //Debug.Log("Position was: " + mParent.position.x + " " + mParent.position.y + " " + mParent.position.z);
                Debug.Log("intersection: " + intersection.x + " " + intersection.y + " " + intersection.z);
                Debug.Log("localIntersection: " + localIntersection.x + " " + localIntersection.y + " " + localIntersection.z);
                Debug.Log("startIntersection: " + startIntersection.x + " " + startIntersection.y + " " + startIntersection.z);
                */
                Vector3 deltaPosition = localIntersection - startIntersection;
                float deltaDistance = deltaPosition.magnitude;
                if (deltaDistance > 0.5f)
                {
                    deltaPosition *= 0.5f / deltaDistance;
                }
                //Debug.Log("Moving by: " + deltaPosition.x + " " + deltaPosition.y + " " + deltaPosition.z);

                var vec3 = mParent.TransformVector(deltaPosition);
                //Debug.Log("Which transformed is: " + vec3.x + " " + vec3.y + " " + vec3.z);
                mTarget.position += vec3;

                //Debug.Log("New Position is: " + mParent.position.x + " " + mParent.position.y + " " + mParent.position.z);

                
                /*
                Ray newRay = new Ray(mParent.position, mParent.TransformDirection(normal));
                (Vector3 newIntersection, float _) = PlaneIntersection(newRay, other);
                Vector3 newlLocalIntersection = mParent.InverseTransformPoint(newIntersection);

                
                Debug.Log("New: Intersection: " + newIntersection.x + " " + newIntersection.y + " " + newIntersection.z);
                Debug.Log("New: LocalIntersection: " + newlLocalIntersection.x + " " + newlLocalIntersection.y + " " + newlLocalIntersection.z);
                */
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
    }
}