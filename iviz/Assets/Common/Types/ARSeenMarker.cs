#nullable enable

using System.Runtime.Serialization;
using Iviz.Roslib.Utils;
using UnityEngine;

namespace Iviz.Common
{
    [DataContract]
    public sealed class ARSeenMarker : JsonToString
    {
        [DataMember] public ARMarkerType Type { get; set; }
        [DataMember] public string Code { get; set; } = "";
        [DataMember] public float SizeInMm { get; set; }
        [DataMember] public float LastSeen { get; set; }
        public Pose UnityPose { get; set; }
    }
}