using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="OccupancyGridModuleData"/> 
    /// </summary>
    public sealed class OccupancyGridPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        public FrameWidget Frame { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public ToggleWidget FlipColors { get; private set; }
        public NumberInputFieldWidget ScaleZ { get; private set; }
        public ColorPickerWidget Tint { get; private set; }
        //public SliderWidget Alpha { get; private set; }
        public ToggleWidget OcclusionOnlyMode { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Occupancy\nGrid");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)Resource.ColormapId.gray);
            FlipColors = p.AddToggle("Flip Color Bounds");
            ScaleZ = p.AddNumberInputField("Height Mult.");

            Tint = p.AddColorPicker("Tint");
            //Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            OcclusionOnlyMode = p.AddToggle("AR Occlusion Only Mode");

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}