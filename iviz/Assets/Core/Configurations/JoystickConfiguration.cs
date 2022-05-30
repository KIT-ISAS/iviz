using System.Runtime.Serialization;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.Common.Configurations
{
    public enum JoystickMode
    {
        Left,
        Right,
        Two,
        Four
    }
    
    [DataContract]
    public sealed class JoystickConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string JoyTopic { get; set; } = "";
        [DataMember] public bool PublishJoy { get; set; }
        [DataMember] public string TwistTopic { get; set; } = "";
        [DataMember] public bool PublishTwist { get; set; } = true;
        [DataMember] public bool UseTwistStamped { get; set; }
        [DataMember] public Vector3 MaxSpeed { get; set; } = Vector3.One * 0.25f;
        [DataMember] public string AttachToFrame { get; set; } = "";
        [DataMember] public bool XIsFront { get; set; } = true;
        [DataMember] public string Id { get; set; } = nameof(ModuleType.Joystick);
        [DataMember] public JoystickMode Mode { get; set; } = JoystickMode.Two;
        [DataMember] public ModuleType ModuleType => ModuleType.Joystick;
        [DataMember] public bool Visible { get; set; } = true;
    }
}