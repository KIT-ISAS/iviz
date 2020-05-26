
using System;
using System.Globalization;
using Iviz.Resources;
using UnityEngine.UI;

namespace Iviz.App
{
    public class NumberInputFieldWidget : Widget
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
                name = "NumberInputField:" + value;
            }
        }

        public float Value
        {
            get
            {
                UnityUtils.TryParse(text.text, out float f);
                return f;
            }
            set
            {
                text.text = value.ToString(UnityUtils.Culture);
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
                label.color = value ? Resource.Colors.EnabledFontColor : Resource.Colors.DisabledFontColor;
            }
        }


        public event Action<float> ValueChanged;
        public event Action<float> EndEdit;

        void Awake()
        {
            Value = 0;
        }

        public void OnValueChanged(string text)
        {
            UnityUtils.TryParse(text, out float f);
            ValueChanged?.Invoke(f);
        }

        public void OnEndEdit(string text)
        {
            UnityUtils.TryParse(text, out float f);
            EndEdit?.Invoke(f);
        }

        public override void ClearSubscribers()
        {
            ValueChanged = null;
            EndEdit = null;
        }

        public NumberInputFieldWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        public NumberInputFieldWidget SetValue(float f)
        {
            Value = f;
            return this;
        }

        public NumberInputFieldWidget SetPlaceholder(string f)
        {
            Placeholder = f;
            return this;
        }

        public NumberInputFieldWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }

        public NumberInputFieldWidget SubscribeValueChanged(Action<float> f)
        {
            ValueChanged += f;
            return this;
        }

        public NumberInputFieldWidget SubscribeEndEdit(Action<float> f)
        {
            EndEdit += f;
            return this;
        }
    }
}