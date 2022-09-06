/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Float32 : IDeserializable<Float32>, IHasSerializer<Float32>, IMessage
    {
        [DataMember (Name = "data")] public float Data;
    
        public Float32()
        {
        }
        
        public Float32(float Data)
        {
            this.Data = Data;
        }
        
        public Float32(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public Float32(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Data);
        }
        
        public Float32 RosDeserialize(ref ReadBuffer b) => new Float32(ref b);
        
        public Float32 RosDeserialize(ref ReadBuffer2 b) => new Float32(ref b);
    
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
        
    
        public const string MessageType = "std_msgs/Float32";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "73fcbf46b49191e672908e50842a83d4";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vLyU8sMTZSSEksSeQCAK0qjc8NAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Float32> CreateSerializer() => new Serializer();
        public Deserializer<Float32> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Float32>
        {
            public override void RosSerialize(Float32 msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Float32 msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Float32 _) => RosFixedMessageLength;
            public override int Ros2MessageLength(Float32 _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<Float32>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Float32 msg) => msg = new Float32(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Float32 msg) => msg = new Float32(ref b);
        }
    }
}
