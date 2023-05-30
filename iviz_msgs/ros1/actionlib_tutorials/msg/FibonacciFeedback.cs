/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciFeedback : IHasSerializer<FibonacciFeedback>, IMessage, IFeedback<FibonacciActionFeedback>
    {
        //feedback
        [DataMember (Name = "sequence")] public int[] Sequence;
    
        public FibonacciFeedback()
        {
            Sequence = EmptyArray<int>.Value;
        }
        
        public FibonacciFeedback(int[] Sequence)
        {
            this.Sequence = Sequence;
        }
        
        public FibonacciFeedback(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                int[] array;
                if (n == 0) array = EmptyArray<int>.Value;
                else
                {
                    array = new int[n];
                    b.DeserializeStructArray(array);
                }
                Sequence = array;
            }
        }
        
        public FibonacciFeedback(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                int[] array;
                if (n == 0) array = EmptyArray<int>.Value;
                else
                {
                    array = new int[n];
                    b.DeserializeStructArray(array);
                }
                Sequence = array;
            }
        }
        
        public FibonacciFeedback RosDeserialize(ref ReadBuffer b) => new FibonacciFeedback(ref b);
        
        public FibonacciFeedback RosDeserialize(ref ReadBuffer2 b) => new FibonacciFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Sequence.Length);
            b.SerializeStructArray(Sequence);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Sequence.Length);
            b.SerializeStructArray(Sequence);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Sequence, nameof(Sequence));
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
    
        public const string MessageType = "actionlib_tutorials/FibonacciFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b81e37d2a31925a0e8ae261a8699cb79";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxNoqOVShOLSxNzUtO5eICAHPWhAoTAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<FibonacciFeedback> CreateSerializer() => new Serializer();
        public Deserializer<FibonacciFeedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<FibonacciFeedback>
        {
            public override void RosSerialize(FibonacciFeedback msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(FibonacciFeedback msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(FibonacciFeedback msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(FibonacciFeedback msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(FibonacciFeedback msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<FibonacciFeedback>
        {
            public override void RosDeserialize(ref ReadBuffer b, out FibonacciFeedback msg) => msg = new FibonacciFeedback(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out FibonacciFeedback msg) => msg = new FibonacciFeedback(ref b);
        }
    }
}
