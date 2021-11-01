using Iviz.Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ImageDialogContents : DetachablePanelContents
    {
        [SerializeField] Text text = null;
        [SerializeField] RawImage previewImage = null;
        [SerializeField] TrashButtonWidget closeButton = null;

        public Text Text => text;
        public RawImage PreviewImage => previewImage;
        public TrashButtonWidget CloseButton => closeButton;

        public string Title
        {
            get => text.text;
            set => text.text = value;
        }

        public Material Material
        {
            get => previewImage.material;
            set => previewImage.material = value;
        }

        Vector2Int imageSize;

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
            var rect = ((RectTransform)previewImage.transform).rect;
            float maxWidth = rect.width;
            float maxHeight = rect.height;

            if (maxWidth == 0 || maxHeight == 0)
            {
                previewImage.transform.localScale = Vector3.one;
                return;
            }

            if (imageSize.x == 0 || imageSize.y == 0)
            {
                previewImage.transform.localScale = Vector3.one;
                return;
            }

            float xScale = maxWidth / imageSize.x;
            float scaledY = imageSize.y * xScale;

            float yScale = maxHeight / imageSize.y;
            float scaledX = imageSize.x * yScale;

            previewImage.transform.localScale = scaledY < maxHeight
                ? Vector3.one.WithY(scaledY / maxHeight)
                : Vector3.one.WithX(scaledX / maxWidth);
        }

        void Awake()
        {
            AdjustSize();

            scalerWidget.ScaleChanged += AdjustSize;
        }
        
        public void ToggleImageEnabled()
        {
            previewImage.enabled = false;
            previewImage.enabled = true;
        }        

        public override void ClearSubscribers()
        {
            closeButton.ClearSubscribers();
            Material = null;
        }
    }
}