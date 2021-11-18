#nullable enable

using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Common
{
    [DataContract]
    public sealed class ARExecutableMarker : JsonToString
    {
        [DataMember] public ARMarkerType Type { get; set; }
        [DataMember] public ARMarkerAction Action { get; set; }
        [DataMember] public string Code { get; set; } = "";
        [DataMember] public float SizeInMm { get; set; }
    }
}