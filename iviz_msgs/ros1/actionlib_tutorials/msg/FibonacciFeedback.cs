/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciFeedback : IDeserializable<FibonacciFeedback>, IMessage, IFeedback<FibonacciActionFeedback>
    {
        //feedback
        [DataMember (Name = "sequence")] public int[] Sequence;
    
        public FibonacciFeedback()
        {
            Sequence = System.Array.Empty<int>();
        }
        
        public FibonacciFeedback(int[] Sequence)
        {
            this.Sequence = Sequence;
        }
        
        public FibonacciFeedback(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Sequence);
        }
        
        public FibonacciFeedback(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Sequence);
        }
        
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
    
        public const string MessageType = "actionlib_tutorials/FibonacciFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b81e37d2a31925a0e8ae261a8699cb79";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxNoqOVShOLSxNzUtO5eICAHPWhAoTAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
