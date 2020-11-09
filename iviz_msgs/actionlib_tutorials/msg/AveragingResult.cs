/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract (Name = "actionlib_tutorials/AveragingResult")]
    public sealed class AveragingResult : IDeserializable<AveragingResult>, IResult<AveragingActionResult>
    {
        //result definition
        [DataMember (Name = "mean")] public float Mean { get; set; }
        [DataMember (Name = "std_dev")] public float StdDev { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingResult()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingResult(float Mean, float StdDev)
        {
            this.Mean = Mean;
            this.StdDev = StdDev;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AveragingResult(ref Buffer b)
        {
            Mean = b.Deserialize<float>();
            StdDev = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingResult(ref b);
        }
        
        AveragingResult IDeserializable<AveragingResult>.RosDeserialize(ref Buffer b)
        {
            return new AveragingResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Mean);
            b.Serialize(StdDev);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d5c7decf6df75ffb4367a05c1bcc7612";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+NKy8lPLDE2UshNTczjgnGKS1LiU1LLuABqLEBHHgAAAA==";
                
    }
}
