using System;
using System.Globalization;
using UnityEngine.UI;

namespace Iviz.App
{
    public class SliderWidget : Widget
    {
        public Slider slider;
        public Text label;
        public Text value;

        public string Label
        {
            get => label.text;
            set
            {
                name = "Slider:" + value;
                label.text = value;
            }
        }
        public float Value
        {
            get => slider.value;
            set
            {
                slider.value = value;
            }
        }

        public bool Interactable
        {
            get => slider.interactable;
            set
            {
                slider.interactable = value;
                label.color = value ? Resource.Colors.EnabledFontColor : Resource.Colors.DisabledFontColor;
                this.value.color = value ? Resource.Colors.EnabledFontColor : Resource.Colors.DisabledFontColor;
            }
        }

        public float Max
        {
            get => slider.maxValue;
            set
            {
                slider.maxValue = value;
            }
        }

        public float Min
        {
            get => slider.minValue;
            set
            {
                slider.minValue = value;
            }
        }

        public bool IntegerOnly
        {
            get => slider.wholeNumbers;
            set
            {
                slider.wholeNumbers = value;
            }
        }

        public event Action<float> ValueChanged;

        public void OnValueChanged(float f)
        {
            value.text = f.ToString("0.###", CultureInfo.InvariantCulture);
            ValueChanged?.Invoke(f);
        }

        public override void ClearSubscribers()
        {
            ValueChanged = null;
        }

        public SliderWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }

        public SliderWidget SetValue(float f)
        {
            Value = f;
            return this;
        }

        public SliderWidget SetMinValue(float f)
        {
            Min = f;
            return this;
        }

        public SliderWidget SetMaxValue(float f)
        {
            Max = f;
            return this;
        }
        
        public SliderWidget UpdateValue()
        {
            OnValueChanged(Value);
            return this;
        }

        public SliderWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        public SliderWidget SetIntegerOnly(bool f)
        {
            IntegerOnly = f;
            return this;
        }

        public SliderWidget SubscribeValueChanged(Action<float> f)
        {
            ValueChanged += f;
            return this;
        }

    }
}