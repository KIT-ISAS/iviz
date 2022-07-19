/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    public sealed class AccelStamped : IDeserializableRos2<AccelStamped>, IMessageRos2
    {
        // An accel with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "accel")] public Accel Accel;
    
        /// Constructor for empty message.
        public AccelStamped()
        {
            Accel = new Accel();
        }
        
        /// Explicit constructor.
        public AccelStamped(in StdMsgs.Header Header, Accel Accel)
        {
            this.Header = Header;
            this.Accel = Accel;
        }
        
        /// Constructor with buffer.
        public AccelStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new Accel(ref b);
        }
        
        public AccelStamped RosDeserialize(ref ReadBuffer2 b) => new AccelStamped(ref b);
    
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
        public const string MessageType = "geometry_msgs/AccelStamped";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
