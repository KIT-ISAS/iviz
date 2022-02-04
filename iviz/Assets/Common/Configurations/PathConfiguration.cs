using System;
using System.Runtime.Serialization;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class PathConfiguration : IConfigurationWithTopic
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Path;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public float LineWidth { get; set; } = 0.01f;
        [DataMember] public bool FramesVisible { get; set; } = false;
        [DataMember] public float FrameSize { get; set; } = 0.125f;
        [DataMember] public bool LinesVisible { get; set; } = true;
        [DataMember] public SerializableColor LineColor { get; set; } = Color.yellow;
    }
}