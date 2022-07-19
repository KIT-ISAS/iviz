/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsGoal : IDeserializableRos1<TwoIntsGoal>, IMessageRos1, IGoal<TwoIntsActionGoal>
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
        public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib/TwoIntsGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "36d09b846be0b371c5f190354dd3153e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxM1FI5ILQSVwAD962hBEAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
