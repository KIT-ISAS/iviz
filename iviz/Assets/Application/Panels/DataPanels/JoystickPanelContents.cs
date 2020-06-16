
namespace Iviz.App
{
    /// <summary>
    /// <see cref="JoystickModuleData"/> 
    /// </summary>
    public class JoystickPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        public ToggleWidget SendJoy { get; private set; }
        public ToggleWidget SendTwist { get; private set; }
        public Vector3Widget MaxSpeed { get; private set; }
        public ToggleWidget XIsFront { get; private set; }
        public InputFieldWidget AttachToFrame { get; private set; }

        public SenderWidget JoySender { get; private set; }
        public SenderWidget TwistSender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Joystick");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            SendJoy = p.AddToggle("Publish Joy Message");
            SendTwist = p.AddToggle("Publish Twist Message");
            MaxSpeed = p.AddVector3("Max Speed [X, Y, Angular]");
            XIsFront = p.AddToggle("X is Front / Up");
            AttachToFrame = p.AddInputField("Attach to TF Frame");

            JoySender = p.AddSender();
            TwistSender = p.AddSender();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}