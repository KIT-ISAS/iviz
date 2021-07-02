using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App
{
    public class DraggableButtonWidget : MonoBehaviour, IWidget, IDragHandler, IEndDragHandler
    {
        [SerializeField] Button button;

        public event Action Clicked;
        public event Action Dragged;
        bool dragged;

        void Awake()
        {
            if (button == null)
            {
                button = GetComponent<Button>();
            }

            button.onClick.AddListener(OnClicked);
        }

        void OnClicked()
        {
            Clicked?.Invoke();
        }

        void IWidget.ClearSubscribers()
        {
            if (!dragged)
            {
                Clicked = null;
            }
        }
        
        void IEndDragHandler.OnEndDrag(PointerEventData _)
        {
            if (dragged)
            {
                Dragged?.Invoke();
                dragged = false;
            }
        }

        void IDragHandler.OnDrag([NotNull] PointerEventData pointerEventData)
        {
            if (pointerEventData.delta.x < 0)
            {
                dragged = true;
            }
        }
    }
}