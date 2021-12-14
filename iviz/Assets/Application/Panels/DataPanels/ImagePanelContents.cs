using Iviz.Common;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ImageModuleData"/> 
    /// </summary>
    public sealed class ImagePanelContents : ListenerPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public ToggleWidget ShowBillboard { get; private set; }
        public ToggleWidget BillboardFollowsCamera { get; private set; }
        public SliderWidget BillboardSize { get; private set; }
        public Vector3SliderWidget BillboardOffset { get; private set; }
        public ImagePreviewWidget PreviewWidget { get; private set; }
        public DataLabelWidget Description { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public ToggleWidget ForceMinMax { get; private set; }
        public SliderWidgetWithScale Min { get; private set; }
        public SliderWidgetWithScale Max { get; private set; }
        public ToggleWidget FlipMinMax { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Image");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            Description = p.AddDataLabel("").SetHasRichText(true).SetCentered();
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
                .UpdateSize();

            PreviewWidget = p.AddImagePreviewWidget("Preview");

            ShowBillboard = p.AddToggle("Show As Billboard");
            BillboardSize = p.AddSlider("Billboard Size").SetMinValue(0.1f).SetMaxValue(10);
            BillboardFollowsCamera = p.AddToggle("Billboard Points To You");
            BillboardOffset = p.AddVector3Slider("Billboard Offset");

            p.AddCollapsibleWidget("Billboard")
                .Attach(ShowBillboard)
                .Attach(BillboardSize)
                .Attach(BillboardFollowsCamera)
                .Attach(BillboardOffset)
                .UpdateSize();

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}