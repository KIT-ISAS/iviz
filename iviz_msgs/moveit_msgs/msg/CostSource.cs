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
        internal CostSource(ref Buffer b)
        {
            CostDensity = b.Deserialize<double>();
            b.Deserialize(out AabbMin);
            b.Deserialize(out AabbMax);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new CostSource(ref b);
        
        CostSource IDeserializable<CostSource>.RosDeserialize(ref Buffer b) => new CostSource(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(CostDensity);
            b.Serialize(ref AabbMin);
            b.Serialize(ref AabbMax);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 56;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/CostSource";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "abb7e013237dacaaa8b97e704102f908";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2SQWvcQAyF7/MrHuSSwMaFJuRQyCG5lBwKhYReF9mWHVF7ZGbk7Dq/vhrvskkh5BSf" +
                "ZI2+pyfNnOHpmdFyzGILtIP5b6PZkHVODYduULKb6zW3PdaFcLZiLzrMI39AQTIST4kzR+MWlEHRIdpL" +
                "vqRB+ujJWufYSuw92OP87u7+/uKoW+IikSdupJNSu8B2WjqJZeg0qfsoLVPklEPoWUe2tGzH3Odvf7gx" +
                "TVcgquvtKPHzc9qHcPvFX/j1+PMHPuy6zvh+P74bvKxnkIguMfvg1HAFL32wsgiNw4KRKRpM30gHW0mO" +
                "isaqbC5xp4k3viS0yhlRzTVG+uuSfnVcaJomFyNYopgHKmxJO3LOVV9tsHvmeKgql0PFRc++ZmmQpJf2" +
                "QHqj8QQTjsNtYN137GQYDp4Pzfx1uEhSW4GLCg8dFp2xKwN5kNCSuSNF7RaPvqgeil/dYC7GV4n/F/pb" +
                "xfmRc6ben1zMxtRW4fRk96doOUWv4R9Jgca98gIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
