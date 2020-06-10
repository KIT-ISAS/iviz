using System;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ImagePreviewWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text label;
        [SerializeField] RawImage image;
        [SerializeField] Button button;

        public Action Clicked;

        void Awake()
        {
            button.onClick.AddListener(() => { Clicked?.Invoke(); });
        }

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

        public void ToggleImageEnabled()
        {
            image.enabled = false;
            image.enabled = true;
        }

        public void ClearSubscribers()
        {
            Clicked = null;
        }
    }
}