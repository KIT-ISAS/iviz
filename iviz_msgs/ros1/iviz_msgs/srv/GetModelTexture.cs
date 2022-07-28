using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class GetModelTexture : IService
    {
        /// Request message.
        [DataMember] public GetModelTextureRequest Request;
        
        /// Response message.
        [DataMember] public GetModelTextureResponse Response;
        
        /// Empty constructor.
        public GetModelTexture()
        {
            Request = new GetModelTextureRequest();
            Response = new GetModelTextureResponse();
        }
        
        /// Setter constructor.
        public GetModelTexture(GetModelTextureRequest request)
        {
            Request = request;
            Response = new GetModelTextureResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetModelTextureRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetModelTextureResponse)value;
        }
        
        public const string ServiceType = "iviz_msgs/GetModelTexture";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "0d382728fb593e7fac7232b27f8a271f";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModelTextureRequest : IRequest<GetModelTexture, GetModelTextureResponse>, IDeserializable<GetModelTextureRequest>
    {
        [DataMember (Name = "uri")] public string Uri;
    
        public GetModelTextureRequest()
        {
            Uri = "";
        }
        
        public GetModelTextureRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        public GetModelTextureRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Uri);
        }
        
        public GetModelTextureRequest(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Uri);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetModelTextureRequest(ref b);
        
        public GetModelTextureRequest RosDeserialize(ref ReadBuffer b) => new GetModelTextureRequest(ref b);
        
        public GetModelTextureRequest RosDeserialize(ref ReadBuffer2 b) => new GetModelTextureRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uri);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            if (Uri is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Uri);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Uri);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModelTextureResponse : IResponse, IDeserializable<GetModelTextureResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "image")] public SensorMsgs.CompressedImage Image;
        [DataMember (Name = "message")] public string Message;
    
        public GetModelTextureResponse()
        {
            Image = new SensorMsgs.CompressedImage();
            Message = "";
        }
        
        public GetModelTextureResponse(bool Success, SensorMsgs.CompressedImage Image, string Message)
        {
            this.Success = Success;
            this.Image = Image;
            this.Message = Message;
        }
        
        public GetModelTextureResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Image = new SensorMsgs.CompressedImage(ref b);
            b.DeserializeString(out Message);
        }
        
        public GetModelTextureResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            Image = new SensorMsgs.CompressedImage(ref b);
            b.DeserializeString(out Message);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetModelTextureResponse(ref b);
        
        public GetModelTextureResponse RosDeserialize(ref ReadBuffer b) => new GetModelTextureResponse(ref b);
        
        public GetModelTextureResponse RosDeserialize(ref ReadBuffer2 b) => new GetModelTextureResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            Image.RosSerialize(ref b);
            b.Serialize(Message);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            Image.RosSerialize(ref b);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Image is null) BuiltIns.ThrowNullReference();
            Image.RosValidate();
            if (Message is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + Image.RosMessageLength + WriteBuffer.GetStringSize(Message);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Success);
            Image.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Message);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
