using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class TfConfiguration : RosMessageWrapper<TfConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/TfConfiguration";

        [DataMember, NotNull] public string Topic { get; set; } = "";
        [DataMember] public float FrameSize { get; set; } = 0.125f;
        [DataMember] public bool FrameLabelsVisible { get; set; }
        [DataMember] public bool ParentConnectorVisible { get; set; }
        [DataMember] public bool KeepAllFrames { get; set; } = true;
        [DataMember, NotNull] public string Id { get; set; } = "tf";
        [DataMember] public ModuleType ModuleType => ModuleType.TF;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember, NotNull] public HashSet<string> BlacklistedFrames { get; set; } = new HashSet<string>();
        [DataMember, NotNull] public string FixedFrameId { get; set; } = "";
    }
}