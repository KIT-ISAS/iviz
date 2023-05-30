using Iviz.Controllers;
using JetBrains.Annotations;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARModuleData"/> 
    /// </summary>
    public sealed class ARModulePanel : ModulePanel
    {
        public FrameWidget Frame { get; private set; }
        public SimpleButtonWidget CloseButton { get; private set; }
        public SimpleButtonWidget ResetButton { get; private set; }

        public SliderWidget WorldScale { get; private set; }

        public DropdownWidget OcclusionQuality { get; private set; }
        public DataLabelWidget Description { get; private set; }

        public ToggleWidget AutoFocus { get; private set; }

        //public ARMarkerWidget ARMarkers { get; private set; }
        public DropdownWidget PublishFrequency { get; private set; }

        public SenderWidget MarkerSender { get; private set; }
        public SenderWidget ColorSender { get; private set; }
        public SenderWidget DepthSender { get; private set; }
        public SenderWidget DepthConfidenceSender { get; private set; }
        public SenderWidget MeshSender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("AR");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            ResetButton = p.AddResetButton();
            Frame = p.AddFrame();
            Description = p.AddDataLabel("").SetHasRichText(true).SetCentered();
            WorldScale = p.AddSlider("World Scale").SetMinValue(0.01f).SetMaxValue(1f);

            AutoFocus = p.AddToggle("Enable AutoFocus");

            PublishFrequency = p.AddDropdown("Publish AR camera images");
            PublishFrequency.Options = new[]
            {
                "Off",
                "5 FPS",
                "10 FPS",
                "15 FPS",
                "20 FPS",
                "30 FPS",
                "Max FPS"
            };

            OcclusionQuality = p.AddDropdown("Occlusion Quality");
            OcclusionQuality.Options = new[]
            {
                "Off",
                "Fastest",
                "Medium",
                "Best",
            };

            //ARMarkers = p.AddARMarker();
            MarkerSender = p.AddSender();
            ColorSender = p.AddSender();
            DepthSender = p.AddSender();
            DepthConfidenceSender = p.AddSender();
            MeshSender = p.AddSender();

            p.AddCollapsibleWidget("Publishers")
                .Attach(MarkerSender)
                .Attach(ColorSender)
                .Attach(DepthSender)
                .Attach(DepthConfidenceSender)
                .Attach(MeshSender)
                .FinishAttaching();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}