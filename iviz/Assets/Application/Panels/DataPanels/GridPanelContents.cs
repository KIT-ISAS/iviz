using Iviz.Displays;
using UnityEngine;

namespace Iviz.App
{
    public class GridPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public SliderWidget LineWidth { get; private set; }
        public SliderWidget CellSize { get; private set; }
        public SliderWidget NumberOfCells { get; private set; }
        public DropdownWidget Orientation { get; private set; }
        public ColorPickerWidget ColorPicker { get; private set; }
        public ToggleWidget ShowInterior { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Grid");
            CloseButton = p.AddTrashButton();
            LineWidth = p.AddSlider("Grid Line Width").SetMinValue(0.01f).SetMaxValue(0.1f).UpdateValue();
            CellSize = p.AddSlider("Grid Cell Size").SetMinValue(0.01f).SetMaxValue(0.1f).SetValue(1.0f).UpdateValue();
            NumberOfCells = p.AddSlider("Number of Cells").SetMinValue(1).SetMaxValue(50).SetIntegerOnly(true).SetValue(10).UpdateValue();
            Orientation = p.AddDropdown("Orientation").SetOptions(GridResource.OrientationNames).SetIndex(0);
            ColorPicker = p.AddColorPicker("Grid Color").SetValue(Color.gray);
            ShowInterior = p.AddToggle("Show Interior").SetValue(true);
            p.UpdateSize();
            gameObject.SetActive(false);

            Widgets = new Widget[] { CloseButton, LineWidth, CellSize, NumberOfCells, Orientation, ColorPicker, ShowInterior };
        }
    }
}