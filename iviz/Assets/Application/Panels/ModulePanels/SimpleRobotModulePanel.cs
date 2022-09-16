﻿namespace Iviz.App
{
    /// <summary>
    /// <see cref="SimpleRobotModuleData"/> 
    /// </summary>
    /// 
    public sealed class SimpleRobotModulePanel : ModulePanel
    {
        public SimpleButtonWidget CloseButton { get; private set; }
        public SimpleButtonWidget ResetButton { get; private set; }
        public FrameWidget Frame { get; private set; }
        public DataLabelWidget HelpText { get; private set; }
        public InputFieldWithHintsWidget SourceParameter { get; private set; }
        public DropdownWidget SavedRobotName { get; private set; }
        public ToggleWidget AttachToTf { get; private set; }
        public ToggleWidget EnableColliders { get; private set; }
        CollapsibleWidget Material { get; set; }
        public ColorPickerWidget Tint { get; private set; }
        public SliderWidget Alpha { get; private set; }
        public SliderWidget Metallic { get; private set; }
        public SliderWidget Smoothness { get; private set; }
        public ToggleWidget OcclusionOnlyMode { get; private set; }
        public ToggleWidget KeepMeshMaterials { get; private set; }
        public ToggleWidget Save { get; private set; }

        public RobotWidget Robot { get; private set; }

        CollapsibleWidget Decorations { get; set; }
        public InputFieldWidget Prefix { get; private set; }
        public InputFieldWidget Suffix { get; private set; }



        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Robot");
            Frame = p.AddFrame();
            HelpText = p.AddDataLabel("<b>No Robot Loaded</b>").SetCentered().SetHasRichText(true);
            SourceParameter = p.AddInputFieldWithHints("Load From Source Parameter").SetPlaceholder("<none>");
            SavedRobotName = p.AddDropdown("Load From Saved");
            AttachToTf = p.AddToggle("Attach to TF Frames");
            EnableColliders = p.AddToggle("Links Interactable");
            Tint = p.AddColorPicker("Tint");
            Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            Metallic = p.AddSlider("Metallic").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            Smoothness = p.AddSlider("Smoothness").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            OcclusionOnlyMode = p.AddToggle("AR Occlusion Only Mode");
            KeepMeshMaterials = p.AddToggle("Keep Mesh Materials");
            Save = p.AddToggle("Save this Robot Locally");

            Robot = p.AddRobot();

            Material = p.AddCollapsibleWidget("Visuals")
                .Attach(OcclusionOnlyMode)
                .Attach(KeepMeshMaterials)
                .Attach(Tint)
                .Attach(Alpha)
                .Attach(Metallic)
                .Attach(Smoothness)
                .FinishAttaching();

            Prefix = p.AddInputField("Add Link Prefix");
            Suffix = p.AddInputField("Add Link Suffix");
            Decorations = p.AddCollapsibleWidget("Prefixes and Suffixes")
                .Attach(Prefix)
                .Attach(Suffix)
                .FinishAttaching();
            
            CloseButton = p.AddTrashButton();
            ResetButton = p.AddResetButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}