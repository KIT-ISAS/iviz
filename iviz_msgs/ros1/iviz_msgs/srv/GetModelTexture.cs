using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class GetModelTexture : IService
    {
        /// Request message.
        [DataMember] public GetModelTextureRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetModelTextureResponse Response { get; set; }
        
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
    public sealed class GetModelTextureRequest : IRequest<GetModelTexture, GetModelTextureResponse>, IDeserializableRos1<GetModelTextureRequest>
    {
        [DataMember (Name = "uri")] public string Uri;
    
        /// Constructor for empty message.
        public GetModelTextureRequest()
        {
            Uri = "";
        }
        
        /// Explicit constructor.
        public GetModelTextureRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        /// Constructor with buffer.
        public GetModelTextureRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Uri);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetModelTextureRequest(ref b);
        
        public GetModelTextureRequest RosDeserialize(ref ReadBuffer b) => new GetModelTextureRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            if (Uri is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Uri);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModelTextureResponse : IResponse, IDeserializableRos1<GetModelTextureResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "image")] public SensorMsgs.CompressedImage Image;
        [DataMember (Name = "message")] public string Message;
    
        /// Constructor for empty message.
        public GetModelTextureResponse()
        {
            Image = new SensorMsgs.CompressedImage();
            Message = "";
        }
        
        /// Explicit constructor.
        public GetModelTextureResponse(bool Success, SensorMsgs.CompressedImage Image, string Message)
        {
            this.Success = Success;
            this.Image = Image;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        public GetModelTextureResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Image = new SensorMsgs.CompressedImage(ref b);
            b.DeserializeString(out Message);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetModelTextureResponse(ref b);
        
        public GetModelTextureResponse RosDeserialize(ref ReadBuffer b) => new GetModelTextureResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        public override string ToString() => Extensions.ToString(this);
    }
}
