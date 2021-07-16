using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public sealed class DialogMoverWidget : MonoBehaviour, IWidget, IDragHandler, IEndDragHandler
    {
        [SerializeField] Transform targetTransform;
        [SerializeField] bool hideModuleList = true;
        bool isDragging;
        
        public event Action StartedDragging;
        
        void Awake()
        {
            if (targetTransform == null)
            {
                targetTransform = transform.parent;
            }
        }

        void IDragHandler.OnDrag([NotNull] PointerEventData eventData)
        {
            if (!isDragging)
            {
                StartedDragging?.Invoke();
                isDragging = true;
                if (hideModuleList)
                {
                    ModuleListPanel.Instance.DialogIsDragged = true;
                }
            }

            if (targetTransform != null)
            {
                targetTransform.position += (Vector3) eventData.delta;
            }
        }

        void IWidget.ClearSubscribers()
        {
        }

        void IEndDragHandler.OnEndDrag(PointerEventData _)
        {
            isDragging = false;
        }
    }
}