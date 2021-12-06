using System;
using System.Runtime.Serialization;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class ImageConfiguration : IConfigurationWithType
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Image;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public ColormapId Colormap { get; set; } = ColormapId.gray;
        [DataMember] public float MinIntensity { get; set; } = 0.0f;
        [DataMember] public float MaxIntensity { get; set; } = 1.0f;
        [DataMember] public bool FlipMinMax { get; set; } = false;
        [DataMember] public bool EnableBillboard { get; set; } = false;
        [DataMember] public float BillboardSize { get; set; } = 1.0f;
        [DataMember] public bool BillboardFollowCamera { get; set; } = false;
        [DataMember] public SerializableVector3 BillboardOffset { get; set; }
    }
}