using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.Core.Configurations
{
    [DataContract]
    public sealed class MagnitudeConfiguration : JsonToString, IConfigurationWithTopic
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Magnitude;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public bool TrailVisible { get; set; }
        [DataMember] public bool AngleVisible { get; set; } = true;
        [DataMember] public bool FrameVisible { get; set; } = true;
        [DataMember] public float Scale { get; set; } = 1.0f;
        [DataMember] public bool PreferUdp { get; set; } = false;
        [DataMember] public bool VectorVisible { get; set; } = true;
        [DataMember] public float VectorScale { get; set; } = 1.0f;
        [DataMember] public ColorRGBA VectorColor { get; set; } = ColorRGBA.Red;
        [DataMember] public ColorRGBA AngleColor { get; set; } = ColorRGBA.Yellow;
        [DataMember] public float TrailTime { get; set; } = 2.0f;
    }
}