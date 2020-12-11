using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridModuleData"/> 
    /// </summary>
    public sealed class GridPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public SliderWidget LineWidth { get; private set; }
        //public SliderWidget CellSize { get; private set; }
        //public SliderWidget NumberOfCells { get; private set; }
        //public DropdownWidget Orientation { get; private set; }
        public ColorPickerWidget ColorPicker { get; private set; }
        public ToggleWidget ShowInterior { get; private set; }
        public ToggleWidget FollowCamera { get; private set; }
        public ToggleWidget HideInARMode { get; private set; }
        public Vector3SliderWidget Offset { get; private set; }
        public ToggleWidget PublishLongTapPosition { get; private set; }
        public InputFieldWithHintsWidget TapTopic { get; private set; }
        public SenderWidget Sender { get; private set; }
        public DataLabelWidget LastTapPosition { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Grid");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            LineWidth = p.AddSlider("Grid Line Width").SetMinValue(0.001f).SetMaxValue(0.1f).SetNumberOfSteps(99);
            //CellSize = p.AddSlider("Grid Cell Size").SetMinValue(0.1f).SetMaxValue(10f).SetValue(1.0f).UpdateValue();
            //NumberOfCells = p.AddSlider("Number of Cells").SetMinValue(10).SetMaxValue(90).SetNumberOfSteps(4).SetValue(10).UpdateValue();
            //Orientation = p.AddDropdown("Orientation").SetOptions(GridResource.OrientationNames).SetIndex(0);
            ColorPicker = p.AddColorPicker("Grid Color").SetValue(Color.gray);
            ShowInterior = p.AddToggle("Show Interior").SetValue(true);
            FollowCamera = p.AddToggle("Follow Camera").SetValue(true);
            HideInARMode = p.AddToggle("Hide in AR Mode").SetValue(true);
            Offset = p.AddVector3Slider("Offset");
            PublishLongTapPosition = p.AddToggle("Publish Long Tap Position").SetValue(true);
            TapTopic = p.AddInputFieldWithHints("Tap Topic");
            Sender = p.AddSender();
            LastTapPosition = p.AddDataLabel("").SetAlignment(TextAnchor.MiddleCenter).SetHasRichText(true);
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}