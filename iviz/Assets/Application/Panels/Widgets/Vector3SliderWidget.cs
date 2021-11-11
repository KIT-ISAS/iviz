using System;
using Iviz.Resources;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class Vector3SliderWidget : MonoBehaviour, IWidget
    {
        [SerializeField] SliderWidget inputX = null;
        [SerializeField] SliderWidget inputY = null;
        [SerializeField] SliderWidget inputZ = null;
        [SerializeField] TMP_Text label = null;
        [SerializeField] Image panel = null;
        //bool disableUpdates;

        [NotNull]
        public string Label
        {
            get => label.text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                
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
                int numSteps = (int) (2 * range * 100);
                inputX.SetMinValue(mean.x - range).SetMaxValue(mean.x + range).SetNumberOfSteps(numSteps);
                inputY.SetMinValue(mean.y - range).SetMaxValue(mean.y + range).SetNumberOfSteps(numSteps);
                inputZ.SetMinValue(mean.z - range).SetMaxValue(mean.z + range).SetNumberOfSteps(numSteps);
            }
        }

        Vector3 mean;
        public Vector3 Mean
        {
            get => mean;
            set
            {
                //disableUpdates = true;
                mean = value;
                inputX.SetMinValue(mean.x - range).SetMaxValue(mean.x + range);
                inputY.SetMinValue(mean.y - range).SetMaxValue(mean.y + range);
                inputZ.SetMinValue(mean.z - range).SetMaxValue(mean.z + range);
                inputX.Value = mean.x;
                inputY.Value = mean.y;
                inputZ.Value = mean.z;
                //disableUpdates = false;
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
                label.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
                panel.color = value ? Resource.Colors.EnabledPanel : Resource.Colors.DisabledPanel;
            }
        }

        public event Action<Vector3> ValueChanged;

        void OnValueChanged()
        {
            /*
            if (disableUpdates)
            {
                return;
            }
            */

            value = new Vector3(
                inputX.Value,
                inputY.Value,
                inputZ.Value
            );
            ValueChanged?.Invoke(value);
        }


        void UpdateInputLabels()
        {
            //disableUpdates = true;
            inputX.Value = value.x;
            inputY.Value = value.y;
            inputZ.Value = value.z;
            //disableUpdates = false;
        }

        public void ClearSubscribers()
        {
            ValueChanged = null;
        }

        [NotNull]
        public Vector3SliderWidget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }

        [NotNull]
        public Vector3SliderWidget SetValue(Vector3 f)
        {
            Value = f;
            return this;
        }

        [NotNull]
        public Vector3SliderWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        [NotNull]
        public Vector3SliderWidget SubscribeValueChanged(Action<Vector3> f)
        {
            ValueChanged += f;
            return this;
        }

        [NotNull]
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

            HandleLink[] links = GetComponentsInChildren<HandleLink>();
            foreach (var link in links)
            {
                link.Clicked += LinkOnClicked;
            }
        }

        void LinkOnClicked()
        {
            Mean = Value;
        }

        void Start()
        {
            OnValueChanged();
        }
    }
}