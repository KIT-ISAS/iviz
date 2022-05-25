using System;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class Vector3SliderWidget : MonoBehaviour, IVector3Widget
    {
        [SerializeField] SliderWidget inputX;
        [SerializeField] SliderWidget inputY;
        [SerializeField] SliderWidget inputZ;
        [SerializeField] TMP_Text label;

        [SerializeField] Image panel;
        //bool disableUpdates;

        public string Label
        {
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                label.text = value;
            }
        }

        Vector3? value; // ensure that the next Value set will be different

        public Vector3 Value
        {
            get => value ?? Vector3.zero;
            set
            {
                if (this.value is { } existingValue && (existingValue - value).ApproximatelyZero())
                {
                    return;
                }

                this.value = value;
                Mean = value;
                UpdateInputLabels();
            }
        }

        float range;

        float Range
        {
            get => range;
            set
            {
                range = value;
                int numSteps = (int)(2 * range * 100);
                inputX.SetMinValue(mean.x - range).SetMaxValue(mean.x + range).SetNumberOfSteps(numSteps);
                inputY.SetMinValue(mean.y - range).SetMaxValue(mean.y + range).SetNumberOfSteps(numSteps);
                inputZ.SetMinValue(mean.z - range).SetMaxValue(mean.z + range).SetNumberOfSteps(numSteps);
            }
        }

        Vector3 mean;

        Vector3 Mean
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
            set
            {
                inputX.Interactable = value;
                inputY.Interactable = value;
                inputZ.Interactable = value;
                label.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
                panel.color = value ? Resource.Colors.EnabledPanel : Resource.Colors.DisabledPanel;
            }
        }

        public bool Visible
        {
            set => gameObject.SetActive(value);
        }

        public event Action<Vector3> ValueChanged;

        void OnValueChanged()
        {
            value = new Vector3(
                inputX.Value,
                inputY.Value,
                inputZ.Value
            );
            ValueChanged?.Invoke(Value);
        }


        void UpdateInputLabels()
        {
            inputX.Value = Value.x;
            inputY.Value = Value.y;
            inputZ.Value = Value.z;
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
            inputX.SetNumberOfSteps(100);
            inputY.SetNumberOfSteps(100);
            inputZ.SetNumberOfSteps(100);
            Range = 0.5f;
            UpdateInputLabels();
            inputX.SubscribeValueChanged(_ => OnValueChanged());
            inputY.SubscribeValueChanged(_ => OnValueChanged());
            inputZ.SubscribeValueChanged(_ => OnValueChanged());

            var links = GetComponentsInChildren<HandleLink>();
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