
using UnityEngine;

namespace Iviz.App
{
    public sealed class SaveConfigDialogContents : ItemListDialogContents
    {
        [SerializeField] InputFieldWidget input = null;
        [SerializeField] TrashButtonWidget saveButton = null;

        public InputFieldWidget Input => input;
        public TrashButtonWidget SaveButton => saveButton;

        public override void ClearSubscribers()
        {
            base.ClearSubscribers();
            input.ClearSubscribers();
            saveButton.ClearSubscribers();
        }
    }
}