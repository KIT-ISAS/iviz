#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ImageDialogPanel : DetachableDialogPanel
    {
        [SerializeField] TMP_Text? text;
        [SerializeField] RawImage? previewImage;
        [SerializeField] TrashButtonWidget? closeButton;
        [SerializeField] ImageCursorHandler? cursorHandler;

        Vector2 startRect;
        Vector2Int imageSize;

        TMP_Text Text => text.AssertNotNull(nameof(text));
        RawImage PreviewImage => previewImage.AssertNotNull(nameof(previewImage));
        TrashButtonWidget CloseButton => closeButton.AssertNotNull(nameof(closeButton));
        ImageCursorHandler CursorHandler => cursorHandler.AssertNotNull(nameof(cursorHandler));

        public event Action? Closed;
        public event Action<Vector2>? Clicked;

        RectTransform Parent => (RectTransform)transform.parent;

        public string Title
        {
            get => Text.text;
            set => Text.text = value;
        }

        public Material? Material
        {
            get => PreviewImage.material;
            set => PreviewImage.material = value;
        }

        public Vector2Int ImageSize
        {
            get => imageSize;
            set
            {
                if (Mathf.Approximately(imageSize.x, value.x)
                    && Mathf.Approximately(imageSize.y, value.y))
                {
                    return;
                }

                imageSize = value;
                AdjustSize();
            }
        }
        
        void Awake()
        {
            startRect = Parent.sizeDelta;
            
            AdjustSize();

            CloseButton.Clicked += () => Closed?.Invoke();
            CursorHandler.Clicked += c => Clicked?.Invoke(c);
            ScalerWidget.ScaleChanged += AdjustSize;
        }        

        void AdjustSize()
        {
            var rect = ((RectTransform)PreviewImage.transform).rect;
            float maxWidth = rect.width;
            float maxHeight = rect.height;

            if (maxWidth == 0
                || maxHeight == 0
                || imageSize.x == 0
                || imageSize.y == 0)
            {
                PreviewImage.transform.localScale = Vector3.one;
                return;
            }

            float xScale = maxWidth / imageSize.x;
            float scaledY = imageSize.y * xScale;

            float yScale = maxHeight / imageSize.y;
            float scaledX = imageSize.x * yScale;

            PreviewImage.transform.localScale = scaledY < maxHeight
                ? Vector3.one.WithY(scaledY / maxHeight)
                : Vector3.one.WithX(scaledX / maxWidth);
        }

        public void ResetImageEnabled()
        {
            PreviewImage.enabled = false;
            PreviewImage.enabled = true;
        }

        public override void ClearSubscribers()
        {
            Closed = null;
            Clicked = null;
            Material = null;
            
            imageSize = default;

            Parent.sizeDelta = startRect;
        }
    }
}