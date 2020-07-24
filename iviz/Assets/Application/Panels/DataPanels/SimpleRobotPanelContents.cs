using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="SimpleRobotModuleData"/> 
    /// </summary>
    /// 
    public sealed class SimpleRobotPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public FrameWidget Frame { get; private set; }
        public InputFieldWithHintsWidget SourceParam { get; private set; }
        public ToggleWidget AttachToTf { get; private set; }
        public InputFieldWidget FramePrefix { get; private set; }
        public InputFieldWidget FrameSuffix { get; private set; }
        public ColorPickerWidget Tint { get; private set; }
        public SliderWidget Alpha { get; private set; }
        public ToggleWidget OcclusionOnlyMode { get; private set; }


        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Robot");
            Frame = p.AddFrame();
            SourceParam = p.AddInputFieldWithHints("Source Parameter").SetPlaceholder("<none>");
            AttachToTf = p.AddToggle("Attach to TF Frames");
            FramePrefix = p.AddInputField("TF Frame Prefix").SetPlaceholder("<none>");
            FrameSuffix = p.AddInputField("TF Frame Suffix").SetPlaceholder("<none>");
            Tint = p.AddColorPicker("Tint");
            Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            OcclusionOnlyMode = p.AddToggle("AR Occlusion Only Mode");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}