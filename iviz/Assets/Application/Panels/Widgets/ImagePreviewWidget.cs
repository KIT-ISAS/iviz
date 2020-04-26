using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ImagePreviewWidget : Widget
    {
        public Text label;
        public RawImage image;

        public bool Interactable
        {
            get => false;
        }

        public string Label
        {
            get => label.text;
            set
            {
                name = "ImagePreview:" + value;
                label.text = value;
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

        public ImagePreviewWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }

        public ImagePreviewWidget SetMaterial(Material f)
        {
            Material = f;
            return this;
        }

        public override void ClearSubscribers()
        {
        }
    }
}