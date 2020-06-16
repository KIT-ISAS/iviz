namespace Iviz.App
{
    /// <summary>
    /// <see cref="MarkerModuleData"/> 
    /// </summary>
    public class MarkerPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ColorPickerWidget Tint { get; private set; }
        public SliderWidget Alpha { get; private set; }
        public ToggleWidget OcclusionOnlyMode { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Markers");
            Listener = p.AddListener();

            Tint = p.AddColorPicker("Tint");
            Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);
            OcclusionOnlyMode = p.AddToggle("AR Occlusion Only Mode");

            CloseButton = p.AddTrashButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}