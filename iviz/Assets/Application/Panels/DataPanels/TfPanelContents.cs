namespace Iviz.App
{
    /// <summary>
    /// <see cref="TfModuleData"/> 
    /// </summary>
    public sealed class TfPanelContents : ListenerPanelContents
    {
        public ListenerWidget ListenerStatic { get; private set; }
        public TrashButtonWidget ResetButton { get; private set; }
        public FrameWidget Frame { get; private set; }
        public ToggleWidget ShowFrameLabels { get; private set; }
        public SliderWidget FrameSize { get; private set; }
        public ToggleWidget ConnectToParent { get; private set; }
        public ToggleWidget KeepAllFrames { get; private set; }
        public ToggleWidget FlipZ { get; private set; }
        public SenderWidget Sender { get; private set; }
        public SenderWidget TapSender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("TF");
            HideButton = p.AddHideButton();
            ResetButton = p.AddResetButton();
            Listener = p.AddListener();
            ListenerStatic = p.AddListener();
            Frame = p.AddFrame();
            KeepAllFrames = p.AddToggle("Keep All Frames, Even if Unused");
            ShowFrameLabels = p.AddToggle("Show Frame Names");
            ConnectToParent = p.AddToggle("Connect Children to Parents");
            FlipZ = p.AddToggle("Make Z Axis Point Down");
            FrameSize = p.AddSlider("Frame Size").SetMinValue(0.01f).SetMaxValue(0.5f).SetNumberOfSteps(49);
            Sender = p.AddSender();
            TapSender = p.AddSender();
            
            p.AddCollapsibleWidget("Publishers")
                .Attach(Sender)
                .Attach(TapSender)
                .UpdateSize();            
            
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}