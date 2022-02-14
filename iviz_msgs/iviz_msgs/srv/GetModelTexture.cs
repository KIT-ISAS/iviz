using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
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
        
        IService IService.Create() => new GetModelTexture();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/GetModelTexture";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "0d382728fb593e7fac7232b27f8a271f";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModelTextureRequest : IRequest<GetModelTexture, GetModelTextureResponse>, IDeserializable<GetModelTextureRequest>
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
            Uri = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetModelTextureRequest(ref b);
        
        public GetModelTextureRequest RosDeserialize(ref ReadBuffer b) => new GetModelTextureRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            if (Uri is null) throw new System.NullReferenceException(nameof(Uri));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Uri);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModelTextureResponse : IResponse, IDeserializable<GetModelTextureResponse>
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
            Success = b.Deserialize<bool>();
            Image = new SensorMsgs.CompressedImage(ref b);
            Message = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetModelTextureResponse(ref b);
        
        public GetModelTextureResponse RosDeserialize(ref ReadBuffer b) => new GetModelTextureResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            Image.RosSerialize(ref b);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Image is null) throw new System.NullReferenceException(nameof(Image));
            Image.RosValidate();
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength => 5 + Image.RosMessageLength + BuiltIns.GetStringSize(Message);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
