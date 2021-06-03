using System;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Iviz.Displays
{
    public sealed class DraggablePlane : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDraggable
    {
        [SerializeField] Vector3 normal = new Vector3(0, 0, 1);

        public Vector3 Normal
        {
            get => normal;
            set => normal = value;
        }

        [SerializeField] Transform sourceTransform = null;

        Transform mTransform;
        [NotNull] public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        bool needsStart;
        Vector3 startIntersection;

        public Transform TargetTransform { get; set; }

        public event MovedAction Moved;
        public event Action PointerDown;
        public event Action PointerUp;
        public event Action StartDragging;
        public event Action EndDragging;

        void Awake()
        {
            if (TargetTransform == null)
            {
                TargetTransform = Transform;
            }
        }

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

        void IDraggable.OnStartDragging()
        {
            needsStart = true;
            StartDragging?.Invoke();
        }

        void IDraggable.OnEndDragging()
        {
            EndDragging?.Invoke();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            ModuleListPanel.GuiInputModule.TrySetDraggedObject(this);
            PointerDown?.Invoke();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke();
        }

        void SetTargetPose(in Pose pose)
        {
            TargetTransform.SetPose(pose);
        }

        static (Vector3, float) PlaneIntersection(in Ray ray, in Ray other)
        {
            float t = Vector3.Dot(other.origin - ray.origin, ray.direction) /
                      Vector3.Dot(-other.direction, ray.direction);
            Vector3 p = other.origin + t * other.direction;

            return (p, t);
        }

        void IDraggable.OnPointerMove(in Ray pointerRay)
        {
            OnPointerMove(pointerRay);
        }

        void OnPointerMove(in Ray pointerRay)
        {
            Transform mParent = sourceTransform.CheckedNull()
                                ?? Transform.parent.CheckedNull()
                                ?? Transform;
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
                if (deltaDistance > 0.5f) deltaPosition *= 0.75f / deltaDistance;

                Vector3 deltaPositionWorld = mParent.TransformVector(deltaPosition);
                SetTargetPose(new Pose(mTarget.position + deltaPositionWorld, mTarget.rotation));
                //mTarget.position += deltaPositionWorld;

                Moved?.Invoke(mTarget.AsPose());
                //startIntersection = localIntersection;
            }
        }
    }
}