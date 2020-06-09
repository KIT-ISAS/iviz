using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="RobotDisplayData"/> 
    /// </summary>
    /// 
    public class RobotPanelContents : DataPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public DropdownWidget ResourceType { get; private set; }
        public ToggleWidget AttachToTF { get; private set; }
        public InputFieldWidget FramePrefix { get; private set; }
        public InputFieldWidget FrameSuffix { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Robot");
            Frame = p.AddFrame();
            ResourceType = p.AddDropdown("Resource").SetOptions(Resource.Robots.Names);
            AttachToTF = p.AddToggle("Attach to TF Frames");
            FramePrefix = p.AddInputField("TF Frame Prefix").SetPlaceholder("<none>");
            FrameSuffix = p.AddInputField("TF Frame Suffix").SetPlaceholder("<none>");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}