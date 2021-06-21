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
            Dragged?.Invoke();
            dragged = false;
        }

        void IDragHandler.OnDrag(PointerEventData _)
        {
            dragged = true;
        }
    }
}