using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;

namespace Iviz.App
{
    [DataContract]
    public sealed class ConnectionConfiguration : JsonToString
    {
        [NotNull, DataMember] public string MasterUri { get; set; } = "";
        [NotNull, DataMember] public string MyUri { get; set; } = "";
        [NotNull, DataMember] public string MyId { get; set; } = "";
        [NotNull, DataMember] public List<Uri> LastMasterUris { get; set; } = new List<Uri>();
        [NotNull, DataMember] public SettingsConfiguration Settings { get; set; } = new SettingsConfiguration();
        [NotNull, DataMember] public HostAlias[] HostAliases { get; set; } = Array.Empty<HostAlias>();

        [NotNull, DataMember]
        public ARMarkersConfiguration MarkersConfiguration { get; set; } = new ARMarkersConfiguration();
    }
}