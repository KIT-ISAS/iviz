/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsGoal : IHasSerializer<TwoIntsGoal>, IMessage, IGoal<TwoIntsActionGoal>
    {
        [DataMember (Name = "a")] public long A;
        [DataMember (Name = "b")] public long B;
    
        public TwoIntsGoal()
        {
        }
        
        public TwoIntsGoal(long A, long B)
        {
            this.A = A;
            this.B = B;
        }
        
        public TwoIntsGoal(ref ReadBuffer b)
        {
            b.Deserialize(out A);
            b.Deserialize(out B);
        }
        
        public TwoIntsGoal(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out A);
            b.Deserialize(out B);
        }
        
        public TwoIntsGoal RosDeserialize(ref ReadBuffer b) => new TwoIntsGoal(ref b);
        
        public TwoIntsGoal RosDeserialize(ref ReadBuffer2 b) => new TwoIntsGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(A);
            b.Serialize(B);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align8();
            b.Serialize(A);
            b.Serialize(B);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 16;
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 16;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "actionlib/TwoIntsGoal";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "36d09b846be0b371c5f190354dd3153e";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxM1FI5ILQSVwAD962hBEAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TwoIntsGoal> CreateSerializer() => new Serializer();
        public Deserializer<TwoIntsGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TwoIntsGoal>
        {
            public override void RosSerialize(TwoIntsGoal msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TwoIntsGoal msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TwoIntsGoal _) => RosFixedMessageLength;
            public override int Ros2MessageLength(TwoIntsGoal _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<TwoIntsGoal>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TwoIntsGoal msg) => msg = new TwoIntsGoal(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TwoIntsGoal msg) => msg = new TwoIntsGoal(ref b);
        }
    }
}
