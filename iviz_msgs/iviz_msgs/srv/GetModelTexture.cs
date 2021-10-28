using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/GetModelTexture")]
    public sealed class GetModelTexture : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetModelTextureRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetModelTextureResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetModelTexture()
        {
            Request = new GetModelTextureRequest();
            Response = new GetModelTextureResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/GetModelTexture";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "0d382728fb593e7fac7232b27f8a271f";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModelTextureRequest : IRequest<GetModelTexture, GetModelTextureResponse>, IDeserializable<GetModelTextureRequest>
    {
        [DataMember (Name = "uri")] public string Uri;
    
        /// <summary> Constructor for empty message. </summary>
        public GetModelTextureRequest()
        {
            Uri = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetModelTextureRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetModelTextureRequest(ref Buffer b)
        {
            Uri = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetModelTextureRequest(ref b);
        }
        
        GetModelTextureRequest IDeserializable<GetModelTextureRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetModelTextureRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Uri);
        }
        
        public void Dispose()
        {
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
    
        /// <summary> Constructor for empty message. </summary>
        public GetModelTextureResponse()
        {
            Image = new SensorMsgs.CompressedImage();
            Message = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetModelTextureResponse(bool Success, SensorMsgs.CompressedImage Image, string Message)
        {
            this.Success = Success;
            this.Image = Image;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetModelTextureResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Image = new SensorMsgs.CompressedImage(ref b);
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetModelTextureResponse(ref b);
        }
        
        GetModelTextureResponse IDeserializable<GetModelTextureResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetModelTextureResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            Image.RosSerialize(ref b);
            b.Serialize(Message);
        }
        
        public void Dispose()
        {
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
