
namespace Iviz.App
{
    /// <summary>
    /// <see cref="JoystickModuleData"/> 
    /// </summary>
    public sealed class JoystickPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        public ToggleWidget SendJoy { get; private set; }
        public InputFieldWidget JoyTopic { get; private set; }
        public SenderWidget JoySender { get; private set; }

        public ToggleWidget SendTwist { get; private set; }
        public Vector3Widget MaxSpeed { get; private set; }
        public ToggleWidget XIsFront { get; private set; }


        public InputFieldWithHintsWidget TwistTopic { get; private set; }
        public ToggleWidget UseStamped { get; private set; }
        public InputFieldWithHintsWidget AttachToFrame { get; private set; }
        public SenderWidget TwistSender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Joystick");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            SendJoy = p.AddToggle("Publish Joy Message");
            JoyTopic = p.AddInputField("Joy Topic").SetPlaceholder("joy");
            JoySender = p.AddSender();

            SendTwist = p.AddToggle("Publish Twist Message");
            MaxSpeed = p.AddVector3("Max Speed [X, Y, Angular]");
            XIsFront = p.AddToggle("Joy Right is Robot Front");

            UseStamped = p.AddToggle("Use TwistStamped instead of Twist");
            TwistTopic = p.AddInputFieldWithHints("Twist Topic").SetPlaceholder("twist");
            AttachToFrame = p.AddInputFieldWithHints("TF Frame For Header").SetPlaceholder("map");

            TwistSender = p.AddSender();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}