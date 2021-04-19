using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.MsgsWrapper;
using Iviz.Resources;
using Iviz.Roslib.Utils;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class InteractiveMarkerConfiguration : RosMessageWrapper<InteractiveMarkerConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/InteractiveMarkerConfiguration";
        
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public bool DescriptionsVisible { get; set; }
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.InteractiveMarker;
        [DataMember] public bool Visible { get; set; } = true; 
    }
}