using System;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class InputFieldWidget : MonoBehaviour, IWidget
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
                name = "InputField:" + value;
            }
        }

        [NotNull]
        public string Value
        {
            get => text.text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                text.text = value;
            }
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

        InputField.ContentType ContentType
        {
            get => text.contentType;
            set => text.contentType = value;
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

        public event Action<string> ValueChanged;
        public event Action<string> EndEdit;

        public void OnValueChanged(string f)
        {
            ValueChanged?.Invoke(f);
        }

        public void OnEndEdit(string f)
        {
            EndEdit?.Invoke(f);
        }

        public void ClearSubscribers()
        {
            ValueChanged = null;
            EndEdit = null;
        }

        [NotNull]
        public InputFieldWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        [NotNull]
        public InputFieldWidget SetValue([NotNull] string f)
        {
            Value = f ?? throw new ArgumentNullException(nameof(f));
            return this;
        }

        [NotNull]
        public InputFieldWidget SetPlaceholder([NotNull] string f)
        {
            Placeholder = f;
            return this;
        }

        [NotNull]
        public InputFieldWidget SetContentType(InputField.ContentType contentType)
        {
            ContentType = contentType;
            return this;
        }

        [NotNull]
        public InputFieldWidget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }

        [NotNull]
        public InputFieldWidget SubscribeValueChanged(Action<string> f)
        {
            ValueChanged += f;
            return this;
        }

        [NotNull]
        public InputFieldWidget SubscribeEndEdit(Action<string> f)
        {
            EndEdit += f;
            return this;
        }
    }
}