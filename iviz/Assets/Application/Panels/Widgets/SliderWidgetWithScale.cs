#nullable enable

using System;
using System.Globalization;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class SliderWidgetWithScale : MonoBehaviour, IWidget
    {
        const int MinPower = -4;
        const int MaxPower = 7;

        static float[]? powersOf10;

        static float[] PowersOf10 => powersOf10 ??= new[] // must match MinPower -> MaxPower!
        {
            1e-4f, 1e-3f, 1e-2f, 1e-1f, 1,
            1e1f, 1e2f, 1e3f, 1e4f, 1e5f, 1e6f, 1e7f
        };

        [SerializeField] Slider? slider;
        [SerializeField] TMP_Text? text;
        [SerializeField] TMP_Text? valueText;
        [SerializeField] Button? left;
        [SerializeField] Button? right;
        [SerializeField] Button? sign;
        [SerializeField] TMP_Text? scaleText;
        [SerializeField] TMP_Text? signText;

        bool isNegative;

        Slider Slider => slider.AssertNotNull(nameof(slider));
        TMP_Text Text => text.AssertNotNull(nameof(text));
        TMP_Text ValueText => valueText.AssertNotNull(nameof(valueText));
        TMP_Text ScaleText => scaleText.AssertNotNull(nameof(scaleText));
        TMP_Text SignText => signText.AssertNotNull(nameof(signText));
        Button Left => left.AssertNotNull(nameof(left));
        Button Right => right.AssertNotNull(nameof(right));
        Button Sign => sign.AssertNotNull(nameof(sign));

        public event Action<float>? ValueChanged;

        public string Label
        {
            set
            {
                Text.text = value ?? throw new ArgumentNullException(nameof(value));
                name = nameof(SliderWidgetWithScale) + ":" + value;
            }
        }

        int power;
        float scale = 1;

        public float Value
        {
            get => ValueInternal / 100 * scale * (isNegative ? -1 : 1);
            set
            {
                isNegative = value < 0;
                SignText.text = isNegative ? "[ -1 ]" : "[ +1 ]";

                float absValue = Mathf.Abs(value);
                if (absValue == 0)
                {
                    ValueInternal = 0;
                    UpdatePower(0);
                    UpdateLabel(0);
                    OnValueChanged();
                    return;
                }

                // ReSharper disable once NegativeIndex
                for (int i = MinPower; i < MaxPower; i++)
                {
                    if (absValue >= PowersOf10[i - MinPower])
                    {
                        continue;
                    }

                    UpdatePower(i);
                    ValueInternal = absValue * 100 / scale;
                    UpdateLabel(value);
                    OnValueChanged();
                    return;
                }

                // just set maximum value
                float newAbsValue = PowersOf10[MaxPower - MinPower];
                float newValue = value < 0 ? -newAbsValue : newAbsValue;
                UpdatePower(MaxPower);
                ValueInternal = newAbsValue * 100 / scale;
                UpdateLabel(newValue);
                OnValueChanged();
            }
        }

        float ValueInternal
        {
            get => isNegative ? (100 - Slider.value) : Slider.value;
            set
            {
                int valueToSet = Math.Max((int)value, 0);
                Slider.SetValueWithoutNotify(isNegative ? (100 - valueToSet) : valueToSet);
            }
        }

        public bool Interactable
        {
            set
            {
                Slider.interactable = value;
                Left.interactable = value;
                Right.interactable = value;
                Sign.interactable = value;

                var textColor = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
                Text.color = textColor;
                ValueText.color = textColor;
                ScaleText.color = textColor;
                SignText.color = textColor;
            }
        }

        void Awake()
        {
            Slider.onValueChanged.AddListener(_ => { OnValueChanged(); });
            Slider.GetComponent<EventTrigger>().triggers[0].callback.AddListener(_ =>
            {
                if (ValueInternal is (> 0 and < 10) or 100)
                {
                    Value = Value;
                }
            });
            Left.onClick.AddListener(() => UpdatePowerNoChange(power - 1));
            Right.onClick.AddListener(() => UpdatePowerNoChange(power + 1));
            Sign.onClick.AddListener(() =>
            {
                Value = -Value;
            });
            Value = 50;
        }

        void UpdatePowerNoChange(int newPower)
        {
            float currentValue = Value;
            UpdatePower(newPower);
            ValueInternal = Mathf.Abs(currentValue) * 100 / scale;
            OnValueChanged();
        }


        void UpdatePower(int newPower)
        {
            power = Mathf.Clamp(newPower, MinPower, MaxPower);
            scale = PowersOf10[power - MinPower];
            ScaleText.text = power <= 0 ? power.ToString() : "+" + power.ToString();
        }

        void OnValueChanged()
        {
            float v = Value;
            UpdateLabel(v);
            ValueChanged?.Invoke(v);
        }

        void UpdateLabel(float v)
        {
            ValueText.text = v.ToString("0.####", CultureInfo.InvariantCulture);
        }

        public void ClearSubscribers()
        {
            ValueChanged = null;
        }

        public SliderWidgetWithScale SetLabel(string f)
        {
            Label = f;
            return this;
        }

        public SliderWidgetWithScale SetValue(float f)
        {
            Value = f;
            return this;
        }

        public SliderWidgetWithScale UpdateValue()
        {
            OnValueChanged();
            return this;
        }

        public SliderWidgetWithScale SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        public SliderWidgetWithScale SubscribeValueChanged(Action<float> f)
        {
            ValueChanged += f;
            return this;
        }

        public SliderWidgetWithScale EnableNegative(bool f)
        {
            Sign.enabled = f;
            SignText.enabled = f;
            return this;
        }
    }
}