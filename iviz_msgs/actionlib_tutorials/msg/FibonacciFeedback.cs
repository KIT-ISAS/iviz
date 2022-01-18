/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class FibonacciFeedback : IDeserializable<FibonacciFeedback>, IFeedback<FibonacciActionFeedback>
    {
        //feedback
        [DataMember (Name = "sequence")] public int[] Sequence;
    
        /// Constructor for empty message.
        public FibonacciFeedback()
        {
            Sequence = System.Array.Empty<int>();
        }
        
        /// Explicit constructor.
        public FibonacciFeedback(int[] Sequence)
        {
            this.Sequence = Sequence;
        }
        
        /// Constructor with buffer.
        public FibonacciFeedback(ref ReadBuffer b)
        {
            Sequence = b.DeserializeStructArray<int>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new FibonacciFeedback(ref b);
        
        public FibonacciFeedback RosDeserialize(ref ReadBuffer b) => new FibonacciFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Sequence);
        }
        
        public void RosValidate()
        {
            if (Sequence is null) throw new System.NullReferenceException(nameof(Sequence));
        }
    
        public int RosMessageLength => 4 + 4 * Sequence.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b81e37d2a31925a0e8ae261a8699cb79";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PKzCsxNoqOVShOLSxNzUtO5eICAHPWhAoTAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
