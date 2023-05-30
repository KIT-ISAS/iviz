#nullable enable

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using static Iviz.Core.UnityUtils;

namespace Iviz.App
{
    public sealed class DialogScalerWidget : MonoBehaviour, IWidget, IDragHandler, IEndDragHandler
    {
        [SerializeField] RectTransform? targetTransform;
        [SerializeField] Vector2 minSize = DialogData.MinSize;
        public event Action? ScaleChanged;

        public RectTransform TargetTransform => targetTransform != null
            ? targetTransform
            : (targetTransform = (RectTransform)transform.parent);
        
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            float scale = 1f / ModuleListPanel.CanvasScale;
            var size = TargetTransform.sizeDelta + Vector2.Scale(eventData.delta, new Vector2(scale, -scale));
            TargetTransform.sizeDelta = Vector2.Max(size, minSize);
            ScaleChanged.TryRaise(this);
        }

        void IWidget.ClearSubscribers()
        {
            ScaleChanged = null;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData _)
        {
        }
    }
}