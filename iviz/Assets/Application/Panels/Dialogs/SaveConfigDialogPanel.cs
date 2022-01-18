
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public sealed class SaveConfigDialogPanel : ItemListDialogPanel
    {
        [SerializeField] InputFieldWidget input;
        [SerializeField] TrashButtonWidget saveButton;

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