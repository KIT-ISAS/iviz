using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ToggleButtonWidget : TrashButtonWidget
    {
        [SerializeField] Sprite activeSprite;
        [SerializeField] Sprite inactiveSprite;

        [SerializeField] string activeText = ""; 
        [SerializeField] string inactiveText = "";
        [SerializeField] Text text = null;

        public Sprite ActiveSprite
        {
            get => activeSprite;
            set => activeSprite = value;
        }

        public Sprite InactiveSprite
        {
            get => inactiveSprite;
            set => inactiveSprite = value;
        }

        [NotNull]
        public string ActiveText
        {
            get => activeText;
            set => activeText = value ?? throw new ArgumentNullException(nameof(value));
        }

        [NotNull]
        public string InactiveText
        {
            get => inactiveText;
            set => inactiveText = value ?? throw new ArgumentNullException(nameof(value));
        }

        bool state;
        public bool State
        {
            get => state;
            set
            {
                state = value;
                Sprite = state ? activeSprite : inactiveSprite;
                if (text != null)
                {
                    text.text = state ? activeText : inactiveText;
                }
            }
        }
    }
}