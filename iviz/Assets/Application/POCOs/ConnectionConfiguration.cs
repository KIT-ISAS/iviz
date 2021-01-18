using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Core;
using JetBrains.Annotations;

namespace Iviz.App
{
    [DataContract]
    public sealed class ConnectionConfiguration
    {
        [CanBeNull, DataMember] public Uri MasterUri { get; set; }
        [CanBeNull, DataMember] public Uri MyUri { get; set; }
        [CanBeNull, DataMember] public string MyId { get; set; }
        [CanBeNull, DataMember] public List<Uri> LastMasterUris { get; set; }
        [CanBeNull, DataMember] public SettingsConfiguration Settings { get; set; }
    }
}
