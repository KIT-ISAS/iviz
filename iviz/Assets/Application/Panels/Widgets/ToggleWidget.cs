using System;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ToggleWidget : Widget
    {
        public Toggle toggle;
        public Text label;

        public string Label
        {
            get => label.text;
            set
            {
                name = "Toggle:" + value;
                label.text = value;
            }
        }
        public bool Value
        {
            get => toggle.isOn;
            set
            {
                toggle.isOn = value;
            }
        }

        public bool Interactable
        {
            get => toggle.interactable;
            set
            {
                toggle.interactable = value;
                label.color = value ? Display.EnabledFontColor : Display.DisabledFontColor;
            }
        }

        public event Action<bool> ValueChanged;

        public void OnValueChanged(bool f)
        {
            ValueChanged?.Invoke(f);
        }

        public override void ClearSubscribers()
        {
            ValueChanged = null;
        }

        public ToggleWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }

        public ToggleWidget SetValue(bool f)
        {
            Value = f;
            return this;
        }

        public ToggleWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        public ToggleWidget SubscribeValueChanged(Action<bool> f)
        {
            ValueChanged += f;
            return this;
        }
    }
}