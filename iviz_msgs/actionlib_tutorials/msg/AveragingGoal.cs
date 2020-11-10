/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract (Name = "actionlib_tutorials/AveragingGoal")]
    public sealed class AveragingGoal : IDeserializable<AveragingGoal>, IGoal<AveragingActionGoal>
    {
        //goal definition
        [DataMember (Name = "samples")] public int Samples { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingGoal()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingGoal(int Samples)
        {
            this.Samples = Samples;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AveragingGoal(ref Buffer b)
        {
            Samples = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingGoal(ref b);
        }
        
        AveragingGoal IDeserializable<AveragingGoal>.RosDeserialize(ref Buffer b)
        {
            return new AveragingGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Samples);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "32c9b10ef9b253faa93b93f564762c8f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PKzCsxNlIoTswtyEkt5gIAdDD8hw8AAAA=";
                
    }
}
