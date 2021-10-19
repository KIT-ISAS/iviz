using System;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ToggleWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Toggle toggle = null;
        [SerializeField] Text label = null;

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
                
                name = "Toggle:" + value;
                label.text = value;
            }
        }
        public bool Value
        {
            get => toggle.isOn;
            set
            {
                toggle.isOn = value;
            }
        }

        public bool Interactable
        {
            get => toggle.interactable;
            set
            {
                toggle.interactable = value;
                label.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
            }
        }

        public event Action<bool> ValueChanged;

        public void OnValueChanged(bool f)
        {
            ValueChanged?.Invoke(f);
        }

        public void ClearSubscribers()
        {
            ValueChanged = null;
        }

        [NotNull]
        public ToggleWidget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }

        [NotNull]
        public ToggleWidget SetValue(bool f)
        {
            Value = f;
            return this;
        }

        [NotNull]
        public ToggleWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        [NotNull]
        public ToggleWidget SubscribeValueChanged(Action<bool> f)
        {
            ValueChanged += f;
            return this;
        }
    }
}