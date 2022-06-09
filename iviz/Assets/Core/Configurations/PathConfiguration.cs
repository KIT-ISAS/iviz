using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.Core.Configurations
{
    [DataContract]
    public sealed class PathConfiguration : JsonToString, IConfigurationWithTopic
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
        [DataMember] public ColorRGBA LineColor { get; set; } = ColorRGBA.Yellow;
    }
}