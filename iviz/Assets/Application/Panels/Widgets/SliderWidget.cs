using System;
using System.Globalization;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class SliderWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Slider slider = null;
        [SerializeField] Text label = null;
        [SerializeField] Text value = null;

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
            get => Min + (Max - Min) * ValueInternal / NumberOfSteps;
            set
            {
                ValueInternal = (value - Min) / (Max - Min) * NumberOfSteps;
                UpdateLabel(value);                
            }
        }

        float ValueInternal
        {
            get => slider.value;
            set => slider.SetValueWithoutNotify(Math.Max((int) value, 0));
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

        float max = 1;

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

        float min = 0;

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

        int numberOfSteps = 99;

        public int NumberOfSteps
        {
            get => numberOfSteps;
            set
            {
                float v = Value;

                numberOfSteps = value;
                slider.minValue = 0;
                slider.maxValue = NumberOfSteps;
                //Debug.Log(Label + " " + NumberOfSteps);
                Value = v;
            }
        }

        bool integerOnly;

        public bool IntegerOnly
        {
            get => integerOnly;
            set
            {
                integerOnly = value;
                Max = (int) Max;
                Min = (int) Min;
                NumberOfSteps = (int) (Max - Min);
            }
        }

        public event Action<float> ValueChanged;

        void Awake()
        {
            slider.wholeNumbers = true;
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
            value.text = v.ToString("0.###", CultureInfo.InvariantCulture);
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