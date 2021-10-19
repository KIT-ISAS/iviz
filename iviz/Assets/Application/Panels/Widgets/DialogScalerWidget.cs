using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;

namespace Iviz.App
{
    public sealed class DialogScalerWidget : MonoBehaviour, IWidget, IDragHandler, IEndDragHandler
    {
        [SerializeField] RectTransform targetTransform;

        void Awake()
        {
            if (targetTransform == null)
            {
                targetTransform = (RectTransform)transform.parent;
            }
        }

        void IDragHandler.OnDrag([NotNull] PointerEventData eventData)
        {
            if (targetTransform == null)
            {
                return;
            }
            
            float scale = 1f / ModuleListPanel.CanvasScale;
            var size = targetTransform.sizeDelta + Vector2.Scale(eventData.delta, new Vector2(scale, -scale));
            targetTransform.sizeDelta = Vector2.Max(size, DialogData.MinSize);
        }

        void IWidget.ClearSubscribers()
        {
        }

        void IEndDragHandler.OnEndDrag(PointerEventData _)
        {
        }
    }
}