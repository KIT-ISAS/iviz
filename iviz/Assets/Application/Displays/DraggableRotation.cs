using System;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Displays
{
    public sealed class DraggableRotation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDraggable
    {
        public Vector3 normal;

        bool needsStart;
        Vector3 startIntersection;

        public bool DoesRotationReset { get; set; }
        public Transform TargetTransform { get; set; }

        public event MovedAction Moved;
        public event Action PointerDown;
        public event Action PointerUp;

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void OnPointerMove(in Vector2 cursorPos)
        {
            Ray pointerRay = Settings.MainCamera.ScreenPointToRay(cursorPos);
            OnPointerMove(pointerRay);
        }

        public void OnStartDragging()
        {
            needsStart = true;
        }

        public void OnEndDragging()
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (TfListener.GuiInputModule != null)
            {
                TfListener.GuiInputModule.DraggedObject = this;
            }

            PointerDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke();
        }

        static (Vector3, float) PlaneIntersection(in Ray ray, in Ray other)
        {
            float t = Vector3.Dot(other.origin - ray.origin, ray.direction) /
                      Vector3.Dot(-other.direction, ray.direction);
            Vector3 p = other.origin + t * other.direction;

            return (p, t);
        }

        void SetTargetPose(in Pose pose)
        {
            TargetTransform.SetPose(pose);
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
                m.SetColumn(0, startIntersection.Normalized());
                m.SetColumn(1, localIntersection.Normalized());
                m.SetColumn(2, normal);

                float angle = Mathf.Asin(m.determinant) * Mathf.Rad2Deg;

                //Debug.Log(startIntersection.normalized + " " + localIntersection.normalized + " " + angle);
                Quaternion q = Quaternion.AngleAxis(angle, mTarget.InverseTransformDirection(normalRay.direction));
                //mTarget.localRotation *= q;
                SetTargetPose(new Pose(mTarget.position, mTarget.rotation * q));
                Moved?.Invoke(mTarget.AsPose());

                //startIntersection = mTarget.transform.position + Quaternion.Inverse(q) * (localIntersection - mTarget.transform.position));
            }

            if (DoesRotationReset)
            {
                startIntersection = localIntersection;
            }
        }
    }
}