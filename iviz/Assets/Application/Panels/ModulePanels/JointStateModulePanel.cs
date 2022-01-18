namespace Iviz.App
{
    public sealed class JointStateModulePanel : ListenerModulePanel
    {
        public DropdownWidget Robot { get; private set; }
        public InputFieldWidget JointPrefix { get; private set; }
        public InputFieldWidget JointSuffix { get; private set; }
        public SliderWidget TrimFromEnd { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("JointState");
            Listener = p.AddListener();
            Robot = p.AddDropdown("Robot");
            JointPrefix = p.AddInputField("Joint Prefix").SetPlaceholder("<none>");
            JointSuffix = p.AddInputField("Joint Suffix").SetPlaceholder("<none>");
            TrimFromEnd = p.AddSlider("Trim End Characters").SetMinValue(0).SetMaxValue(10).SetIntegerOnly(true);
            CloseButton = p.AddTrashButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}