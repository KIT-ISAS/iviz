/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int8 : IHasSerializer<Int8>, IMessage
    {
        [DataMember (Name = "data")] public sbyte Data;
    
        public Int8()
        {
        }
        
        public Int8(sbyte Data)
        {
            this.Data = Data;
        }
        
        public Int8(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public Int8(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public Int8 RosDeserialize(ref ReadBuffer b) => new Int8(ref b);
        
        public Int8 RosDeserialize(ref ReadBuffer2 b) => new Int8(ref b);
    
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
        
    
        public const string MessageType = "std_msgs/Int8";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "27ffa0c9c4b8fb8492252bcad9e5c57b";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMK7FQSEksSeTiAgDmSq87CwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Int8> CreateSerializer() => new Serializer();
        public Deserializer<Int8> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Int8>
        {
            public override void RosSerialize(Int8 msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Int8 msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Int8 _) => RosFixedMessageLength;
            public override int Ros2MessageLength(Int8 _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<Int8>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Int8 msg) => msg = new Int8(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Int8 msg) => msg = new Int8(ref b);
        }
    }
}
