/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    public sealed class Polygon : IDeserializableRos2<Polygon>, IMessageRos2
    {
        // A specification of a polygon where the first and last points are assumed to be connected
        [DataMember (Name = "points")] public Point32[] Points;
    
        /// Constructor for empty message.
        public Polygon()
        {
            Points = System.Array.Empty<Point32>();
        }
        
        /// Explicit constructor.
        public Polygon(Point32[] Points)
        {
            this.Points = Points;
        }
        
        /// Constructor with buffer.
        public Polygon(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Points);
        }
        
        public Polygon RosDeserialize(ref ReadBuffer2 b) => new Polygon(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Points);
        }
        
        public void RosValidate()
        {
            if (Points is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Points);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Polygon";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
