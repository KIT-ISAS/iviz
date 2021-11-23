using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetSdf : IService
    {
        /// Request message.
        [DataMember] public GetSdfRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetSdfResponse Response { get; set; }
        
        /// Empty constructor.
        public GetSdf()
        {
            Request = new GetSdfRequest();
            Response = new GetSdfResponse();
        }
        
        /// Setter constructor.
        public GetSdf(GetSdfRequest request)
        {
            Request = request;
            Response = new GetSdfResponse();
        }
        
        IService IService.Create() => new GetSdf();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetSdfRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetSdfResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/GetSdf";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "4268e0641c7ff6b587e46790f433e3ba";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetSdfRequest : IRequest<GetSdf, GetSdfResponse>, IDeserializable<GetSdfRequest>
    {
        // Retrieves a scene, which can contain one or multiple 3D models and lights
        [DataMember (Name = "uri")] public string Uri; // Uri of the file. Example: package://some_package/file.world
    
        /// Constructor for empty message.
        public GetSdfRequest()
        {
            Uri = string.Empty;
        }
        
        /// Explicit constructor.
        public GetSdfRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        /// Constructor with buffer.
        internal GetSdfRequest(ref Buffer b)
        {
            Uri = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetSdfRequest(ref b);
        
        GetSdfRequest IDeserializable<GetSdfRequest>.RosDeserialize(ref Buffer b) => new GetSdfRequest(ref b);
    
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
    public sealed class GetSdfResponse : IResponse, IDeserializable<GetSdfResponse>
    {
        [DataMember (Name = "success")] public bool Success; // Whether the retrieval succeeded
        [DataMember (Name = "scene")] public Scene Scene; // The scene
        [DataMember (Name = "message")] public string Message; // An error message if success is false
    
        /// Constructor for empty message.
        public GetSdfResponse()
        {
            Scene = new Scene();
            Message = string.Empty;
        }
        
        /// Explicit constructor.
        public GetSdfResponse(bool Success, Scene Scene, string Message)
        {
            this.Success = Success;
            this.Scene = Scene;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        internal GetSdfResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Scene = new Scene(ref b);
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetSdfResponse(ref b);
        
        GetSdfResponse IDeserializable<GetSdfResponse>.RosDeserialize(ref Buffer b) => new GetSdfResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            Scene.RosSerialize(ref b);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Scene is null) throw new System.NullReferenceException(nameof(Scene));
            Scene.RosValidate();
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength => 5 + Scene.RosMessageLength + BuiltIns.GetStringSize(Message);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
