using System;
using System.Runtime.Serialization;
using Iviz.Core;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class MagnitudeConfiguration : IConfigurationWithType
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Magnitude;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public bool TrailVisible { get; set; } = false;
        [DataMember] public bool AngleVisible { get; set; } = true;
        [DataMember] public bool FrameVisible { get; set; } = true;
        [DataMember] public float Scale { get; set; } = 1.0f;
        [DataMember] public bool PreferUdp { get; set; } = true;
        [DataMember] public bool VectorVisible { get; set; } = true;
        [DataMember] public float VectorScale { get; set; } = 1.0f;
        [DataMember] public float ScaleMultiplierPow10 { get; set; } = 0;
        [DataMember] public SerializableColor Color { get; set; } = UnityEngine.Color.red;
        [DataMember] public float TrailTime { get; set; } = 2.0f;
    }
}