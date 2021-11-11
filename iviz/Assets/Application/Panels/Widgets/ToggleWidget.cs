#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ToggleWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Toggle? toggle = null;
        [SerializeField] TMP_Text? label = null;

        TMP_Text Label => label.AssertNotNull(nameof(label));
        Toggle Toggle => toggle.AssertNotNull(nameof(toggle));
        
        public string Text
        {
            get => Label.text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                
                name = "Toggle:" + value;
                Label.text = value;
            }
        }
        public bool Value
        {
            get => Toggle.isOn;
            set => Toggle.isOn = value;
        }

        public bool Interactable
        {
            get => Toggle.interactable;
            set
            {
                Toggle.interactable = value;
                Label.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
            }
        }

        public event Action<bool>? ValueChanged;

        public void OnValueChanged(bool f)
        {
            ValueChanged?.Invoke(f);
        }

        public void ClearSubscribers()
        {
            ValueChanged = null;
        }

        public ToggleWidget SetLabel(string f)
        {
            Text = f;
            return this;
        }

        public ToggleWidget SetValue(bool f)
        {
            Value = f;
            return this;
        }

        public ToggleWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        public ToggleWidget SubscribeValueChanged(Action<bool> f)
        {
            ValueChanged += f;
            return this;
        }
    }
}