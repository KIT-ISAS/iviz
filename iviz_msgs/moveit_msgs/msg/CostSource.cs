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
                "H4sIAAAAAAAAE62SQUvkUAzH74X5DgEvCmMX3MWD4EEvi4cFQdnrkLZpDdu+lJfUme6n36TjVgXBiz3l" +
                "pfn980/eO4HHJ4KGkrLNIC2YH2tRA5Up17Qp2l7QLn8syd1L4abYFCcL+Sz9NNAHILBCpjGTUjJqABUw" +
                "BYUH1nPsuUuerWRKDafOgwOc3tzc3p79V45DiOhINbccxTPYXqIXm4KMo7iXaJoTZQ1PHclAlufdoJ1+" +
                "+021Sf4OiFW1Gzh9VoGHTVFcf/FX/Hr4eQUftl0GfbsmXxE8L/+AE7SZyKfHmkrw0juLbUjqZxgIk4HJ" +
                "K+lgw9lRllTG+jK1kmnrm4JGSCGJucaAf1zS75CCxnF0MQTLmLTHYCPtyCmVXbmF/ROlY1VcEYaLjnzX" +
                "XEPmjpsj6Y2GFUZ4GW4L1l7Anvv+6PnYzB+Ji2SxBTgr4a6FWSbYx0AeZGjQMIQqWn1h1Ydf2cIUxheJ" +
                "9wu9F3Z+IFXs/OUlNcKmLNa3e1ijeY3+Fv8AiLiga/wCAAA=";
                
    }
}
