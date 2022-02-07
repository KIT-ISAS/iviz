#nullable enable

using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Roslib.Utils;

namespace Iviz.App
{
    [DataContract]
    public sealed class XRStartConfiguration : JsonToString
    {
        [DataMember] public SerializableVector3 AnchorPosition { get; set; }
        [DataMember] public SerializableQuaternion AnchorOrientation { get; set; }
    }
}