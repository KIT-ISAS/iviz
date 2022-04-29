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
        /// <summary> Uri of the file. Example: package://some_package/file.dae </summary>
        [DataMember (Name = "uri")] public string Uri;
    
        /// Constructor for empty message.
        public GetModelResourceRequest()
        {
            Uri = "";
        }
        
        /// Explicit constructor.
        public GetModelResourceRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        /// Constructor with buffer.
        public GetModelResourceRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Uri);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetModelResourceRequest(ref b);
        
        public GetModelResourceRequest RosDeserialize(ref ReadBuffer b) => new GetModelResourceRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            if (Uri is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Uri);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModelResourceResponse : IResponse, IDeserializable<GetModelResourceResponse>
    {
        /// <summary> Whether the retrieval succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> The 3D model </summary>
        [DataMember (Name = "model")] public Model Model;
        /// <summary> An error message if success is false </summary>
        [DataMember (Name = "message")] public string Message;
    
        /// Constructor for empty message.
        public GetModelResourceResponse()
        {
            Model = new Model();
            Message = "";
        }
        
        /// Explicit constructor.
        public GetModelResourceResponse(bool Success, Model Model, string Message)
        {
            this.Success = Success;
            this.Model = Model;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        public GetModelResourceResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Model = new Model(ref b);
            b.DeserializeString(out Message);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetModelResourceResponse(ref b);
        
        public GetModelResourceResponse RosDeserialize(ref ReadBuffer b) => new GetModelResourceResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            Model.RosSerialize(ref b);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Model is null) BuiltIns.ThrowNullReference();
            Model.RosValidate();
            if (Message is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + Model.RosMessageLength + BuiltIns.GetStringSize(Message);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
