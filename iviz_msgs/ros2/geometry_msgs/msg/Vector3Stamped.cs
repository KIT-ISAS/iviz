/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    public sealed class Vector3Stamped : IDeserializable<Vector3Stamped>, IMessageRos2
    {
        // This represents a Vector3 with reference coordinate frame and timestamp
        // Note that this follows vector semantics with it always anchored at the origin,
        // so the rotational elements of a transform are the only parts applied when transforming.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "vector")] public Vector3 Vector;
    
        /// Constructor for empty message.
        public Vector3Stamped()
        {
        }
        
        /// Explicit constructor.
        public Vector3Stamped(in StdMsgs.Header Header, in Vector3 Vector)
        {
            this.Header = Header;
            this.Vector = Vector;
        }
        
        /// Constructor with buffer.
        public Vector3Stamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Vector);
        }
        
        public Vector3Stamped RosDeserialize(ref ReadBuffer2 b) => new Vector3Stamped(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Vector);
        }
        
        public void RosValidate()
        {
        }
    
        public void GetRosMessageLength(ref int c)
        {
            Header.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, Vector);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Vector3Stamped";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
