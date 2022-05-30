#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.App
{
    public sealed class SaveConfigDialogPanel : ItemListDialogPanel
    {
        [SerializeField] InputFieldWidget? input;
        [SerializeField] SimpleButtonWidget? saveButton;

        public InputFieldWidget Input => input.AssertNotNull(nameof(input));
        public SimpleButtonWidget SaveButton => saveButton.AssertNotNull(nameof(saveButton));

        public override void ClearSubscribers()
        {
            base.ClearSubscribers();
            Input.ClearSubscribers();
            SaveButton.ClearSubscribers();
        }
    }
}