using System.Collections.Generic;

namespace Iviz.App
{
    public class TFPanelContents : ListenerPanelContents
    {
        public ToggleWidget ShowAxes { get; private set; }
        public ToggleWidget ShowFrameLabels { get; private set; }
        public SliderWidget FrameSize { get; private set; }
        public SliderWidget FrameLabelSize { get; private set; }
        public ToggleWidget ConnectToParent { get; private set; }
        public ToggleWidget ShowAllFrames { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("TF");
            Stats = p.AddSectionTitleWidget("Off | 0 Hz | 0 - 0 ms");
            ShowAxes = p.AddToggle("Show Axes");
            ShowFrameLabels = p.AddToggle("Show Frame Names");
            FrameSize = p.AddSlider("Frame Size").SetMinValue(0.01f).SetMaxValue(0.5f);
            FrameLabelSize = p.AddSlider("Frame Names Size").SetMinValue(0.01f).SetMaxValue(0.5f);
            ConnectToParent = p.AddToggle("Connect Children to Parents");
            ShowAllFrames = p.AddToggle("Show All Frames");
            p.UpdateSize();
            gameObject.SetActive(false);

            Widgets = new Widget[] { ShowAxes, ShowFrameLabels, FrameLabelSize, ConnectToParent, FrameSize, ShowAllFrames };

        }
    }
}