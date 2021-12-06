#nullable enable

using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class InteractiveMarkerConfiguration : IConfigurationWithTopic
    {
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public bool DescriptionsVisible { get; set; }
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.InteractiveMarker;
        [DataMember] public bool Visible { get; set; } = true;
    }
}