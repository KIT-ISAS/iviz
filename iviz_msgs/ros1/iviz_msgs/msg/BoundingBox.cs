/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct BoundingBox : IMessage, IHasSerializer<BoundingBox>
    {
        [DataMember (Name = "center")] public GeometryMsgs.Pose Center;
        [DataMember (Name = "size")] public GeometryMsgs.Vector3 Size;
    
        public BoundingBox(in GeometryMsgs.Pose Center, in GeometryMsgs.Vector3 Size)
        {
            this.Center = Center;
            this.Size = Size;
        }
        
        public BoundingBox(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        public BoundingBox(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly BoundingBox RosDeserialize(ref ReadBuffer b) => new BoundingBox(ref b);
        
        public readonly BoundingBox RosDeserialize(ref ReadBuffer2 b) => new BoundingBox(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 80;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 80;
        
        public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "iviz_msgs/BoundingBox";
    
        public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "727c83f2b037373b8e968433d9c84ecb";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71SwUrDQBC971cM9KIQIqh4EDx4kh6EisWrTJPJZjHZibtbY/r1ziZtYrDQizSn2ey8" +
                "t+/NPE1cU3Dde+21v1qxJ8jIBnJKz27eKAvsbsCbHSn18M+fen59ugf9R4tawCM4ahx5EYXBsAUuoIky" +
                "jYXCEYFvMKMEMq7j73x/b/petHJ25oBNQa3Y2DA2qJctilfb80595zIoUsThujRe5MvbxnoIJU36xQvK" +
                "KUqe2VVFxRjubuF7rLqx2p1H/jS6g4dxUV4G/3uec/Hx9DnNvWBXp+qEo0PVnsfbPu3HjMFXfze3JMFa" +
                "wFKW5IFt1UFNKCsLPCEFmBsn0D6G65IciXHJrQmQM3mwHLNQ44dQkpV8CxqbRsgQgkPrq2GU8lsgF5Tq" +
                "NIG2JDt0GaulURg0WXImA2e0yQdknPAIRtibSyAU19Caqho0D49J/ITE8bC4yxSWBXS8hTYaksJBjgEj" +
                "0YZGXbipol5OYBuF9xRHsi5j8R51DIAPhPnJrf8Ad8VqnZ0EAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<BoundingBox> CreateSerializer() => new Serializer();
        public Deserializer<BoundingBox> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<BoundingBox>
        {
            public override void RosSerialize(BoundingBox msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(BoundingBox msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(BoundingBox _) => RosFixedMessageLength;
            public override int Ros2MessageLength(BoundingBox _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<BoundingBox>
        {
            public override void RosDeserialize(ref ReadBuffer b, out BoundingBox msg) => msg = new BoundingBox(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out BoundingBox msg) => msg = new BoundingBox(ref b);
        }
    }
}
