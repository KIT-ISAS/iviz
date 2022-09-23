/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciResult : IHasSerializer<FibonacciResult>, IMessage, IResult<FibonacciActionResult>
    {
        //result definition
        [DataMember (Name = "sequence")] public int[] Sequence;
    
        public FibonacciResult()
        {
            Sequence = EmptyArray<int>.Value;
        }
        
        public FibonacciResult(int[] Sequence)
        {
            this.Sequence = Sequence;
        }
        
        public FibonacciResult(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<int>.Value
                    : new int[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Sequence = array;
            }
        }
        
        public FibonacciResult(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<int>.Value
                    : new int[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Sequence = array;
            }
        }
        
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += 4 * Sequence.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Sequence.Length
            size += 4 * Sequence.Length;
            return size;
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
    
        public Serializer<FibonacciResult> CreateSerializer() => new Serializer();
        public Deserializer<FibonacciResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<FibonacciResult>
        {
            public override void RosSerialize(FibonacciResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(FibonacciResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(FibonacciResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(FibonacciResult msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(FibonacciResult msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<FibonacciResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out FibonacciResult msg) => msg = new FibonacciResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out FibonacciResult msg) => msg = new FibonacciResult(ref b);
        }
    }
}
