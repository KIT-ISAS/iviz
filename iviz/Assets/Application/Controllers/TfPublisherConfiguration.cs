using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Core.Configurations;

namespace Iviz.Controllers.TF
{
    [DataContract]
    public sealed class TfPublisherConfiguration : IConfiguration
    {
        [DataMember] public string Id { get; set; } = nameof(ModuleType.TFPublisher);
        [DataMember] public ModuleType ModuleType => ModuleType.TFPublisher;

        [DataMember]
        public TfPublishedFrameConfiguration[] Frames { get; set; } = Array.Empty<TfPublishedFrameConfiguration>();
    }
    
    [DataContract]
    public sealed class TfPublishedFrameConfiguration
    {
        [DataMember] public string Id { get; set; } = "";
        [DataMember] public string Parent { get; set; } = "";
        [DataMember] public Msgs.GeometryMsgs.Pose LocalPose { get; set; }
        [DataMember] public float Scale { get; set; } = 1;
        [DataMember] public bool Visible { get; set; } = true;
    }    
}