#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Displays
{
    public abstract class ScreenDraggable : MonoBehaviour, IScreenDraggable,
        IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] Transform? targetTransform;
        [SerializeField] Collider? rayCollider;

        Transform? mTransform;
        Vector3? referenceNormalLocal;

        protected Transform? interactorTransform;

        public bool IsHovering { get; private set; }
        public bool IsDragging { get; private set; }

        public abstract Quaternion BaseOrientation { set; }

        public float? Damping { get; set; } = 0.2f;

        protected float? DampingPerFrame => Damping is { } validatedDamping
            ? validatedDamping * (Time.deltaTime / 0.016f)
            : null;

        public Collider RayCollider
        {
            get => rayCollider.AssertNotNull(nameof(rayCollider));
            set => rayCollider = value != null ? value : throw new ArgumentNullException(nameof(value));
        }

        public event Action? Moved;
        public event Action? PointerDown;
        public event Action? PointerUp;
        public event Action? StartDragging;
        public event Action? EndDragging;
        public event Action? StateChanged;

        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        protected Vector3? ReferencePointLocal { get; private set; }

        public Vector3? ReferencePoint => ReferencePointLocal is { } validatedPointLocal
            ? Transform.TransformPoint(validatedPointLocal)
            : null;

        public Vector3? ReferenceNormal => referenceNormalLocal is { } validatedNormalLocal
            ? Transform.TransformDirection(validatedNormalLocal)
            : null;

        public Transform TargetTransform
        {
            protected get => targetTransform.CheckedNull() ?? Transform;
            set => targetTransform = value.CheckedNull() ?? throw new NullReferenceException(nameof(targetTransform));
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }


        protected Vector3 InitializeReferencePoint(in Ray pointerRay)
        {
            if (!RayCollider.TryIntersectRay(pointerRay, out var intersectionWorld, out var normalWorld))
            {
                return default; // shouldn't happen
            }

            ReferencePointLocal = Transform.InverseTransformPoint(intersectionWorld);
            referenceNormalLocal = Transform.InverseTransformDirection(normalWorld);
            return intersectionWorld;
        }

        void IScreenDraggable.OnStartDragging()
        {
            ReferencePointLocal = null;
            IsDragging = true;
            StartDragging?.Invoke();
            StateChanged?.Invoke();
        }

        void IScreenDraggable.OnEndDragging()
        {
            IsDragging = false;
            EndDragging?.Invoke();
            StateChanged?.Invoke();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData _)
        {
            interactorTransform = Settings.MainCameraTransform;
            StartSelected();
        }

        protected void StartSelected()
        {
            Settings.DragHandler.TrySetDraggedObject(this);
            PointerDown?.Invoke();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData _)
        {
            EndSelected();
            interactorTransform = null;
        }

        protected void EndSelected()
        {
            Settings.DragHandler.TryUnsetDraggedObject(this);
            PointerUp?.Invoke();
        }

        void IScreenDraggable.OnPointerMove(in Ray pointerRay)
        {
            OnPointerMove(pointerRay);
        }

        protected abstract void OnPointerMove(in Ray pointerRay);

        protected void RaiseMoved()
        {
            Moved?.Invoke();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData _)
        {
            IsHovering = true;
            StateChanged?.Invoke();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData _)
        {
            IsHovering = false;
            StateChanged?.Invoke();
        }

        void OnDisable()
        {
            Settings.DragHandler.TryUnsetDraggedObject(this);
            if (IsHovering || IsDragging)
            {
                IsHovering = false;
                IsDragging = false;
                StateChanged?.Invoke();
            }
        }

        public void ClearSubscribers()
        {
            Moved = null;
            PointerDown = null;
            PointerUp = null;
            StartDragging = null;
            EndDragging = null;
            StateChanged = null;
        }
    }

}