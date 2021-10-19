/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/FibonacciResult")]
    public sealed class FibonacciResult : IDeserializable<FibonacciResult>, IResult<FibonacciActionResult>
    {
        //result definition
        [DataMember (Name = "sequence")] public int[] Sequence;
    
        /// <summary> Constructor for empty message. </summary>
        public FibonacciResult()
        {
            Sequence = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciResult(int[] Sequence)
        {
            this.Sequence = Sequence;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FibonacciResult(ref Buffer b)
        {
            Sequence = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FibonacciResult(ref b);
        }
        
        FibonacciResult IDeserializable<FibonacciResult>.RosDeserialize(ref Buffer b)
        {
            return new FibonacciResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Sequence, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Sequence is null) throw new System.NullReferenceException(nameof(Sequence));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * Sequence.Length;
                return size;
            }
        }
    
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
