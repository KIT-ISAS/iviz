﻿namespace Iviz.App
{
    /// <summary>
    /// <see cref="TfModuleData"/> 
    /// </summary>
    public sealed class TfModulePanel : ListenerModulePanel
    {
        public ListenerWidget ListenerStatic { get; private set; }
        public SimpleButtonWidget CloseButton { get; private set; }
        public SimpleButtonWidget ResetButton { get; private set; }
        public FrameWidget Frame { get; private set; }
        public ToggleWidget PreferUdp { get; private set; }
        public ToggleWidget ShowFrameLabels { get; private set; }
        public SliderWidget FrameSize { get; private set; }
        public ToggleWidget ConnectToParent { get; private set; }
        public ToggleWidget KeepAllFrames { get; private set; }
        public ToggleWidget FlipZ { get; private set; }
        public ToggleWidget Interactable { get; private set; }
        public TfPublisherWidget Publisher { get; private set; }
        public SenderWidget Sender { get; private set; }
        //public SenderWidget TapSender { get; private set; }

        protected override void Initialize()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("TF");
            HideButton = p.AddHideButton();
            CloseButton = p.AddCloseButton();
            ResetButton = p.AddResetButton();
            Listener = p.AddListener();
            ListenerStatic = p.AddListener();
            Frame = p.AddFrame();
            PreferUdp = p.AddToggle("Prefer Unreliable/UDP");
            KeepAllFrames = p.AddToggle("Keep All Frames, Even if Unused");
            ShowFrameLabels = p.AddToggle("Show Frame Names");
            ConnectToParent = p.AddToggle("Connect Children to Parents");
            FlipZ = p.AddToggle("Make Z Axis Point Down");
            Interactable = p.AddToggle("Frames Interactable");
            FrameSize = p.AddSlider("Frame Size").SetMinValue(0.01f).SetMaxValue(0.5f).SetNumberOfSteps(49);
            Publisher = p.AddTfPublisher();
            Sender = p.AddSender();
            //TapSender = p.AddSender();
            
            p.AddCollapsibleWidget("Publishers")
                .Attach(Sender)
                //.Attach(TapSender)
                .FinishAttaching();            
            
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}