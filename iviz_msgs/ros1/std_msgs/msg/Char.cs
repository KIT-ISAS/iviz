/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Char : IHasSerializer<Char>, IMessage
    {
        [DataMember (Name = "data")] public sbyte Data;
    
        public Char()
        {
        }
        
        public Char(sbyte Data)
        {
            this.Data = Data;
        }
        
        public Char(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public Char(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public Char RosDeserialize(ref ReadBuffer b) => new Char(ref b);
        
        public Char RosDeserialize(ref ReadBuffer2 b) => new Char(ref b);
    
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
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 1;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c + Ros2FixedMessageLength;
        
    
        public const string MessageType = "std_msgs/Char";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1bf77f25acecdedba0e224b162199717";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vOSCxSSEksSeQCADeiGH4KAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Char> CreateSerializer() => new Serializer();
        public Deserializer<Char> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Char>
        {
            public override void RosSerialize(Char msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Char msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Char _) => RosFixedMessageLength;
            public override int Ros2MessageLength(Char _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<Char>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Char msg) => msg = new Char(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Char msg) => msg = new Char(ref b);
        }
    }
}
