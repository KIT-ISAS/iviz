using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.Core.Configurations
{
    [DataContract]
    public sealed class ImageConfiguration : JsonToString, IConfigurationWithTopic
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Image;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public ColormapId Colormap { get; set; } = ColormapId.bone;
        [DataMember] public float MinIntensity { get; set; }
        [DataMember] public float MaxIntensity { get; set; } = 1.0f;
        [DataMember] public bool FlipMinMax { get; set; }
        [DataMember] public bool OverrideMinMax { get; set; }
        [DataMember] public bool EnableBillboard { get; set; }
        [DataMember] public float BillboardSize { get; set; } = 1.0f;
        [DataMember] public bool BillboardFollowCamera { get; set; }
        [DataMember] public bool UseIntrinsicScale { get; set; } = true;
        [DataMember] public Vector3 BillboardOffset { get; set; } = new(0, 0, 0.5f);
    }
}