#nullable enable

using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.MsgsWrapper;

namespace Iviz.Common
{
    [DataContract]
    public sealed class TfConfiguration : IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/TfConfiguration";

        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public float FrameSize { get; set; } = 0.125f;
        [DataMember] public bool FrameLabelsVisible { get; set; }
        [DataMember] public bool ParentConnectorVisible { get; set; }
        [DataMember] public bool KeepAllFrames { get; set; } = true;
        [DataMember] public bool PreferUdp { get; set; } = true;
        [DataMember] public string Id { get; set; } = "tf";
        [DataMember] public ModuleType ModuleType => ModuleType.TF;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public HashSet<string> BlacklistedFrames { get; set; } = new();
        [DataMember] public string FixedFrameId { get; set; } = "";
        [DataMember] public bool FlipZ { get; set; } = false;
    }
}