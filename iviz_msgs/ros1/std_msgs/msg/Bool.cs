/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Bool : IDeserializable<Bool>, IHasSerializer<Bool>, IMessage
    {
        [DataMember (Name = "data")] public bool Data;
    
        public Bool()
        {
        }
        
        public Bool(bool Data)
        {
            this.Data = Data;
        }
        
        public Bool(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public Bool(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public Bool RosDeserialize(ref ReadBuffer b) => new Bool(ref b);
        
        public Bool RosDeserialize(ref ReadBuffer2 b) => new Bool(ref b);
    
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
        
    
        public const string MessageType = "std_msgs/Bool";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "8b94c1b53db61fb6aed406028ad6332a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vKz89RSEksSeQCAGFR0NcKAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Bool> CreateSerializer() => new Serializer();
        public Deserializer<Bool> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Bool>
        {
            public override void RosSerialize(Bool msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Bool msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Bool msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Bool msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<Bool>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Bool msg) => msg = new Bool(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Bool msg) => msg = new Bool(ref b);
        }
    }
}
