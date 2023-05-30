
namespace Iviz.App
{
    /// <summary>
    /// <see cref="JoystickModuleData"/> 
    /// </summary>
    public sealed class JoystickModulePanel : ModulePanel
    {
        public SimpleButtonWidget CloseButton { get; private set; }

        public DropdownWidget Mode { get; private set; }
        public ToggleWidget SendJoy { get; private set; }
        public InputFieldWithHintsWidget JoyTopic { get; private set; }
        public SenderWidget JoySender { get; private set; }

        public ToggleWidget SendTwist { get; private set; }
        public Vector3MultiWidget MaxSpeed { get; private set; }
        public ToggleWidget XIsFront { get; private set; }


        public InputFieldWithHintsWidget TwistTopic { get; private set; }
        public ToggleWidget UseStamped { get; private set; }
        public InputFieldWithHintsWidget AttachToFrame { get; private set; }
        public SenderWidget TwistSender { get; private set; }

        protected override void Initialize()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Joystick");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            Mode = p.AddDropdown("Mode").SetOptions(new[]
            {
                "Left Joystick",
                "Right Joystick",
                "Two Joysticks",
                "Four Joysticks"
            });
            
            SendJoy = p.AddToggle("Publish Joy Message");
            JoyTopic = p.AddInputFieldWithHints("Joy Topic").SetPlaceholder("joy");
            JoySender = p.AddSender();

            SendTwist = p.AddToggle("Publish Twist Message");
            MaxSpeed = p.AddVector3Multi("Max Speed [X, Y, Angular]").SetRange(5);
            XIsFront = p.AddToggle("Joystick Right is Robot Front");

            UseStamped = p.AddToggle("Use TwistStamped instead of Twist");
            TwistTopic = p.AddInputFieldWithHints("Twist Topic").SetPlaceholder("twist");
            AttachToFrame = p.AddInputFieldWithHints("TF Frame For Header").SetPlaceholder("map");

            TwistSender = p.AddSender();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}