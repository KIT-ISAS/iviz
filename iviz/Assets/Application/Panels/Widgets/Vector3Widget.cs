using System;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class Vector3Widget : MonoBehaviour, IWidget
    {
        [SerializeField] InputFieldWidget inputX = null;
        [SerializeField] InputFieldWidget inputY = null;
        [SerializeField] InputFieldWidget inputZ = null;
        [SerializeField] Text label = null;
        [SerializeField] Image panel = null;
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
            ValueChanged?.Invoke(value);
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