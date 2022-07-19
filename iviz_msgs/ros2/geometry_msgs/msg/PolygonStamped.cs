/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    public sealed class PolygonStamped : IDeserializableRos2<PolygonStamped>, IMessageRos2
    {
        // This represents a Polygon with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "polygon")] public Polygon Polygon;
    
        /// Constructor for empty message.
        public PolygonStamped()
        {
            Polygon = new Polygon();
        }
        
        /// Explicit constructor.
        public PolygonStamped(in StdMsgs.Header Header, Polygon Polygon)
        {
            this.Header = Header;
            this.Polygon = Polygon;
        }
        
        /// Constructor with buffer.
        public PolygonStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Polygon = new Polygon(ref b);
        }
        
        public PolygonStamped RosDeserialize(ref ReadBuffer2 b) => new PolygonStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Polygon.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Polygon is null) BuiltIns.ThrowNullReference();
            Polygon.RosValidate();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            Polygon.AddRosMessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PolygonStamped";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
