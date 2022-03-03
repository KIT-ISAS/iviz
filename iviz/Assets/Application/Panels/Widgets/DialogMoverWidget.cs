#nullable enable

using Iviz.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using IDragHandler = UnityEngine.EventSystems.IDragHandler;

namespace Iviz.App
{
    public sealed class DialogMoverWidget : MonoBehaviour, IWidget, IDragHandler, IEndDragHandler
    {
        [SerializeField] RectTransform? targetTransform;
        [SerializeField] bool dragCausesDetach = true;

        bool isDragging;

        RectTransform TargetTransform => targetTransform != null
            ? targetTransform
            : (targetTransform = (RectTransform)transform.parent);

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (!isDragging)
            {
                if (dragCausesDetach)
                {
                    ModuleListPanel.Instance.DialogPanelManager.DetachIfSelectedPanel(
                        TargetTransform.GetComponent<DialogPanel>());
                    if (Settings.IsXR)
                    {
                        return;
                    }
                }

                isDragging = true;
                TargetTransform.SetAsLastSibling();
            }

            TargetTransform.anchoredPosition += eventData.delta / ModuleListPanel.CanvasScale;
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