/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class BoundingBox : IDeserializable<BoundingBox>, IMessage
    {
        [DataMember (Name = "center")] public GeometryMsgs.Pose Center;
        [DataMember (Name = "size")] public GeometryMsgs.Vector3 Size;
    
        /// Constructor for empty message.
        public BoundingBox()
        {
        }
        
        /// Explicit constructor.
        public BoundingBox(in GeometryMsgs.Pose Center, in GeometryMsgs.Vector3 Size)
        {
            this.Center = Center;
            this.Size = Size;
        }
        
        /// Constructor with buffer.
        public BoundingBox(ref ReadBuffer b)
        {
            b.Deserialize(out Center);
            b.Deserialize(out Size);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new BoundingBox(ref b);
        
        public BoundingBox RosDeserialize(ref ReadBuffer b) => new BoundingBox(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Center);
            b.Serialize(in Size);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 80;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "iviz_msgs/BoundingBox";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "727c83f2b037373b8e968433d9c84ecb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71SwUrDQBC971cM9KIQIqh4EDx4kh6EisWrTJPJZjHZibtbY/r1ziZtYrDQizSn2ey8" +
                "t+/NPE1cU3Dde+21v1qxJ8jIBnJKz27eKAvsbsCbHSn18M+fen59ugf9R4tawCM4ahx5EYXBsAUuoIky" +
                "jYXCEYFvMKMEMq7j73x/b/petHJ25oBNQa3Y2DA2qJctilfb80595zIoUsThujRe5MvbxnoIJU36xQvK" +
                "KUqe2VVFxRjubuF7rLqx2p1H/jS6g4dxUV4G/3uec/Hx9DnNvWBXp+qEo0PVnsfbPu3HjMFXfze3JMFa" +
                "wFKW5IFt1UFNKCsLPCEFmBsn0D6G65IciXHJrQmQM3mwHLNQ44dQkpV8CxqbRsgQgkPrq2GU8lsgF5Tq" +
                "NIG2JDt0GaulURg0WXImA2e0yQdknPAIRtibSyAU19Caqho0D49J/ITE8bC4yxSWBXS8hTYaksJBjgEj" +
                "0YZGXbipol5OYBuF9xRHsi5j8R51DIAPhPnJrf8Ad8VqnZ0EAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
