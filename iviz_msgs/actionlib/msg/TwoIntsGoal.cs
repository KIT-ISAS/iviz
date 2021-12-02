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
        internal TwoIntsGoal(ref Buffer b)
        {
            A = b.Deserialize<long>();
            B = b.Deserialize<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TwoIntsGoal(ref b);
        
        TwoIntsGoal IDeserializable<TwoIntsGoal>.RosDeserialize(ref Buffer b) => new TwoIntsGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(A);
            b.Serialize(B);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "36d09b846be0b371c5f190354dd3153e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuPKzCsxM1FI5ILQSVwAD962hBEAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
