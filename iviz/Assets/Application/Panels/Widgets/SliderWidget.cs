#nullable enable

using System;
using System.Globalization;
using Iviz.Core;
using Iviz.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class SliderWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Slider? slider;
        [SerializeField] TMP_Text? text;
        [SerializeField] TMP_Text? valueText;

        Slider Slider => slider.AssertNotNull(nameof(slider));
        TMP_Text Text => text.AssertNotNull(nameof(text));
        TMP_Text ValueText => valueText.AssertNotNull(nameof(valueText));

        float max = 1;
        float min;
        int numberOfSteps = 99;
        bool integerOnly;
        
        public event Action<float>? ValueChanged;

        public string Label
        {
            get => Text.text;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                name = "Slider:" + value;
                Text.text = value;
            }
        }

        public float Value
        {
            get => Min + (Max - Min) * ValueInternal / NumberOfSteps;
            set
            {
                float oldValueInternal = ValueInternal;
                ValueInternal = (value - Min) / (Max - Min) * NumberOfSteps;
                if (Mathf.Approximately(ValueInternal, oldValueInternal))
                {
                    return;
                }

                UpdateLabel(value);
            }
        }

        float ValueInternal
        {
            get => Slider.value;
            set => Slider.SetValueWithoutNotify(Mathf.Max((int)value, 0));
        }

        public bool Interactable
        {
            get => Slider.interactable;
            set
            {
                Slider.interactable = value;
                Text.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
                ValueText.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
            }
        }

        public float Max
        {
            get => max;
            set
            {
                float v = Value;
                max = value;
                Value = v;
            }
        }

        public float Min
        {
            get => min;
            set
            {
                float v = Value;
                min = value;
                Value = v;
            }
        }
        
        public int NumberOfSteps
        {
            get => numberOfSteps;
            set
            {
                if (value == 0)
                {
                    ThrowHelper.ThrowArgumentOutOfRange(nameof(value));
                }
                
                float v = Value;

                numberOfSteps = value;
                Slider.minValue = 0;
                Slider.maxValue = NumberOfSteps;
                Value = v;
            }
        }
        
        public bool IntegerOnly
        {
            get => integerOnly;
            set
            {
                integerOnly = value;
                Max = (int)Max;
                Min = (int)Min;
                NumberOfSteps = (int)(Max - Min);
            }
        }

        void Awake()
        {
            Slider.wholeNumbers = true;
            NumberOfSteps = NumberOfSteps;
            Min = Min;
            Max = Max;
        }

        public void OnValueChanged(float f)
        {
            float v = Min + (Max - Min) * f / NumberOfSteps;
            UpdateLabel(v);
            ValueChanged?.Invoke(v);
        }

        void UpdateLabel(float v)
        {
            ValueText.text = v.ToString("0.###", CultureInfo.InvariantCulture);
        }

        public void ClearSubscribers()
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
            OnValueChanged(ValueInternal);
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

        public SliderWidget SetNumberOfSteps(int num)
        {
            NumberOfSteps = num;
            return this;
        }
    }
}