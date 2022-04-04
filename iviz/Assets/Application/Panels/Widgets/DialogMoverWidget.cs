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
        [SerializeField] RectTransform? layerTransform;
        [SerializeField] bool dragCausesDetach = true;

        bool isDragging;

        RectTransform TargetTransform => targetTransform != null
            ? targetTransform
            : (targetTransform = (RectTransform)transform.parent);

        RectTransform LayerTransform => layerTransform != null
            ? layerTransform
            : (layerTransform = TargetTransform);

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (!isDragging)
            {
                if (dragCausesDetach)
                {
                    ModuleListPanel.Instance.DialogPanelManager.DetachIfSelectedPanel(
                        TargetTransform.AssertHasComponent<DialogPanel>(nameof(TargetTransform)));
                    if (Settings.IsXR)
                    {
                        return;
                    }
                }

                isDragging = true;
                LayerTransform.SetAsLastSibling();
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