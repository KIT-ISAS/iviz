#nullable enable

using System;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class SimpleButtonWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Button? button;
        [SerializeField] Image? image;

        Button Button => button.AssertNotNull(nameof(button));
        Image Image => image.AssertNotNull(nameof(image));

        public bool Interactable
        {
            get => Button.interactable;
            set => Button.interactable = value;
        }

        public Sprite? Sprite
        {
            get => Image.sprite;
            set => Image.sprite = value;
        }

        public event Action? Clicked;

        public void OnClicked()
        {
            Clicked.TryRaise(this);
        }

        public void ClearSubscribers()
        {
            Clicked = null;
        }

        public SimpleButtonWidget SubscribeClicked(Action f)
        {
            Clicked += f;
            return this;
        }
    }
}