#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ItemButton : MonoBehaviour
    {
        [SerializeField] Button? button;
        [FormerlySerializedAs("text")] [SerializeField] TMP_Text? label;
        
        RectTransform ButtonTransform => (RectTransform) transform;
        Button Button => button.AssertNotNull(nameof(button));
        TMP_Text Label => label.AssertNotNull(nameof(label));
        
        public event Action<int>? Clicked;

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

        public string Caption
        {
            get => Label.text;
            set => Label.text = value;
        }

        public bool Interactable
        {
            get => Button.interactable;
            set => Button.interactable = value;
        }
        
        public Color Color
        {
            get => Button.colors.normalColor;
            set
            {
                var block = Button.colors;
                block.normalColor = value;
                Button.colors = block;
            }
        }

        public float FontSize
        {
            get => Label.fontSize;
            set => Label.fontSize = value;
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