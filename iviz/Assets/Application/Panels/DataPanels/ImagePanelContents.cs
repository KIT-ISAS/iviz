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
        public SliderWidget Min { get; private set; }
        public SliderWidget Max { get; private set; }
        public ToggleWidget FlipMinMax { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        CollapsibleWidget billboard;

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
            Min = p.AddSlider("Colormap Min").SetMinValue(0).SetMaxValue(1);
            Max = p.AddSlider("Colormap Max").SetMinValue(0).SetMaxValue(1);
            FlipMinMax = p.AddToggle("Flip Min/Max");

            PreviewWidget = p.AddImagePreviewWidget("Preview");

            ShowBillboard = p.AddToggle("Show As Billboard");
            BillboardSize = p.AddSlider("Billboard Size").SetMinValue(0.1f).SetMaxValue(10);
            BillboardFollowsCamera = p.AddToggle("Billboard Points To You");
            BillboardOffset = p.AddVector3Slider("Billboard Offset");

            billboard = p.AddCollapsibleWidget("Billboard")
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