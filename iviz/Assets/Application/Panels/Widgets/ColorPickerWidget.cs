using System;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ColorPickerWidget : MonoBehaviour, IWidget
    {
        public SliderWidget sliderX;
        public SliderWidget sliderY;
        public SliderWidget sliderZ;
        public Text label;
        public Button button;
        public Image panel;
        Color color;

        enum ColorMode
        {
            RGB, HSV
        }
        ColorMode colorMode;
        bool disableUpdates;

        public string Label
        {
            get => label.text;
            set
            {
                name = "ColorPicker:" + value;
                label.text = value;
            }
        }
        public Color Value
        {
            get => color;
            set
            {
                color = value;
                UpdateSliderLabels();
                UpdateColorButton();
            }
        }

        public bool Interactable
        {
            get => sliderX.Interactable;
            set
            {
                sliderX.Interactable = value;
                sliderY.Interactable = value;
                sliderZ.Interactable = value;
                button.interactable = value;
                label.color = value ? Resource.Colors.EnabledFontColor : Resource.Colors.DisabledFontColor;
                panel.color = value ? Resource.Colors.EnabledPanelColor : Resource.Colors.DisabledPanelColor;
            }
        }

        public event Action<Color> ValueChanged;

        void OnValueChanged()
        {
            if (disableUpdates)
            {
                return;
            }
            switch (colorMode)
            {
                case ColorMode.RGB:
                    color = new Color(sliderX.Value, sliderY.Value, sliderZ.Value);
                    break;
                case ColorMode.HSV:
                    color = Color.HSVToRGB(sliderX.Value, sliderY.Value, sliderZ.Value);
                    break;
            }
            UpdateColorButton();
            ValueChanged?.Invoke(color);
        }

        void UpdateColorButton()
        {
            ColorBlock colorBlock = button.colors;
            colorBlock.highlightedColor = color;
            colorBlock.normalColor = color;
            colorBlock.selectedColor = color;
            colorBlock.pressedColor = color / 2;
            button.colors = colorBlock;
        }

        void UpdateSliderLabels()
        {
            disableUpdates = true;
            switch (colorMode)
            {
                case ColorMode.RGB:
                    sliderX.Value = color.r;
                    sliderY.Value = color.g;
                    sliderZ.Value = color.b;
                    break;
                case ColorMode.HSV:
                    float h, s, v;
                    Color.RGBToHSV(color, out h, out s, out v);
                    sliderX.Value = h;
                    sliderY.Value = s;
                    sliderZ.Value = v;
                    break;

            }
            disableUpdates = false;
        }

        void SwitchColorMode()
        {
            switch (colorMode)
            {
                case ColorMode.RGB:
                    colorMode = ColorMode.HSV;
                    sliderX.Label = "H";
                    sliderY.Label = "S";
                    sliderZ.Label = "V";
                    break;
                case ColorMode.HSV:
                    colorMode = ColorMode.RGB;
                    sliderX.Label = "R";
                    sliderY.Label = "G";
                    sliderZ.Label = "B";
                    break;
            }
            UpdateSliderLabels();
        }

        public void ClearSubscribers()
        {
            ValueChanged = null;
        }

        public ColorPickerWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }

        public ColorPickerWidget SetValue(Color f)
        {
            Value = f;
            return this;
        }

        public ColorPickerWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        public ColorPickerWidget SubscribeValueChanged(Action<Color> f)
        {
            ValueChanged += f;
            return this;
        }

        void Awake()
        {
            sliderX.SetMinValue(0).SetMaxValue(1).SubscribeValueChanged(f => OnValueChanged());
            sliderY.SetMinValue(0).SetMaxValue(1).SubscribeValueChanged(f => OnValueChanged());
            sliderZ.SetMinValue(0).SetMaxValue(1).SubscribeValueChanged(f => OnValueChanged());
            button.onClick.AddListener(SwitchColorMode);
        }

        void Start()
        {
            OnValueChanged();
        }
    }
}