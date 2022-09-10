/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt32 : IHasSerializer<UInt32>, IMessage
    {
        [DataMember (Name = "data")] public uint Data;
    
        public UInt32()
        {
        }
        
        public UInt32(uint Data)
        {
            this.Data = Data;
        }
        
        public UInt32(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public UInt32(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Data);
        }
        
        public UInt32 RosDeserialize(ref ReadBuffer b) => new UInt32(ref b);
        
        public UInt32 RosDeserialize(ref ReadBuffer2 b) => new UInt32(ref b);
    
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
    
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 4;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "std_msgs/UInt32";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "304a39449588c7f8ce2df6e8001c5fce";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCsxNlJISSxJ5AIAYOk1nQwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<UInt32> CreateSerializer() => new Serializer();
        public Deserializer<UInt32> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<UInt32>
        {
            public override void RosSerialize(UInt32 msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(UInt32 msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(UInt32 _) => RosFixedMessageLength;
            public override int Ros2MessageLength(UInt32 _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<UInt32>
        {
            public override void RosDeserialize(ref ReadBuffer b, out UInt32 msg) => msg = new UInt32(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out UInt32 msg) => msg = new UInt32(ref b);
        }
    }
}
