namespace Iviz.App
{
    public class TFPanelContents : ListenerPanelContents
    {
        public ListenerWidget ListenerStatic { get; protected set; }
        public ToggleWidget ShowAxes { get; private set; }
        public ToggleWidget ShowFrameLabels { get; private set; }
        public SliderWidget FrameSize { get; private set; }
        public SliderWidget FrameLabelSize { get; private set; }
        public ToggleWidget ConnectToParent { get; private set; }
        public ToggleWidget ShowAllFrames { get; private set; }
        public SenderWidget Sender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("TF");
            Listener = p.AddListener();
            ListenerStatic = p.AddListener();
            ShowAxes = p.AddToggle("Show Axes");
            ShowFrameLabels = p.AddToggle("Show Frame Names");
            FrameSize = p.AddSlider("Frame Size").SetMinValue(0.01f).SetMaxValue(0.5f);
            FrameLabelSize = p.AddSlider("Frame Names Size").SetMinValue(0.01f).SetMaxValue(0.5f);
            ConnectToParent = p.AddToggle("Connect Children to Parents");
            ShowAllFrames = p.AddToggle("Show All Frames");
            Sender = p.AddSender();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}