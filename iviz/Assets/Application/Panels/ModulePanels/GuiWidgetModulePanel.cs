
namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARModuleData"/> 
    /// </summary>
    public sealed class GuiWidgetModulePanel : ListenerModulePanel
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public TrashButtonWidget ResetButton { get; private set; }
        public MarkerWidget Marker { get; private set; }
        public SenderWidget FeedbackSender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("GUI Widgets");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Listener = p.AddListener();
            FeedbackSender = p.AddSender();
            ResetButton = p.AddResetButton();
            Marker = p.AddMarker();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}