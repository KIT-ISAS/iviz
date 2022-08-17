#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Core.Configurations;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Roslib.Utils;

namespace Iviz.App
{
    [DataContract]
    public sealed class ConnectionConfiguration : JsonToString
    {
        [DataMember] public string MasterUri { get; set; } = "";
        [DataMember] public string MyUri { get; set; } = "";
        [DataMember] public string MyId { get; set; } = "";
        [DataMember] public List<Uri> LastMasterUris { get; set; } = new();
        [DataMember] public SettingsConfiguration Settings { get; set; } = new();
        [DataMember] public HostAlias?[] HostAliases { get; set; } = Array.Empty<HostAlias?>();
        [DataMember] public RosVersion RosVersion { get; set; }
        [DataMember] public int DomainId { get; set; }
        [DataMember] public Endpoint? DiscoveryServer { get; set; }
        [DataMember] public List<Endpoint?> LastDiscoveryServers { get; set; } = new();
    }
}