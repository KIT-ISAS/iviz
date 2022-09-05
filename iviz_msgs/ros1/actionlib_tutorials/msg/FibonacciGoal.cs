/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciGoal : IDeserializable<FibonacciGoal>, IHasSerializer<FibonacciGoal>, IMessage, IGoal<FibonacciActionGoal>
    {
        //goal definition
        [DataMember (Name = "order")] public int Order;
    
        public FibonacciGoal()
        {
        }
        
        public FibonacciGoal(int Order)
        {
            this.Order = Order;
        }
        
        public FibonacciGoal(ref ReadBuffer b)
        {
            b.Deserialize(out Order);
        }
        
        public FibonacciGoal(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Order);
        }
        
        public FibonacciGoal RosDeserialize(ref ReadBuffer b) => new FibonacciGoal(ref b);
        
        public FibonacciGoal RosDeserialize(ref ReadBuffer2 b) => new FibonacciGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Order);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Order);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 4;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "actionlib_tutorials/FibonacciGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6889063349a00b249bd1661df429d822";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxNlLIL0pJLeICAL/qDR8NAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<FibonacciGoal> CreateSerializer() => new Serializer();
        public Deserializer<FibonacciGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<FibonacciGoal>
        {
            public override void RosSerialize(FibonacciGoal msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(FibonacciGoal msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(FibonacciGoal msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(FibonacciGoal msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<FibonacciGoal>
        {
            public override void RosDeserialize(ref ReadBuffer b, out FibonacciGoal msg) => msg = new FibonacciGoal(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out FibonacciGoal msg) => msg = new FibonacciGoal(ref b);
        }
    }
}
