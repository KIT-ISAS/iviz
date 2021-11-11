#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ImageDialogContents : DetachablePanelContents
    {
        [SerializeField] TMP_Text? text = null;
        [SerializeField] RawImage? previewImage = null;
        [SerializeField] TrashButtonWidget? closeButton = null;

        Vector2Int imageSize;

        TMP_Text Text => text.AssertNotNull(nameof(text));
        RawImage PreviewImage => previewImage.AssertNotNull(nameof(previewImage));
        TrashButtonWidget CloseButton => closeButton.AssertNotNull(nameof(closeButton));

        public event Action? Closed;
        
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

        void AdjustSize()
        {
            var rect = ((RectTransform)PreviewImage.transform).rect;
            float maxWidth = rect.width;
            float maxHeight = rect.height;

            if (maxWidth == 0 || maxHeight == 0)
            {
                PreviewImage.transform.localScale = Vector3.one;
                return;
            }

            if (imageSize.x == 0 || imageSize.y == 0)
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

        void Awake()
        {
            AdjustSize();

            CloseButton.Clicked += () => Closed?.Invoke();
            ScalerWidget.ScaleChanged += AdjustSize;
        }
        
        public void ToggleImageEnabled()
        {
            PreviewImage.enabled = false;
            PreviewImage.enabled = true;
        }        

        public override void ClearSubscribers()
        {
            Closed = null;
            Material = null;
        }
    }
}