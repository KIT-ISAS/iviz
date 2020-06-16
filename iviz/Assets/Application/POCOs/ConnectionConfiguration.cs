using System;
using System.Runtime.Serialization;

namespace Iviz.App
{
    [DataContract]
    public sealed class ConnectionConfiguration
    {
        [DataMember] public Uri MasterUri { get; set; } = null;
        [DataMember] public Uri MyUri { get; set; } = null;
        [DataMember] public string MyId { get; set; } = null;
    }
}
