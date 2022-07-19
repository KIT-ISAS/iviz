/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciFeedback : IDeserializableRos1<FibonacciFeedback>, IDeserializableRos2<FibonacciFeedback>, IMessageRos1, IMessageRos2, IFeedback<FibonacciActionFeedback>
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
            b.DeserializeStructArray(out Sequence);
        }
        
        /// Constructor with buffer.
        public FibonacciFeedback(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Sequence);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new FibonacciFeedback(ref b);
        
        public FibonacciFeedback RosDeserialize(ref ReadBuffer b) => new FibonacciFeedback(ref b);
        
        public FibonacciFeedback RosDeserialize(ref ReadBuffer2 b) => new FibonacciFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Sequence);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Sequence);
        }
        
        public void RosValidate()
        {
            if (Sequence is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 4 * Sequence.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Sequence);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib_tutorials/FibonacciFeedback";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "b81e37d2a31925a0e8ae261a8699cb79";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxNoqOVShOLSxNzUtO5eICAHPWhAoTAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
