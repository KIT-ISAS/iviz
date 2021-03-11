/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/CostSource")]
    public sealed class CostSource : IDeserializable<CostSource>, IMessage
    {
        // The density of the cost source
        [DataMember (Name = "cost_density")] public double CostDensity { get; set; }
        // The volume of the cost source is represented as an
        // axis-aligned bounding box (AABB)
        // The AABB is specified by two of its opposite corners
        [DataMember (Name = "aabb_min")] public GeometryMsgs.Vector3 AabbMin { get; set; }
        [DataMember (Name = "aabb_max")] public GeometryMsgs.Vector3 AabbMax { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public CostSource()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public CostSource(double CostDensity, in GeometryMsgs.Vector3 AabbMin, in GeometryMsgs.Vector3 AabbMax)
        {
            this.CostDensity = CostDensity;
            this.AabbMin = AabbMin;
            this.AabbMax = AabbMax;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public CostSource(ref Buffer b)
        {
            CostDensity = b.Deserialize<double>();
            AabbMin = new GeometryMsgs.Vector3(ref b);
            AabbMax = new GeometryMsgs.Vector3(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new CostSource(ref b);
        }
        
        CostSource IDeserializable<CostSource>.RosDeserialize(ref Buffer b)
        {
            return new CostSource(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(CostDensity);
            AabbMin.RosSerialize(ref b);
            AabbMax.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 56;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/CostSource";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "abb7e013237dacaaa8b97e704102f908";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2SQWvcQAyF7wP7Hx7kksDGhab0EMghuZQcCoWWXBfZlp2h9siM5Oy6v74a77JtIOQU" +
                "n2SNvqcnzVzg1zOj5aTRFkgH899G1KAy54ZDNwjZ1y9rbneqC+FixV5kmEd+g0JUZJ4yKyfjFqSg5BAd" +
                "ol7TEPvkyVrm1MbUe3DA5f39w8PVSbfERUInbmIXS+0C20vpFE0h0yTuo7TMibOG0LOMbHnZjdrrpydu" +
                "TPINiOp6N8b0/jkdwibcffC3Cd9/frvFm30365j/r8jXg5f1EDGhy8w+OzVcwUsfrexC0rBgZEoGk3+k" +
                "g23MjkZJVVle5k4yb31PaIUVScw1Rvrtkn57XGiaJhcjWKakAxW2pB255Kqvttg/czpWlfuh4qJn33Rs" +
                "kGMf2yPpjcYzTDhNt4V1n7GPw3D0fGzmD8RFstgKXFV47LDIjH0ZyIOMlswdCWq3ePJF9VD8yhZzMb5K" +
                "vN7oD4nOj6xKvb+6pMbUVuH8ag/naDlHfzbhL1WeDMH2AgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
