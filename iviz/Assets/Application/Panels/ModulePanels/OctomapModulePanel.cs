using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="OctomapModuleData"/> 
    /// </summary>
    public sealed class OctomapModulePanel : ListenerModulePanel
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public FrameWidget Frame { get; private set; }
        public SliderWidget MaxDepth { get; private set; }
        public ColorPickerWidget Tint { get; private set; }
        public ToggleWidget OcclusionOnlyMode { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Octomap");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Listener = p.AddListener();
            Frame = p.AddFrame();
            Tint = p.AddColorPicker("Tint");
            MaxDepth = p.AddSlider("Max Depth").SetMinValue(8).SetMaxValue(16).SetIntegerOnly(true);
            OcclusionOnlyMode = p.AddToggle("AR Occlusion Only Mode");
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}