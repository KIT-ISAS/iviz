using System.Collections.Generic;
using Iviz.Resources;
using UnityEngine.UI;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="PointCloudDisplayData"/> 
    /// </summary>

    public class PathPanelContents : ListenerPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public SliderWidget LineWidth { get; private set; }
        public ToggleWidget ShowAxes { get; private set; }
        public SliderWidget AxesLength { get; private set; }
        public ToggleWidget ShowLines { get; private set; }
        public ColorPickerWidget LineColor { get; private set; }

        void Start()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("PointCloud");
            Listener = p.AddListener();
            Frame = p.AddFrame();

            ShowAxes = p.AddToggle("Show Frames");
            AxesLength = p.AddSlider("Frame Size").SetMinValue(0.01f).SetMaxValue(0.5f).SetNumberOfSteps(49);

            ShowLines = p.AddToggle("Connect Frames");
            LineWidth = p.AddSlider("Line Width").SetMinValue(0.01f).SetMaxValue(0.5f).SetNumberOfSteps(49);
            LineColor = p.AddColorPicker("Line Color");

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}