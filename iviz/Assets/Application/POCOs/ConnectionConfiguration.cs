#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Common.Configurations;
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
        [DataMember] public ARMarkersConfiguration MarkersConfiguration { get; set; } = new();
    }
}