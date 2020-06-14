using System;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ToggleWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Toggle toggle = null;
        [SerializeField] Text label = null;

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
                label.color = value ? Resource.Colors.EnabledFontColor : Resource.Colors.DisabledFontColor;
            }
        }

        public event Action<bool> ValueChanged;

        public void OnValueChanged(bool f)
        {
            ValueChanged?.Invoke(f);
        }

        public void ClearSubscribers()
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