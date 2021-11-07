#nullable enable

using System;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class TrashButtonWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Button? button = null;
        [SerializeField] Image? image = null;

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
            Clicked?.Invoke();
        }

        public void ClearSubscribers()
        {
            Clicked = null;
        }

        public TrashButtonWidget SubscribeClicked(Action f)
        {
            Clicked += f;
            return this;
        }
    }
}