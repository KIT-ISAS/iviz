/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract (Name = "actionlib/TwoIntsGoal")]
    public sealed class TwoIntsGoal : IDeserializable<TwoIntsGoal>, IGoal<TwoIntsActionGoal>
    {
        [DataMember (Name = "a")] public long A { get; set; }
        [DataMember (Name = "b")] public long B { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsGoal()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsGoal(long A, long B)
        {
            this.A = A;
            this.B = B;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TwoIntsGoal(ref Buffer b)
        {
            A = b.Deserialize<long>();
            B = b.Deserialize<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsGoal(ref b);
        }
        
        TwoIntsGoal IDeserializable<TwoIntsGoal>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "36d09b846be0b371c5f190354dd3153e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzEzUUjkygTTSVwAvrvOkxAAAAA=";
                
    }
}
