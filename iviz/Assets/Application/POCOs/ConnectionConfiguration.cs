﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Core;
using JetBrains.Annotations;

namespace Iviz.App
{
    [DataContract]
    public sealed class ConnectionConfiguration
    {
        [NotNull, DataMember] public string MasterUri { get; set; } = "";
        [NotNull, DataMember] public string MyUri { get; set; } = "";
        [NotNull, DataMember] public string MyId { get; set; } = "";
        [NotNull, DataMember] public List<Uri> LastMasterUris { get; set; } = new List<Uri>();
        [NotNull, DataMember] public SettingsConfiguration Settings { get; set; } = new SettingsConfiguration();
    }
}
