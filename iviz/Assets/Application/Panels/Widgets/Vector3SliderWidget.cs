using System;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class Vector3SliderWidget : MonoBehaviour, IWidget
    {
        [SerializeField] SliderWidget inputX = null;
        [SerializeField] SliderWidget inputY = null;
        [SerializeField] SliderWidget inputZ = null;
        [SerializeField] Text label = null;
        [SerializeField] Image panel = null;
        bool disableUpdates;

        public string Label
        {
            get => label.text;
            set
            {
                name = "Vector3SliderWidget:" + value;
                label.text = value;
            }
        }

        Vector3 value;
        public Vector3 Value
        {
            get => value;
            set
            {
                this.value = value;
                UpdateInputLabels();
            }
        }

        float range;
        public float Range
        {
            get => range;
            set
            {
                range = value;
                int numSteps = (int)(2 * range * 100);
                inputX.SetMinValue(-range).SetMaxValue(range).SetNumberOfSteps(numSteps);
                inputY.SetMinValue(-range).SetMaxValue(range).SetNumberOfSteps(numSteps);
                inputZ.SetMinValue(-range).SetMaxValue(range).SetNumberOfSteps(numSteps);
                
            }
        }

        public bool Interactable
        {
            get => inputX.Interactable;
            set
            {
                inputX.Interactable = value;
                inputY.Interactable = value;
                inputZ.Interactable = value;
                label.color = value ? Resource.Colors.EnabledFontColor : Resource.Colors.DisabledFontColor;
                panel.color = value ? Resource.Colors.EnabledPanelColor : Resource.Colors.DisabledPanelColor;
            }
        }

        public event Action<Vector3> ValueChanged;

        void OnValueChanged()
        {
            if (disableUpdates)
            {
                return;
            }
            value = new Vector3(
                inputX.Value,
                inputY.Value,
                inputZ.Value
                );
            ValueChanged?.Invoke(value);
        }


        void UpdateInputLabels()
        {
            disableUpdates = true;
            inputX.Value = value.x;
            inputY.Value = value.y;
            inputZ.Value = value.z;
            disableUpdates = false;
        }

        public void ClearSubscribers()
        {
            ValueChanged = null;
        }

        public Vector3SliderWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }

        public Vector3SliderWidget SetValue(Vector3 f)
        {
            Value = f;
            return this;
        }

        public Vector3SliderWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        public Vector3SliderWidget SubscribeValueChanged(Action<Vector3> f)
        {
            ValueChanged += f;
            return this;
        }
        
        public Vector3SliderWidget SetRange(float f)
        {
            Range = f;
            return this;
        }

        void Awake()
        {
            Interactable = true;
            inputX.SetNumberOfSteps(100);
            inputY.SetNumberOfSteps(100);
            inputZ.SetNumberOfSteps(100);
            Range = 0.5f;
            UpdateInputLabels();
            inputX.SubscribeValueChanged(_ => OnValueChanged());
            inputY.SubscribeValueChanged(_ => OnValueChanged());
            inputZ.SubscribeValueChanged(_ => OnValueChanged());
        }

        void Start()
        {
            OnValueChanged();
        }
    }
}