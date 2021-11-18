#nullable enable

using System;
using Iviz.App;
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
        [SerializeField] Transform? targetTransform = null;
        Transform? mTransform;
        protected bool needsStart;
        protected Vector3 startIntersection;

        public bool IsHovering { get; private set; }
        public bool IsDragging { get; private set; }

        public event Action? Moved;
        public event Action? PointerDown;
        public event Action? PointerUp;
        public event Action? StartDragging;
        public event Action? EndDragging;
        public event Action? StateChanged;

        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        public Vector3 ReferencePoint => Transform.TransformPoint(startIntersection);
        
        public Transform TargetTransform
        {
            get => targetTransform.CheckedNull() ?? Transform;
            set => targetTransform = value.CheckedNull() ?? throw new NullReferenceException(nameof(targetTransform));
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void IScreenDraggable.OnStartDragging()
        {
            needsStart = true;
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
            StartSelected();
        }

        protected void StartSelected()
        {
            GuiInputModule.Instance.TrySetDraggedObject(this);
            PointerDown?.Invoke();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData _)
        {
            EndSelected();
        }

        protected void EndSelected()
        {
            GuiInputModule.Instance.TryUnsetDraggedObject(this);
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

        protected virtual void Awake()
        {
            if (targetTransform == null)
            {
                targetTransform = Transform;
            }
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
    }
}