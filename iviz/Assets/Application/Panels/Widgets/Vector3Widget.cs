using System;
using System.Threading;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class Vector3Widget : MonoBehaviour, IVector3Widget
    {
        [SerializeField] InputFieldWidget inputX;
        [SerializeField] InputFieldWidget inputY;
        [SerializeField] InputFieldWidget inputZ;
        [SerializeField] Text label;
        [SerializeField] Image panel;
        bool disableUpdates;

        public string Label
        {
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                label.text = value;
            }
        }

        Vector3 value;

        public Vector3 Value
        {
            get => value;
            set
            {
                if ((this.value - value).ApproximatelyZero())
                {
                    ValidateValues();
                    return;
                }

                this.value = value;
                UpdateInputLabels();
            }
        }

        void ValidateValues()
        {
            if (!UnityUtils.TryParse(inputX.Value, out _))
            {
                inputX.Value = "0";
            }

            if (!UnityUtils.TryParse(inputY.Value, out _))
            {
                inputY.Value = "0";
            }

            if (!UnityUtils.TryParse(inputZ.Value, out _))
            {
                inputZ.Value = "0";
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
        public event Action<Vector3> EndEdit;

        void ParseValue()
        {
            Vector3 v;
            UnityUtils.TryParse(inputX.Value, out v.x);
            UnityUtils.TryParse(inputY.Value, out v.y);
            UnityUtils.TryParse(inputZ.Value, out v.z);
            value = v;
        }

        void OnValueChanged()
        {
            if (disableUpdates)
            {
                return;
            }

            ParseValue();
            ValueChanged?.Invoke(value);
        }

        void OnEndEdit()
        {
            if (disableUpdates)
            {
                return;
            }

            ParseValue();
            EndEdit?.Invoke(value);
        }


        void UpdateInputLabels()
        {
            disableUpdates = true;
            inputX.Value = FloatToString(value.x);
            inputY.Value = FloatToString(value.y);
            inputZ.Value = FloatToString(value.z);
            disableUpdates = false;
            
            [NotNull]
            static string FloatToString(float f) =>f.ToString("0.###", UnityUtils.Culture);
        }

        public void ClearSubscribers()
        {
            ValueChanged = null;
            EndEdit = null;
        }

        [NotNull]
        public Vector3Widget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }

        [NotNull]
        public Vector3Widget SetValue(Vector3 f)
        {
            Value = f;
            return this;
        }

        [NotNull]
        public Vector3Widget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        [NotNull]
        public Vector3Widget SubscribeValueChanged(Action<Vector3> f)
        {
            ValueChanged += f;
            return this;
        }

        void Awake()
        {
            Interactable = true;

            inputX.SetContentType(TMP_InputField.ContentType.DecimalNumber);
            inputY.SetContentType(TMP_InputField.ContentType.DecimalNumber);
            inputZ.SetContentType(TMP_InputField.ContentType.DecimalNumber);

            UpdateInputLabels();

            void ValueCallback(string _) => OnValueChanged();
            inputX.SubscribeValueChanged(ValueCallback);
            inputY.SubscribeValueChanged(ValueCallback);
            inputZ.SubscribeValueChanged(ValueCallback);

            void EndEditCallback(string _) => OnEndEdit();
            inputX.SubscribeEndEdit(EndEditCallback);
            inputY.SubscribeEndEdit(EndEditCallback);
            inputZ.SubscribeEndEdit(EndEditCallback);
        }
    }
}