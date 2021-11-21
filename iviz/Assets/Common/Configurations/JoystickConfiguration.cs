using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Roslib.Utils;
using UnityEngine;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class JoystickConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string JoyTopic { get; set; } = "";
        [DataMember] public bool PublishJoy { get; set; }
        [DataMember] public string TwistTopic { get; set; } = "";
        [DataMember] public bool PublishTwist { get; set; } = true;
        [DataMember] public bool UseTwistStamped { get; set; }
        [DataMember] public SerializableVector3 MaxSpeed { get; set; } = Vector3.one * 0.25f;
        [DataMember] public string AttachToFrame { get; set; } = "";
        [DataMember] public bool XIsFront { get; set; } = true;
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Joystick;
        [DataMember] public bool Visible { get; set; } = true;
    }
}