using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Iviz.Core.UnityUtils;

namespace Iviz.App
{
    public sealed class TopButtonWidget : MonoBehaviour, IWidget, IDragHandler, IEndDragHandler
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
            Clicked.TryRaise(this);
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
                Dragged.TryRaise(this);
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