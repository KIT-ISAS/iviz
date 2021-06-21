using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridModuleData"/> 
    /// </summary>
    public sealed class GridPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ColorPickerWidget ColorPicker { get; private set; }
        public ToggleWidget ShowInterior { get; private set; }
        public ToggleWidget FollowCamera { get; private set; }
        public ToggleWidget HideInARMode { get; private set; }
        public Vector3SliderWidget Offset { get; private set; }
        public ToggleWidget PublishLongTapPosition { get; private set; }
        public InputFieldWithHintsWidget TapTopic { get; private set; }
        public SenderWidget Sender { get; private set; }
        public DataLabelWidget LastTapPosition { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Grid");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            ColorPicker = p.AddColorPicker("Grid Color").SetValue(Color.gray);
            ShowInterior = p.AddToggle("Show Interior").SetValue(true);
            FollowCamera = p.AddToggle("Follow Camera").SetValue(true);
            HideInARMode = p.AddToggle("Hide in AR Mode").SetValue(true);
            Offset = p.AddVector3Slider("Offset");
            PublishLongTapPosition = p.AddToggle("Publish Long Tap Position").SetValue(true);
            TapTopic = p.AddInputFieldWithHints("Tap Topic");
            Sender = p.AddSender();
            LastTapPosition = p.AddDataLabel("").SetAlignment(TextAnchor.MiddleCenter).SetHasRichText(true);
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}