using Iviz.Sdf;

namespace Iviz.App
{
    public sealed class XRModulePanel : ModulePanel
    {
        public SliderWidgetWithScale WorldScale { get; private set; }
        public SenderWidget LeftSender { get; private set; }
        public SenderWidget RightSender { get; private set; }
        public SenderWidget GazeSender { get; private set; }
        
        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("XR");
            HideButton = p.AddHideButton();
            WorldScale = p.AddSliderWidgetWithScale("World Scale").EnableNegative(false);
            LeftSender = p.AddSender();
            RightSender = p.AddSender();
            GazeSender = p.AddSender();
            
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}