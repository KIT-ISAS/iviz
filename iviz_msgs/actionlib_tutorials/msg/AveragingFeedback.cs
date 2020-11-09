/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract (Name = "actionlib_tutorials/AveragingFeedback")]
    public sealed class AveragingFeedback : IDeserializable<AveragingFeedback>, IFeedback<AveragingActionFeedback>
    {
        //feedback
        [DataMember (Name = "sample")] public int Sample { get; set; }
        [DataMember (Name = "data")] public float Data { get; set; }
        [DataMember (Name = "mean")] public float Mean { get; set; }
        [DataMember (Name = "std_dev")] public float StdDev { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingFeedback()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingFeedback(int Sample, float Data, float Mean, float StdDev)
        {
            this.Sample = Sample;
            this.Data = Data;
            this.Mean = Mean;
            this.StdDev = StdDev;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AveragingFeedback(ref Buffer b)
        {
            Sample = b.Deserialize<int>();
            Data = b.Deserialize<float>();
            Mean = b.Deserialize<float>();
            StdDev = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingFeedback(ref b);
        }
        
        AveragingFeedback IDeserializable<AveragingFeedback>.RosDeserialize(ref Buffer b)
        {
            return new AveragingFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Sample);
            b.Serialize(Data);
            b.Serialize(Mean);
            b.Serialize(StdDev);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9e8dfc53c2f2a032ca33fa80ec46fd4f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PKzCsxNlIoTswtyEnlSsvJTwRxUxJLEuGc3NTEPDinuCQlPiW1jAsA2sc+JTgAAAA=";
                
    }
}
