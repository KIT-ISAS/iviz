using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Common
{
    [DataContract]
    public sealed class InteractiveMarkerConfiguration : RosMessageWrapper<InteractiveMarkerConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/InteractiveMarkerConfiguration";
        
        [DataMember, NotNull] public string Topic { get; set; } = "";
        [DataMember] public bool DescriptionsVisible { get; set; }
        [DataMember, NotNull] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.InteractiveMarker;
        [DataMember] public bool Visible { get; set; } = true; 
    }
}