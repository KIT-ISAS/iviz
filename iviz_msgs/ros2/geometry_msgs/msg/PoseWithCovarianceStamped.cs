/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    public sealed class PoseWithCovarianceStamped : IDeserializableRos2<PoseWithCovarianceStamped>, IMessageRos2
    {
        // This expresses an estimated pose with a reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "pose")] public PoseWithCovariance Pose;
    
        /// Constructor for empty message.
        public PoseWithCovarianceStamped()
        {
            Pose = new PoseWithCovariance();
        }
        
        /// Explicit constructor.
        public PoseWithCovarianceStamped(in StdMsgs.Header Header, PoseWithCovariance Pose)
        {
            this.Header = Header;
            this.Pose = Pose;
        }
        
        /// Constructor with buffer.
        public PoseWithCovarianceStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Pose = new PoseWithCovariance(ref b);
        }
        
        public PoseWithCovarianceStamped RosDeserialize(ref ReadBuffer2 b) => new PoseWithCovarianceStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Pose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Pose is null) BuiltIns.ThrowNullReference();
            Pose.RosValidate();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            Pose.AddRosMessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PoseWithCovarianceStamped";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
