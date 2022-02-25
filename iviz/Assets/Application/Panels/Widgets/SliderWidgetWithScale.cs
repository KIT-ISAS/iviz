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
    public sealed class SliderWidgetWithScale : MonoBehaviour, IWidget
    {
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
            get => Text.text;
            set
            {
                Text.text = value ?? throw new ArgumentNullException(nameof(value));
                name = "SliderWithScale:" + value;
            }
        }

        int power;
        float scale = 1;

        public float Value
        {
            get => ValueInternal / 100 * scale * (isNegative ? -1 : 1);
            set
            {
                SignText.text = value < 0 ? "[-]" : "[+]";
                isNegative = value < 0;

                float absValue = Math.Abs(value);
                if (absValue == 0)
                {
                    ValueInternal = 0;
                    UpdatePower(0);
                    UpdateLabel(0);
                    OnValueChanged();
                    return;
                }

                float threshold = 1e-4f;
                for (int i = -4; i < 5; i++)
                {
                    if (absValue < threshold)
                    {
                        ValueInternal = absValue * 100 / scale;
                        UpdatePower(i);
                        UpdateLabel(value);
                        OnValueChanged();
                        break;
                    }

                    threshold *= 10;
                }

                /*
                switch (absValue)
                {
                    case 0:
                        ValueInternal = 0;
                        UpdatePower(0);
                        break;
                    case < 1e-3f:
                        //ValueInternal = absValue * (100 / 1e-3f);
                        UpdatePower(-3);
                        break;
                    case < 1e-2f:
                        //ValueInternal = absValue * (100 / 1e-2f);
                        UpdatePower(-2);
                        break;
                    case < 1e-1f:
                        //ValueInternal = absValue * (100 / 1e-1f);
                        UpdatePower(-1);
                        break;
                    case <= 1:
                        //ValueInternal = absValue * (100 / 1e-0f);
                        UpdatePower(0);
                        break;
                    case < 1e1f:
                        //ValueInternal = absValue * (100 * 1e-1f) ;
                        UpdatePower(1);
                        break;
                    case < 1e2f:
                        //ValueInternal = absValue * (100 * 1e-2f);
                        UpdatePower(2);
                        break;
                    case < 1e3f:
                        //ValueInternal = absValue * (100 * 1e-3f);
                        UpdatePower(3);
                        break;
                    case < 1e4f:
                        //ValueInternal = absValue * (100 * 1e-4f);
                        UpdatePower(4);
                        break;
                }
                */

            }
        }

        float ValueInternal
        {
            get => Slider.value;
            set => Slider.SetValueWithoutNotify(Math.Max((int)value, 0));
        }

        public bool Interactable
        {
            get => Slider.interactable;
            set
            {
                Slider.interactable = value;
                Left.interactable = value;
                Right.interactable = value;
                Text.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
                ValueText.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
                ScaleText.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
            }
        }

        void Awake()
        {
            Slider.onValueChanged.AddListener(_ => OnValueChanged());
            Left.onClick.AddListener(() => UpdatePowerNoChange(power - 1));
            Right.onClick.AddListener(() => UpdatePowerNoChange(power + 1));
            Sign.onClick.AddListener(() =>
            {
                isNegative = !isNegative;
                SignText.text = isNegative ? "[-]" : "[+]";
                UpdateValue();
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
            power = newPower switch
            {
                < -3 => -3,
                > 4 => 4,
                _ => newPower
            };

            scale = Mathf.Pow(10, power);
            ScaleText.text = power <= 0 ? power.ToString() : "+" + power;
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
    }
}