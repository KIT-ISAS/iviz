#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class TfPublisherDialogContents : DetachablePanelContents
    {
        [SerializeField] TrashButtonWidget? close;
        [SerializeField] TrashButtonWidget? add;
        [SerializeField] TMP_Text? text;
        [SerializeField] LinkResolver? linkResolver;
        [SerializeField] InputFieldWithHintsWidget? nameField;
        [SerializeField] InputFieldWithHintsWidget? parentField;
        [SerializeField] Vector3SliderWidget? position;
        
        LinkResolver LinkResolver => linkResolver.AssertNotNull(nameof(linkResolver));
        public TrashButtonWidget Close => close.AssertNotNull(nameof(close));
        public TrashButtonWidget Add => add.AssertNotNull(nameof(add));
        public InputFieldWithHintsWidget NameField => nameField.AssertNotNull(nameof(nameField));
        public InputFieldWithHintsWidget ParentField => parentField.AssertNotNull(nameof(parentField));
        public Vector3SliderWidget Position => position.AssertNotNull(nameof(position));
        public TMP_Text Text => text.AssertNotNull(nameof(text));

        public event Action<string>? LinkClicked;

        void Awake()
        {
            LinkResolver.LinkClicked += s => LinkClicked?.Invoke(s);
        }
        
        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
            Add.ClearSubscribers();
            NameField.ClearSubscribers();
            ParentField.ClearSubscribers();
            Position.ClearSubscribers();
            LinkClicked = null;
        }
    }
}
