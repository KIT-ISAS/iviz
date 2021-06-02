
using System;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class NumberInputFieldWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text label = null;
        [SerializeField] InputField text = null;
        [SerializeField] Text placeholder = null;
        [SerializeField] Image textImage = null;

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
                
                label.text = value;
                name = "NumberInputField:" + value;
            }
        }

        public float Value
        {
            get
            {
                UnityUtils.TryParse(text.text, out float f);
                return f;
            }
            set => text.text = value.ToString(UnityUtils.Culture);
        }

        [NotNull]
        public string Placeholder
        {
            get => placeholder.text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                
                placeholder.text = value;
            }
        }

        public bool Interactable
        {
            get => text.interactable;
            set
            {
                text.interactable = value;
                textImage.raycastTarget = value;
                label.color = value ? Resource.Colors.EnabledFontColor : Resource.Colors.DisabledFontColor;
            }
        }


        public event Action<float> ValueChanged;
        public event Action<float> EndEdit;

        void Awake()
        {
            Value = Value;
        }

        public void OnValueChanged(string newText)
        {
            UnityUtils.TryParse(newText, out float f);
            ValueChanged?.Invoke(f);
        }

        public void OnEndEdit(string newText)
        {
            UnityUtils.TryParse(newText, out float f);
            EndEdit?.Invoke(f);
        }

        public void ClearSubscribers()
        {
            ValueChanged = null;
            EndEdit = null;
        }

        [NotNull]
        public NumberInputFieldWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        [NotNull]
        public NumberInputFieldWidget SetValue(float f)
        {
            Value = f;
            return this;
        }

        [NotNull]
        public NumberInputFieldWidget SetPlaceholder([NotNull] string f)
        {
            Placeholder = f;
            return this;
        }

        [NotNull]
        public NumberInputFieldWidget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }

        [NotNull]
        public NumberInputFieldWidget SubscribeValueChanged(Action<float> f)
        {
            ValueChanged += f;
            return this;
        }

        [NotNull]
        public NumberInputFieldWidget SubscribeEndEdit(Action<float> f)
        {
            EndEdit += f;
            return this;
        }
    }
}