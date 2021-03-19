using System;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Displays
{
    public sealed class DraggableTranslation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDraggable
    {
        [SerializeField] Vector3 line = new Vector3(0, 0, 1);

        bool needsStart;
        Vector3 startOffset;
        
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

        void SetTargetPose(in Pose pose)
        {
            TargetTransform.SetPose(pose);
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
                SetTargetPose(new Pose(mTarget.position + deltaPosition - startOffset, mTarget.rotation));
                Moved?.Invoke(mTarget.AsPose());
            }
        }
    }
}