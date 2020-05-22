using System;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class TrashButtonWidget : Widget
    {
        public Button button;
        public Image image;

        public bool Interactable
        {
            get => button.interactable;
            set
            {
                button.interactable = value;
            }
        }

        public Sprite Sprite
        {
            get => image.sprite;
            set
            {
                image.sprite = value;
            }
        }

        public event Action Clicked;

        public void OnClicked()
        {
            Clicked?.Invoke();
        }

        public override void ClearSubscribers()
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