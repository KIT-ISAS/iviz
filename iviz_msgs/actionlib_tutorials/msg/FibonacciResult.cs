/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class FibonacciResult : IDeserializable<FibonacciResult>, IResult<FibonacciActionResult>
    {
        //result definition
        [DataMember (Name = "sequence")] public int[] Sequence;
    
        /// Constructor for empty message.
        public FibonacciResult()
        {
            Sequence = System.Array.Empty<int>();
        }
        
        /// Explicit constructor.
        public FibonacciResult(int[] Sequence)
        {
            this.Sequence = Sequence;
        }
        
        /// Constructor with buffer.
        public FibonacciResult(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Sequence);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new FibonacciResult(ref b);
        
        public FibonacciResult RosDeserialize(ref ReadBuffer b) => new FibonacciResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Sequence);
        }
        
        public void RosValidate()
        {
            if (Sequence is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 4 * Sequence.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b81e37d2a31925a0e8ae261a8699cb79";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PKzCsxNoqOVShOLSxNzUtO5QIAoiQSixIAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
