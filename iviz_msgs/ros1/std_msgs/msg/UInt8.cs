/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt8 : IHasSerializer<UInt8>, IMessage
    {
        [DataMember (Name = "data")] public byte Data;
    
        public UInt8()
        {
        }
        
        public UInt8(byte Data)
        {
            this.Data = Data;
        }
        
        public UInt8(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public UInt8(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public UInt8 RosDeserialize(ref ReadBuffer b) => new UInt8(ref b);
        
        public UInt8 RosDeserialize(ref ReadBuffer2 b) => new UInt8(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 1;
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 1;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c + Ros2FixedMessageLength;
        
    
        public const string MessageType = "std_msgs/UInt8";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "7c8164229e7d2c17eb95e9231617fdee";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCuxUEhJLEnk4gIAgcsUlwwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<UInt8> CreateSerializer() => new Serializer();
        public Deserializer<UInt8> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<UInt8>
        {
            public override void RosSerialize(UInt8 msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(UInt8 msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(UInt8 _) => RosFixedMessageLength;
            public override int Ros2MessageLength(UInt8 _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<UInt8>
        {
            public override void RosDeserialize(ref ReadBuffer b, out UInt8 msg) => msg = new UInt8(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out UInt8 msg) => msg = new UInt8(ref b);
        }
    }
}
