/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class CostSource : IDeserializable<CostSource>, IMessage
    {
        // The density of the cost source
        [DataMember (Name = "cost_density")] public double CostDensity;
        // The volume of the cost source is represented as an
        // axis-aligned bounding box (AABB)
        // The AABB is specified by two of its opposite corners
        [DataMember (Name = "aabb_min")] public GeometryMsgs.Vector3 AabbMin;
        [DataMember (Name = "aabb_max")] public GeometryMsgs.Vector3 AabbMax;
    
        /// Constructor for empty message.
        public CostSource()
        {
        }
        
        /// Explicit constructor.
        public CostSource(double CostDensity, in GeometryMsgs.Vector3 AabbMin, in GeometryMsgs.Vector3 AabbMax)
        {
            this.CostDensity = CostDensity;
            this.AabbMin = AabbMin;
            this.AabbMax = AabbMax;
        }
        
        /// Constructor with buffer.
        public CostSource(ref ReadBuffer b)
        {
            CostDensity = b.Deserialize<double>();
            b.Deserialize(out AabbMin);
            b.Deserialize(out AabbMax);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new CostSource(ref b);
        
        public CostSource RosDeserialize(ref ReadBuffer b) => new CostSource(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(CostDensity);
            b.Serialize(in AabbMin);
            b.Serialize(in AabbMax);
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
