#nullable enable

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ToggleButtonWidget : SimpleButtonWidget
    {
        [SerializeField] Sprite? activeSprite;
        [SerializeField] Sprite? inactiveSprite;

        [SerializeField] string activeText = ""; 
        [SerializeField] string inactiveText = "";
        [SerializeField] Text? text;

        bool state;

        /*
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
        */

        public string ActiveText
        {
            get => activeText;
            set => activeText = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string InactiveText
        {
            get => inactiveText;
            set => inactiveText = value ?? throw new ArgumentNullException(nameof(value));
        }

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