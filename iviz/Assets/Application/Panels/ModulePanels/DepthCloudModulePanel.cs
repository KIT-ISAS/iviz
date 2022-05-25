using Iviz.Common;
using Iviz.Resources;

namespace Iviz.App
{
    public sealed class DepthCloudModulePanel : ModulePanel
    {
        public FrameWidget Frame { get; private set; }
        public InputFieldWithHintsWidget Depth { get; private set; }
        public DataLabelWidget Description { get; private set; }
        public ListenerWidget DepthTopic { get; private set; }
        //public ListenerWidget DepthInfoTopic { get; private set; }
        public InputFieldWithHintsWidget Color { get; private set; }
        public ListenerWidget ColorTopic { get; private set; }
        public ImagePreviewWidget DepthPreview { get; private set; }
        public ImagePreviewWidget ColorPreview { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public ToggleWidget ForceMinMax { get; private set; }
        public SliderWidgetWithScale Min { get; private set; }
        public SliderWidgetWithScale Max { get; private set; }
        public ToggleWidget FlipMinMax { get; private set; }

        public SimpleButtonWidget CloseButton { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("DepthCloud");
            DepthTopic = p.AddListener();
            //DepthInfoTopic = p.AddListener();
            ColorTopic = p.AddListener();
            Frame = p.AddFrame();
            Description = p.AddDataLabel("").SetHasRichText(true).SetCentered();
            Depth = p.AddInputFieldWithHints("Depth Topic");
            Color = p.AddInputFieldWithHints("Color Topic");
            DepthPreview = p.AddImagePreviewWidget("Depth Preview");
            ColorPreview = p.AddImagePreviewWidget("Color Preview");
            
            Colormap = p.AddDropdown("Colormap")
                .SetOptions(Resource.Colormaps.Names)
                .SetIndex((int)ColormapId.gray);
            ForceMinMax = p.AddToggle("Colormap Override Min/Max");
            Min = p.AddSliderWidgetWithScale("Colormap Min");
            Max = p.AddSliderWidgetWithScale("Colormap Max");
            FlipMinMax = p.AddToggle("Flip Min/Max");
            
            p.AddCollapsibleWidget("Colormap")
                .Attach(Colormap)
                .Attach(ForceMinMax)
                .Attach(Min)
                .Attach(Max)
                .Attach(FlipMinMax)
                .FinishAttaching();
            
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}