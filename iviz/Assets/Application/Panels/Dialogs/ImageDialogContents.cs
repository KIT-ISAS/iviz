using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Iviz.App
{
    public class ImageDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] Text text;
        [SerializeField] RawImage image;
        [SerializeField] TrashButtonWidget closeButton;

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

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            closeButton.ClearSubscribers();
        }
    }
}