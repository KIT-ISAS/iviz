using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Iviz.App
{
    public class ImageDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] Text text = null;
        [SerializeField] RawImage image = null;
        [SerializeField] TrashButtonWidget closeButton = null;

        float maxWidth;
        float maxHeight;

        public TrashButtonWidget CloseButton => closeButton;

        public string Label
        {
            get => text.text;
            set
            {
                text.text = value;
            }
        }

        public Material Material
        {
            get => image.material;
            set
            {
                image.material = value;
            }
        }

        Vector2 imageSize;
        public Vector2 ImageSize
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
            float xscale = maxWidth / imageSize.x;
            float scaledY = imageSize.y * xscale;

            float yscale = maxHeight / imageSize.y;
            float scaledX = imageSize.x * yscale;

            if (scaledY < maxHeight)
            {
                float newYscale = scaledY / maxHeight;
                image.transform.localScale = new Vector3(1, newYscale, 1);
            }
            else
            {
                float newXscale = scaledX / maxWidth;
                image.transform.localScale = new Vector3(newXscale, 1, 1);
            }
        }

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void Awake()
        {
            var rect = ((RectTransform)image.transform).rect;
            maxWidth = rect.width;
            maxHeight = rect.height;
            AdjustSize();
        }

        public void ClearSubscribers()
        {
            closeButton.ClearSubscribers();
            Material = null;
        }
    }
}