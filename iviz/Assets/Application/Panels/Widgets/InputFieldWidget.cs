using System;
using UnityEngine.UI;

namespace Iviz.App
{
    public class InputFieldWidget : Widget
    {
        public Text label;
        public InputField text;
        public Text placeholder;

        public string Label
        {
            get => label.text;
            set
            {
                label.text = value;
                name = "InputField:" + value;
            }
        }
        public string Value
        {
            get => text.text;
            set
            {
                text.text = value;
            }
        }

        public string Placeholder
        {
            get => placeholder.text;
            set
            {
                placeholder.text = value;
            }
        }

        public bool Interactable
        {
            get => text.interactable;
            set
            {
                text.interactable = value;
                label.color = value ? Display.EnabledFontColor : Display.DisabledFontColor;
            }
        }


        public event Action<string> ValueChanged;
        public event Action<string> EndEdit;

        public void OnValueChanged(string f)
        {
            ValueChanged?.Invoke(f);
        }

        public void OnEndEdit(string f)
        {
            EndEdit?.Invoke(f);
        }

        public override void ClearSubscribers()
        {
            ValueChanged = null;
            EndEdit = null;
        }

        public InputFieldWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        public InputFieldWidget SetValue(string f)
        {
            Value = f;
            return this;
        }

        public InputFieldWidget SetPlaceholder(string f)
        {
            Placeholder = f;
            return this;
        }

        public InputFieldWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }

        public InputFieldWidget SubscribeValueChanged(Action<string> f)
        {
            ValueChanged += f;
            return this;
        }

        public InputFieldWidget SubscribeEndEdit(Action<string> f)
        {
            EndEdit += f;
            return this;
        }
    }
}