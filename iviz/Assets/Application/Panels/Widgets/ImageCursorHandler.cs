#nullable enable

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public class ImageCursorHandler : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Vector2>? Clicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            var rectTransform = (RectTransform)transform;
            var clickPosition = rectTransform.InverseTransformPoint(eventData.position);
            var rect = rectTransform.rect;
            var eventPosition = new Vector2(
                (clickPosition.x - rect.x) / rect.width,
                1 - (clickPosition.y - rect.y) / rect.height);
            Clicked?.Invoke(eventPosition);
        }
    }
}