/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwoIntsGoal : IDeserializable<TwoIntsGoal>, IGoal<TwoIntsActionGoal>
    {
        [DataMember (Name = "a")] public long A;
        [DataMember (Name = "b")] public long B;
    
        /// Constructor for empty message.
        public TwoIntsGoal()
        {
        }
        
        /// Explicit constructor.
        public TwoIntsGoal(long A, long B)
        {
            this.A = A;
            this.B = B;
        }
        
        /// Constructor with buffer.
        public TwoIntsGoal(ref ReadBuffer b)
        {
            b.Deserialize(out A);
            b.Deserialize(out B);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TwoIntsGoal(ref b);
        
        public TwoIntsGoal RosDeserialize(ref ReadBuffer b) => new TwoIntsGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(A);
            b.Serialize(B);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "36d09b846be0b371c5f190354dd3153e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PKzCsxM1FI5ILQSVwAD962hBEAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
