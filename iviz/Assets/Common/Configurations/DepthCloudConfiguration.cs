using System;
using System.Runtime.Serialization;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class DepthCloudConfiguration : IConfiguration
    {
        [DataMember] public string ColorTopic { get; set; } = "";
        [DataMember] public string DepthTopic { get; set; } = "";
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.DepthCloud;
        [DataMember] public bool Visible { get; set; } = true;
    }
}