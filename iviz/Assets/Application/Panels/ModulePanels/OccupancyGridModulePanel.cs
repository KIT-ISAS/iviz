using Iviz.Common;
using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="OccupancyGridModuleData"/> 
    /// </summary>
    public sealed class OccupancyGridModulePanel : ListenerModulePanel
    {
        public SimpleButtonWidget CloseButton { get; private set; }

        public FrameWidget Frame { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public ToggleWidget FlipColors { get; private set; }
        public ToggleWidget ShowTexture { get; private set; }
        public ToggleWidget ShowCubes { get; private set; }
        public SliderWidget ScaleZ { get; private set; }
        public ColorPickerWidget Tint { get; private set; }
        //public SliderWidget Alpha { get; private set; }
        public ToggleWidget OcclusionOnlyMode { get; private set; }
        public DataLabelWidget Description { get; private set; }

        protected override void Initialize()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Occupancy\nGrid");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            Description = p.AddDataLabel("").SetHasRichText(true).SetCentered();
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)ColormapId.gray);
            FlipColors = p.AddToggle("Colormap Flip Min/Max");
            ShowTexture = p.AddToggle("Show As Textured Plane");

            Tint = p.AddColorPicker("Tint");
            //Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            ShowCubes = p.AddToggle("Show As Bars");
            ScaleZ = p.AddSlider("Height").SetMinValue(0.01f).SetMaxValue(5.0f).SetNumberOfSteps(49);
            OcclusionOnlyMode = p.AddToggle("AR Occlusion Only Mode");

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}