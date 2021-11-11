using UnityEngine;

namespace Iviz.App
{
    public sealed class DepthCloudPanelContents : DataPanelContents
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
        public TrashButtonWidget CloseButton { get; private set; }

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
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}