using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Iviz.App
{
    public sealed class DialogMoverWidget : MonoBehaviour, IWidget, IDragHandler, IEndDragHandler
    {
        [SerializeField] RectTransform targetTransform;
        [FormerlySerializedAs("hideModuleList")] [SerializeField] bool dragCausesDetach = true;
        bool isDragging;
        
        void Awake()
        {
            if (targetTransform == null)
            {
                targetTransform = (RectTransform)transform.parent;
            }
        }

        void IDragHandler.OnDrag([NotNull] PointerEventData eventData)
        {
            if (!isDragging)
            {
                if (dragCausesDetach)
                {
                    ModuleListPanel.Instance.DialogPanelManager.DetachSelectedPanel();
                    if (Settings.IsXR)
                    {
                        return;
                    }
                }
                isDragging = true;
            }

            if (targetTransform != null)
            {
                targetTransform.anchoredPosition += eventData.delta / ModuleListPanel.CanvasScale;
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