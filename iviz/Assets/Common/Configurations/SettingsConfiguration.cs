#nullable enable

using System.Runtime.Serialization;
using Iviz.Msgs.StdMsgs;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class SettingsConfiguration
    {
        [DataMember] public QualityType QualityInView { get; set; } = QualityType.Ultra;
        [DataMember] public QualityType QualityInAr { get; set; } = QualityType.Ultra;
        [DataMember] public int NetworkFrameSkip { get; set; } = 1;
        [DataMember] public int TargetFps { get; set; } = -1;
    }
}