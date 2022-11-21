/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Float64 : IHasSerializer<Float64>, IMessage
    {
        [DataMember (Name = "data")] public double Data;
    
        public Float64()
        {
        }
        
        public Float64(double Data)
        {
            this.Data = Data;
        }
        
        public Float64(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public Float64(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Data);
        }
        
        public Float64 RosDeserialize(ref ReadBuffer b) => new Float64(ref b);
        
        public Float64 RosDeserialize(ref ReadBuffer2 b) => new Float64(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align8();
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "std_msgs/Float64";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "fdb28210bfa9d7c91146260178d9a584";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vLyU8sMTNRSEksSeQCAPMRveQNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Float64> CreateSerializer() => new Serializer();
        public Deserializer<Float64> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Float64>
        {
            public override void RosSerialize(Float64 msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Float64 msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Float64 _) => RosFixedMessageLength;
            public override int Ros2MessageLength(Float64 _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<Float64>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Float64 msg) => msg = new Float64(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Float64 msg) => msg = new Float64(ref b);
        }
    }
}
