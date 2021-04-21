using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.MsgsWrapper;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class Intrinsic : RosMessageWrapper<Intrinsic>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/Intrinsic";

        [DataMember] public float Fx { get; set; }
        [DataMember] public float Cx { get; set; }
        [DataMember] public float Fy { get; set; }
        [DataMember] public float Cy { get; set; }

        public Intrinsic()
        {
        }

        public Intrinsic(float fx, float cx, float fy, float cy) => (Fx, Cx, Fy, Cy) = (fx, cx, fy, cy);
    }
}