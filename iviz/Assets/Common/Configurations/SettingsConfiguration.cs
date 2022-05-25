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
        [DataMember] public int NetworkFrameSkip { get; set; } = 1; // every frame
        [DataMember]
        public int TargetFps { get; set; } =
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
            -1; // default for platform
#else
            -2; // vsync
#endif
    }
}