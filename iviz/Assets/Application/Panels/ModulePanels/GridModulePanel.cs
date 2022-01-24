using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridModuleData"/> 
    /// </summary>
    public sealed class GridModulePanel : ModulePanel
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleWidget ShowInterior { get; private set; }
        public ToggleWidget FollowCamera { get; private set; }
        public ToggleWidget HideInARMode { get; private set; }
        public ToggleWidget Interactable { get; private set; }
        public Vector3SliderWidget Offset { get; private set; }
        public ColorPickerWidget ColorPicker { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Grid");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            ShowInterior = p.AddToggle("Show Interior").SetValue(true);
            FollowCamera = p.AddToggle("Follow Camera").SetValue(true);
            Interactable = p.AddToggle("Enable Collider").SetValue(true);
            HideInARMode = p.AddToggle("Hide in AR Mode").SetValue(true);
            Offset = p.AddVector3Slider("Offset");
            ColorPicker = p.AddColorPicker("Grid Color").SetValue(Color.gray);

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}