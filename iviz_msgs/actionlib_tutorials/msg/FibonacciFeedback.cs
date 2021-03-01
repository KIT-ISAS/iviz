/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/FibonacciFeedback")]
    public sealed class FibonacciFeedback : IDeserializable<FibonacciFeedback>, IFeedback<FibonacciActionFeedback>
    {
        //feedback
        [DataMember (Name = "sequence")] public int[] Sequence { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public FibonacciFeedback()
        {
            Sequence = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciFeedback(int[] Sequence)
        {
            this.Sequence = Sequence;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FibonacciFeedback(ref Buffer b)
        {
            Sequence = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FibonacciFeedback(ref b);
        }
        
        FibonacciFeedback IDeserializable<FibonacciFeedback>.RosDeserialize(ref Buffer b)
        {
            return new FibonacciFeedback(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b81e37d2a31925a0e8ae261a8699cb79";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PKzCsxNoqOVShOLSxNzUtO5eICAHPWhAoTAAAA";
                
    }
}
