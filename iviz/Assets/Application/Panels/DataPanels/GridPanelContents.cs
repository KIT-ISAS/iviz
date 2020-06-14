using Iviz.Displays;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridDisplayData"/> 
    /// </summary>
    public class GridPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public SliderWidget LineWidth { get; private set; }
        public SliderWidget CellSize { get; private set; }
        public SliderWidget NumberOfCells { get; private set; }
        public DropdownWidget Orientation { get; private set; }
        public ColorPickerWidget ColorPicker { get; private set; }
        public ToggleWidget ShowInterior { get; private set; }
        public ToggleWidget FollowCamera { get; private set; }
        public Vector3Widget Offset { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Grid");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            LineWidth = p.AddSlider("Grid Line Width").SetMinValue(0.01f).SetMaxValue(0.1f).UpdateValue();
            CellSize = p.AddSlider("Grid Cell Size").SetMinValue(0.1f).SetMaxValue(10f).SetValue(1.0f).UpdateValue();
            NumberOfCells = p.AddSlider("Number of Cells").SetMinValue(1).SetMaxValue(50).SetIntegerOnly(true).SetValue(10).UpdateValue();
            Orientation = p.AddDropdown("Orientation").SetOptions(GridResource.OrientationNames).SetIndex(0);
            ColorPicker = p.AddColorPicker("Grid Color").SetValue(Color.gray);
            ShowInterior = p.AddToggle("Show Interior").SetValue(true);
            FollowCamera = p.AddToggle("Follow Camera").SetValue(true);
            Offset = p.AddVector3("Offset");
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}