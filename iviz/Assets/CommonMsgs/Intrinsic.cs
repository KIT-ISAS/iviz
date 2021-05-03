using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.MsgsWrapper;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [DataContract]
    public sealed class Intrinsic : RosMessageWrapper<Intrinsic>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/Intrinsic";

        [DataMember] public double Fx { get; set; }
        [DataMember] public double Cx { get; set; }
        [DataMember] public double Fy { get; set; }
        [DataMember] public double Cy { get; set; }

        public Intrinsic()
        {
        }

        public Intrinsic(double fx, double cx, double fy, double cy) => (Fx, Cx, Fy, Cy) = (fx, cx, fy, cy);
    }
}