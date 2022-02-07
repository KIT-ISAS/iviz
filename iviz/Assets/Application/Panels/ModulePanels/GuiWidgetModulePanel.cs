
namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARModuleData"/> 
    /// </summary>
    public sealed class GuiWidgetModulePanel : ListenerModulePanel
    {
        public FrameWidget Frame { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public SenderWidget FeedbackSender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("GUI Widgets");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Listener = p.AddListener();
            Frame = p.AddFrame();
            FeedbackSender = p.AddSender();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}