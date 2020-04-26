namespace Iviz.App
{
    public interface IDialogPanelContents
    {
        bool Active { get; set; }

        void ClearSubscribers();
    }
}