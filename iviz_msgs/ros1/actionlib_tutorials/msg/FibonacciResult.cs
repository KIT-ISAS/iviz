/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciResult : IDeserializableCommon<FibonacciResult>, IMessageCommon, IResult<FibonacciActionResult>
    {
        //result definition
        [DataMember (Name = "sequence")] public int[] Sequence;
    
        public FibonacciResult()
        {
            Sequence = System.Array.Empty<int>();
        }
        
        public FibonacciResult(int[] Sequence)
        {
            this.Sequence = Sequence;
        }
        
        public FibonacciResult(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Sequence);
        }
        
        public FibonacciResult(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Sequence);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new FibonacciResult(ref b);
        
        public FibonacciResult RosDeserialize(ref ReadBuffer b) => new FibonacciResult(ref b);
        
        public FibonacciResult RosDeserialize(ref ReadBuffer2 b) => new FibonacciResult(ref b);
    
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
    
        public const string MessageType = "actionlib_tutorials/FibonacciResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b81e37d2a31925a0e8ae261a8699cb79";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxNoqOVShOLSxNzUtO5QIAoiQSixIAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
