#nullable enable

using System;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;

namespace Iviz.Common
{
    [DataContract]
    public sealed class ARMarkersConfiguration : JsonToString
    {
        [DataMember] public float MaxMarkerDistanceInM { get; set; } = 0.5f;
        [DataMember] public ARExecutableMarker[] Markers { get; set; } = Array.Empty<ARExecutableMarker>();
    }
}