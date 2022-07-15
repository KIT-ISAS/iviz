/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    public sealed class WrenchStamped : IDeserializable<WrenchStamped>, IMessageRos2
    {
        // A wrench with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "wrench")] public Wrench Wrench;
    
        /// Constructor for empty message.
        public WrenchStamped()
        {
            Wrench = new Wrench();
        }
        
        /// Explicit constructor.
        public WrenchStamped(in StdMsgs.Header Header, Wrench Wrench)
        {
            this.Header = Header;
            this.Wrench = Wrench;
        }
        
        /// Constructor with buffer.
        public WrenchStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Wrench = new Wrench(ref b);
        }
        
        public WrenchStamped RosDeserialize(ref ReadBuffer2 b) => new WrenchStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Wrench.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Wrench is null) BuiltIns.ThrowNullReference();
            Wrench.RosValidate();
        }
    
        public void GetRosMessageLength(ref int c)
        {
            Header.GetRosMessageLength(ref c);
            Wrench.GetRosMessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/WrenchStamped";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
