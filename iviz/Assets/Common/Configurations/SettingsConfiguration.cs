#nullable enable

using System.Runtime.Serialization;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class SettingsConfiguration
    {
        [DataMember] public QualityType QualityInView { get; set; } = QualityType.Ultra;
        [DataMember] public QualityType QualityInAr { get; set; } = QualityType.Ultra;
        [DataMember] public int NetworkFrameSkip { get; set; } = 1;
        [DataMember] public int TargetFps { get; set; } = -1;
        [DataMember] public SerializableColor BackgroundColor { get; set; } = new Color(0.125f, 0.169f, 0.245f);
        [DataMember] public int SunDirection { get; set; } = 0;
    }
}