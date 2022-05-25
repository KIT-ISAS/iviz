#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class InputFieldWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? label;
        [SerializeField] TMP_InputField? text;
        [SerializeField] TMP_Text? placeholder;
        [SerializeField] Image? textImage;

        TMP_Text Label => label.AssertNotNull(nameof(label));
        TMP_InputField Text => text.AssertNotNull(nameof(text));
        TMP_Text PlaceholderObject => placeholder.AssertNotNull(nameof(placeholder));
        Image TextImage => textImage.AssertNotNull(nameof(textImage));

        public string Title
        {
            get => Label.text;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                Label.text = value;
                name = "InputField:" + value;
            }
        }

        public string Value
        {
            get => Text.text;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                Text.text = value;
            }
        }

        public string Placeholder
        {
            get => PlaceholderObject.text;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                PlaceholderObject.text = value;
            }
        }

        TMP_InputField.ContentType ContentType
        {
            get => Text.contentType;
            set => Text.contentType = value;
        }

        public bool Interactable
        {
            get => Text.interactable;
            set
            {
                Text.interactable = value;
                TextImage.raycastTarget = value;
                Label.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
            }
        }

        public event Action<string>? ValueChanged;
        public event Action<string>? Submit;
        public event Action<string>? EndEdit;

        void Awake()
        {
            Text.onValueChanged.AddListener(OnValueChanged);
            Text.onSubmit.AddListener(OnSubmit);
            Text.onEndEdit.AddListener(OnEndEdit);
        }

        void OnValueChanged(string f) => ValueChanged?.Invoke(f);

        void OnSubmit(string f) => Submit?.Invoke(f);
        
        void OnEndEdit(string f) => EndEdit?.Invoke(f);

        public void ClearSubscribers()
        {
            ValueChanged = null;
            Submit = null;
            EndEdit = null;
        }

        public InputFieldWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        public InputFieldWidget SetValue(string f)
        {
            Value = f ?? throw new ArgumentNullException(nameof(f));
            return this;
        }

        public InputFieldWidget SetPlaceholder(string f)
        {
            Placeholder = f;
            return this;
        }

        public InputFieldWidget SetContentType(TMP_InputField.ContentType contentType)
        {
            ContentType = contentType;
            return this;
        }

        public InputFieldWidget SetLabel(string f)
        {
            Title = f;
            return this;
        }

        public InputFieldWidget SubscribeValueChanged(Action<string> f)
        {
            ValueChanged += f;
            return this;
        }

        public InputFieldWidget SubscribeEndEdit(Action<string> f)
        {
            Submit += f;
            return this;
        }
    }
}