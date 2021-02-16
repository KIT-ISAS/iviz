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
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(CostDensity);
            AabbMin.RosSerialize(ref b);
            AabbMax.RosSerialize(ref b);
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
                "H4sIAAAAAAAAE62SwUrEQAyG7/MUAS8KawUVD4IHvYgHQVC8LmmbdoPtpExSd+vTm+kuVUE82VOayfcn" +
                "+WeO4GVDUFNUtgmkAfPfStRAZUwVhaYTtKvLObc+1IVwNGPv0o09/UIBKyQaEilFoxpQAaNDuGM9xY7b" +
                "6MlSxlhzbD3YwfHt7d3dyUE3x1lCB6q44Vw7gW0ld2JTkGEQnyO3TJGShtCS9GRpWvfa6tkrVSbpAhDL" +
                "ct1z/PscdyHc/PMXHp/vr+HXrvOO3/1xb+B9PgOO0CQiXxwrKsBLHywbIbGboCeMBiZfpIM1J0dZYpGd" +
                "S9RIopWbBLWQQhRzjR7fXNKvjjKNw+BiCJYwaoeZzWlHjqloixVsNxT3VflyME/RktvMFSRuud6T3qhf" +
                "YITDciuw5hy23HX7mffN/HW4SBKbgZMCHhqYZIRtXsiDBDUaZqGSlrmw7PK8soIxDz5L/DT0Sdj5nlSx" +
                "9ScX1QjrIixPdrdE0xJ9hE9Jgca98gIAAA==";
                
    }
}
