using System;
using Iviz.Displays;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ItemButton : MonoBehaviour
    {
        [SerializeField] Button button = null;
        [SerializeField] Text text = null;
        [NotNull] RectTransform ButtonTransform => (RectTransform) transform;

        public event Action<int> Clicked;

        public void RaiseClicked(int subIndex)
        {
            Clicked?.Invoke(subIndex);
        }

        public Vector2 AnchoredPosition
        {
            get => ButtonTransform.anchoredPosition;
            set => ButtonTransform.anchoredPosition = value;
        }

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public string Text
        {
            get => text.text;
            set => text.text = value;
        }

        public bool Interactable
        {
            get => button.interactable;
            set => button.interactable = value;
        }
        
        public Color Color
        {
            get => button.colors.normalColor;
            set
            {
                var block = button.colors;
                block.normalColor = value;
                button.colors = block;
            }
        }

        public int FontSize
        {
            get => text.fontSize;
            set => text.fontSize = value;
        }

        public float Height
        {
            get => ButtonTransform.sizeDelta.y;
            set => ButtonTransform.sizeDelta = new Vector2(ButtonTransform.sizeDelta.x, value);
        }

        public void Suspend()
        {
            Clicked = null;
            Interactable = true;
            Color = Color.white;
        }
    }
}