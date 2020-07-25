using UnityEngine;

namespace Iviz.App
{
    public class ToggleButtonWidget : TrashButtonWidget
    {
        public Sprite activeSprite;
        public Sprite inactiveSprite;

        bool state;
        public bool State
        {
            get => state;
            set
            {
                state = value;
                Sprite = state ? inactiveSprite : activeSprite;
            }
        }
    }
}