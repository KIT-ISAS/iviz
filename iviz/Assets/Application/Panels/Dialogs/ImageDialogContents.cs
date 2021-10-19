using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ImageDialogContents : PanelContents
    {
        [SerializeField] Text text = null;
        [SerializeField] RawImage image = null;
        [SerializeField] TrashButtonWidget closeButton = null;

        public Text Text => text;
        public RawImage Image => image;
        public TrashButtonWidget CloseButton => closeButton;

        float maxWidth;
        float maxHeight;

        public string Label
        {
            get => text.text;
            set => text.text = value;
        }

        public Material Material
        {
            get => image.material;
            set => image.material = value;
        }

        Vector2Int imageSize;
        public Vector2Int ImageSize
        {
            get => imageSize;
            set
            {
                imageSize = value;
                AdjustSize();
            }
        }

        void AdjustSize()
        {
            if (maxWidth == 0 || maxHeight == 0)
            {
                image.transform.localScale = Vector3.one;
                return;
            }
            if (imageSize.x == 0 || imageSize.y == 0)
            {
                image.transform.localScale = Vector3.one;
                return;
            }
            float xScale = maxWidth / imageSize.x;
            float scaledY = imageSize.y * xScale;

            float yScale = maxHeight / imageSize.y;
            float scaledX = imageSize.x * yScale;

            if (scaledY < maxHeight)
            {
                float newYScale = scaledY / maxHeight;
                image.transform.localScale = new Vector3(1, newYScale, 1);
            }
            else
            {
                float newXScale = scaledX / maxWidth;
                image.transform.localScale = new Vector3(newXScale, 1, 1);
            }
        }

        void Awake()
        {
            var rect = ((RectTransform)image.transform).rect;
            maxWidth = rect.width;
            maxHeight = rect.height;
            AdjustSize();
        }

        public override void ClearSubscribers()
        {
            closeButton.ClearSubscribers();
            Material = null;
        }
    }
}