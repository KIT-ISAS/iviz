#nullable enable

using System.Runtime.Serialization;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.App
{
    [DataContract]
    public sealed class XRStartConfiguration : JsonToString
    {
        [DataMember] public Vector3 AnchorPosition { get; set; }
        [DataMember] public Quaternion AnchorOrientation { get; set; }
    }
}