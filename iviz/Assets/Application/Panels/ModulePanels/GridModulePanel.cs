using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridModuleData"/> 
    /// </summary>
    public sealed class GridModulePanel : ModulePanel
    {
        public SimpleButtonWidget CloseButton { get; private set; }
        public FrameWidget Frame { get; private set; }
        public ToggleWidget ShowInterior { get; private set; }
        public DropdownWidget GridSize { get; private set; }
        public ToggleWidget FollowCamera { get; private set; }
        public ToggleWidget HideInARMode { get; private set; }
        public ToggleWidget Interactable { get; private set; }
        public ToggleWidget DarkMode { get; private set; }
        public Vector3MultiWidget Offset { get; private set; }
        public ColorPickerWidget ColorPicker { get; private set; }
        public SliderWidget Metallic { get; private set; }
        public SliderWidget Smoothness { get; private set; }
        public ToggleWidget OcclusionOnlyMode { get; private set; }
        
        protected override void Initialize()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Grid");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Frame = p.AddFrame();
            GridSize = p.AddDropdown("Size").SetOptions(new[] { "10", "30", "50", "70", "90", "190" });
            ShowInterior = p.AddToggle("Show Interior").SetValue(true);
            FollowCamera = p.AddToggle("Follow Camera").SetValue(true);
            Interactable = p.AddToggle("Interactable").SetValue(true);
            HideInARMode = p.AddToggle("Hide in AR Mode").SetValue(true);
            DarkMode = p.AddToggle("Dark Mode").SetValue(true);
            Offset = p.AddVector3Multi("Offset");
            ColorPicker = p.AddColorPicker("Grid Color").SetValue(Color.gray);
            Metallic = p.AddSlider("Metallic").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            Smoothness = p.AddSlider("Smoothness").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            OcclusionOnlyMode = p.AddToggle("AR Occlusion Only Mode");


            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}