using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.MsgsWrapper;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class InteractiveMarkerConfiguration : RosMessageWrapper<InteractiveMarkerConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/InteractiveMarkerConfiguration";
        
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public bool DescriptionsVisible { get; set; }
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.InteractiveMarker;
        [DataMember] public bool Visible { get; set; } = true; 
    }
}