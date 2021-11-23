using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetModelResource : IService
    {
        /// Request message.
        [DataMember] public GetModelResourceRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetModelResourceResponse Response { get; set; }
        
        /// Empty constructor.
        public GetModelResource()
        {
            Request = new GetModelResourceRequest();
            Response = new GetModelResourceResponse();
        }
        
        /// Setter constructor.
        public GetModelResource(GetModelResourceRequest request)
        {
            Request = request;
            Response = new GetModelResourceResponse();
        }
        
        IService IService.Create() => new GetModelResource();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetModelResourceRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetModelResourceResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/GetModelResource";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "a67de8e71bc8e03882d5d86e64000b51";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModelResourceRequest : IRequest<GetModelResource, GetModelResourceResponse>, IDeserializable<GetModelResourceRequest>
    {
        // Retrieves a 3D model, and converts it into a format that can be used in iviz
        [DataMember (Name = "uri")] public string Uri; // Uri of the file. Example: package://some_package/file.dae
    
        /// Constructor for empty message.
        public GetModelResourceRequest()
        {
            Uri = string.Empty;
        }
        
        /// Explicit constructor.
        public GetModelResourceRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        /// Constructor with buffer.
        internal GetModelResourceRequest(ref Buffer b)
        {
            Uri = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetModelResourceRequest(ref b);
        
        GetModelResourceRequest IDeserializable<GetModelResourceRequest>.RosDeserialize(ref Buffer b) => new GetModelResourceRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
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
    public sealed class GetModelResourceResponse : IResponse, IDeserializable<GetModelResourceResponse>
    {
        [DataMember (Name = "success")] public bool Success; // Whether the retrieval succeeded
        [DataMember (Name = "model")] public Model Model; // The 3D model
        [DataMember (Name = "message")] public string Message; // An error message if success is false
    
        /// Constructor for empty message.
        public GetModelResourceResponse()
        {
            Model = new Model();
            Message = string.Empty;
        }
        
        /// Explicit constructor.
        public GetModelResourceResponse(bool Success, Model Model, string Message)
        {
            this.Success = Success;
            this.Model = Model;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        internal GetModelResourceResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Model = new Model(ref b);
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetModelResourceResponse(ref b);
        
        GetModelResourceResponse IDeserializable<GetModelResourceResponse>.RosDeserialize(ref Buffer b) => new GetModelResourceResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            Model.RosSerialize(ref b);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Model is null) throw new System.NullReferenceException(nameof(Model));
            Model.RosValidate();
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength => 5 + Model.RosMessageLength + BuiltIns.GetStringSize(Message);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
