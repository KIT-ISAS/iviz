/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    public sealed class AccelWithCovarianceStamped : IDeserializableRos2<AccelWithCovarianceStamped>, IMessageRos2
    {
        // This represents an estimated accel with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "accel")] public AccelWithCovariance Accel;
    
        /// Constructor for empty message.
        public AccelWithCovarianceStamped()
        {
            Accel = new AccelWithCovariance();
        }
        
        /// Explicit constructor.
        public AccelWithCovarianceStamped(in StdMsgs.Header Header, AccelWithCovariance Accel)
        {
            this.Header = Header;
            this.Accel = Accel;
        }
        
        /// Constructor with buffer.
        public AccelWithCovarianceStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new AccelWithCovariance(ref b);
        }
        
        public AccelWithCovarianceStamped RosDeserialize(ref ReadBuffer2 b) => new AccelWithCovarianceStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Accel.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Accel is null) BuiltIns.ThrowNullReference();
            Accel.RosValidate();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            Accel.AddRosMessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/AccelWithCovarianceStamped";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
