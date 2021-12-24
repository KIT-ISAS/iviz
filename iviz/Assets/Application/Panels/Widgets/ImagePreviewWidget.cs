using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ImagePreviewWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text label = null;
        [SerializeField] RawImage image = null;
        [SerializeField] Button button = null;

        public Action Clicked { get; set; }

        void Awake()
        {
            button.onClick.AddListener(() => Clicked?.Invoke());
        }

        bool interactable = true;
        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                image.enabled = value;
                image.color = value ? Color.white : Color.black;
                button.interactable = value;
                button.image.raycastTarget = value;
            }
        }

        [NotNull]
        public string Label
        {
            get => label.text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                
                name = "ImagePreview:" + value;
                label.text = value;
            }
        }

        public Material Material
        {
            get => image.material;
            set => image.material = value;
        }

        [NotNull]
        public ImagePreviewWidget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }

        [NotNull]
        public ImagePreviewWidget SetMaterial(Material f)
        {
            Material = f;
            return this;
        }

        public void UpdateMaterial()
        {
            image.enabled = false;
            image.enabled = true;
        }
        
        public void SetMaterialDirty()
        {
            image.SetMaterialDirty();
        }

        public void ClearSubscribers()
        {
            Clicked = null;
        }
    }
}