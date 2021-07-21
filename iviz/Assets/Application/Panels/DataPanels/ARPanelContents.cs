
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARModuleData"/> 
    /// </summary>
    public sealed class ARPanelContents : DataPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public TrashButtonWidget ResetButton { get; private set; }
        public SliderWidget WorldScale { get; private set; }
        //public DropdownWidget OcclusionQuality { get; private set; }
        public DataLabelWidget Description { get; private set; }

        public ToggleWidget AutoFocus { get; private set; }
        
        public ARMarkerWidget ARMarkers { get; private set; }
        public SenderWidget MarkerSender { get; private set; }
        
        public ToggleWidget PublishColor { get; private set; }
        public SenderWidget ColorSender { get; private set; }
        public ToggleWidget PublishDepth { get; private set; }
        public SenderWidget DepthSender { get; private set; }
        

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("AR");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            ResetButton = p.AddResetButton();
            Frame = p.AddFrame();
            Description = p.AddDataLabel("").SetHasRichText(true).SetAlignment(TextAnchor.MiddleCenter);
            WorldScale = p.AddSlider("World Scale").SetMinValue(0.01f).SetMaxValue(1f);

            AutoFocus = p.AddToggle("Enable AutoFocus");

            ARMarkers = p.AddARMarker();
            MarkerSender = p.AddSender();
            PublishColor = p.AddToggle("Publish Color");
            PublishDepth = p.AddToggle("Publish Depth");
            ColorSender = p.AddSender();
            DepthSender = p.AddSender();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}