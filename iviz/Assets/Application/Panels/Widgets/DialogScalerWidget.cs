using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public sealed class DialogScalerWidget : MonoBehaviour, IWidget, IDragHandler, IEndDragHandler
    {
        [SerializeField] RectTransform targetTransform;
        [SerializeField] Vector2 minSize = DialogData.MinSize;
        public event Action ScaleChanged;
        
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
            targetTransform.sizeDelta = Vector2.Max(size, minSize);
            ScaleChanged?.Invoke();
            //Debug.Log(targetTransform.sizeDelta);
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