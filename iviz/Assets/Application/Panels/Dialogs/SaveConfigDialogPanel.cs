
using UnityEngine;

namespace Iviz.App
{
    public sealed class SaveConfigDialogPanel : ItemListDialogPanel
    {
        [SerializeField] InputFieldWidget input;
        [SerializeField] SimpleButtonWidget saveButton;

        public InputFieldWidget Input => input;
        public SimpleButtonWidget SaveButton => saveButton;

        public override void ClearSubscribers()
        {
            base.ClearSubscribers();
            input.ClearSubscribers();
            saveButton.ClearSubscribers();
        }
    }
}