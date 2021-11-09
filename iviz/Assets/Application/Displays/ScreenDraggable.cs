#nullable enable

using System;
using Iviz.App;
using Iviz.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Displays
{
    public abstract class ScreenDraggable : MonoBehaviour, IScreenDraggable, IPointerDownHandler, IPointerUpHandler 
    {
        [SerializeField] Transform? targetTransform = null;
        Transform? mTransform;
        protected bool needsStart;

        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        public event Action? Moved;
        public event Action? PointerDown;
        public event Action? PointerUp;
        public event Action? StartDragging;
        public event Action? EndDragging;
    
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
            StartDragging?.Invoke();
        }

        void IScreenDraggable.OnEndDragging()
        {
            EndDragging?.Invoke();
        }
        
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            StartSelected();
        }

        protected void StartSelected()
        {
            if (ModuleListPanel.GuiInputModule != null)
            {
                ModuleListPanel.GuiInputModule.TrySetDraggedObject(this);
            }

            PointerDown?.Invoke();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            EndSelected();
        }

        protected void EndSelected()
        {
            if (ModuleListPanel.GuiInputModule != null)
            {
                ModuleListPanel.GuiInputModule.TryUnsetDraggedObject(this);
            }

            PointerUp?.Invoke();
        }
        
        void IScreenDraggable.OnPointerMove(in Ray pointerRay)
        {
            OnPointerMove(pointerRay);
        }
        
        void IScreenDraggable.OnPointerMove(in Vector2 cursorPos)
        {
            Ray pointerRay = Settings.MainCamera.ScreenPointToRay(cursorPos);
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
    }
}