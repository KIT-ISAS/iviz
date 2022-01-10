using System;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class Vector3Widget : MonoBehaviour, IWidget
    {
        [SerializeField] InputFieldWidget inputX;
        [SerializeField] InputFieldWidget inputY;
        [SerializeField] InputFieldWidget inputZ;
        [SerializeField] Text label;
        [SerializeField] Image panel;
        bool disableUpdates;

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

                name = "ColorPicker:" + value;
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
                    return;
                }

                this.value = value;
                UpdateInputLabels();
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
        public event Action<Vector3> EndEdit;

        void ParseValue()
        {
            Vector3 v;
            if (!float.TryParse(inputX.Value, out v.x))
            {
                v.x = 0;
            }

            if (!float.TryParse(inputY.Value, out v.y))
            {
                v.y = 0;
            }

            if (!float.TryParse(inputZ.Value, out v.z))
            {
                v.z = 0;
            }

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
            inputX.Value = value.x.ToString(UnityUtils.Culture);
            inputY.Value = value.y.ToString(UnityUtils.Culture);
            inputZ.Value = value.z.ToString(UnityUtils.Culture);
            disableUpdates = false;
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
            UpdateInputLabels();

            inputX.SetContentType(TMP_InputField.ContentType.DecimalNumber);
            inputY.SetContentType(TMP_InputField.ContentType.DecimalNumber);
            inputZ.SetContentType(TMP_InputField.ContentType.DecimalNumber);

            inputX.SubscribeValueChanged(_ => OnValueChanged());
            inputY.SubscribeValueChanged(_ => OnValueChanged());
            inputZ.SubscribeValueChanged(_ => OnValueChanged());
            
            inputX.SubscribeEndEdit(_ => OnEndEdit());
            inputY.SubscribeEndEdit(_ => OnEndEdit());
            inputZ.SubscribeEndEdit(_ => OnEndEdit());
        }

        void Start()
        {
            OnValueChanged();
        }
    }
}