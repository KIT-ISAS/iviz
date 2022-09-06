/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsResult : IDeserializable<TwoIntsResult>, IHasSerializer<TwoIntsResult>, IMessage, IResult<TwoIntsActionResult>
    {
        [DataMember (Name = "sum")] public long Sum;
    
        public TwoIntsResult()
        {
        }
        
        public TwoIntsResult(long Sum)
        {
            this.Sum = Sum;
        }
        
        public TwoIntsResult(ref ReadBuffer b)
        {
            b.Deserialize(out Sum);
        }
        
        public TwoIntsResult(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Sum);
        }
        
        public TwoIntsResult RosDeserialize(ref ReadBuffer b) => new TwoIntsResult(ref b);
        
        public TwoIntsResult RosDeserialize(ref ReadBuffer2 b) => new TwoIntsResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Sum);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Sum);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "actionlib/TwoIntsResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b88405221c77b1878a3cbbfff53428d7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzEzUSguzeUCAHohbPwKAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TwoIntsResult> CreateSerializer() => new Serializer();
        public Deserializer<TwoIntsResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TwoIntsResult>
        {
            public override void RosSerialize(TwoIntsResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TwoIntsResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TwoIntsResult _) => RosFixedMessageLength;
            public override int Ros2MessageLength(TwoIntsResult _) => Ros2FixedMessageLength;
        }
        sealed class Deserializer : Deserializer<TwoIntsResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TwoIntsResult msg) => msg = new TwoIntsResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TwoIntsResult msg) => msg = new TwoIntsResult(ref b);
        }
    }
}
