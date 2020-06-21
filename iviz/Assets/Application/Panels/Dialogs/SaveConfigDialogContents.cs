

namespace Iviz.App
{
    public sealed class SaveConfigDialogContents : DialogItemList
    {
        public InputFieldWidget input;
        public TrashButtonWidget saveButton;

        public override void ClearSubscribers()
        {
            base.ClearSubscribers();
            input.ClearSubscribers();
            saveButton.ClearSubscribers();
        }
    }
}