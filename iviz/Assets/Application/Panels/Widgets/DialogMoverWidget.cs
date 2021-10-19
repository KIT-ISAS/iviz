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
                isDragging = true;
                if (hideModuleList)
                {
                    ModuleListPanel.Instance.DialogPanelManager.DetachSelectedPanel();
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