using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/GetSdf")]
    public sealed class GetSdf : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetSdfRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetSdfResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetSdf()
        {
            Request = new GetSdfRequest();
            Response = new GetSdfResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/GetSdf";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "920692c7a5a3dd3789f2d8f444a0bf8f";
    }

    public sealed class GetSdfRequest : IRequest
    {
        // Retrieves a scene, which can contain one or multiple 3D models and lights
        [DataMember (Name = "uri")] public string Uri { get; set; } // Uri of the file. Example: package://some_package/file.world
    
        /// <summary> Constructor for empty message. </summary>
        public GetSdfRequest()
        {
            Uri = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetSdfRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetSdfRequest(Buffer b)
        {
            Uri = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetSdfRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            if (Uri is null) throw new System.NullReferenceException(nameof(Uri));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Uri);
                return size;
            }
        }
    }

    public sealed class GetSdfResponse : IResponse
    {
        [DataMember (Name = "success")] public bool Success { get; set; } // Whether the retrieval succeeded
        [DataMember (Name = "scene")] public Scene Scene { get; set; } // The scene
        [DataMember (Name = "message")] public string Message { get; set; } // An error message if success is false
    
        /// <summary> Constructor for empty message. </summary>
        public GetSdfResponse()
        {
            Scene = new Scene();
            Message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetSdfResponse(bool Success, Scene Scene, string Message)
        {
            this.Success = Success;
            this.Scene = Scene;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetSdfResponse(Buffer b)
        {
            Success = b.Deserialize<bool>();
            Scene = new Scene(b);
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetSdfResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Success);
            Scene.RosSerialize(b);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Scene is null) throw new System.NullReferenceException(nameof(Scene));
            Scene.RosValidate();
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += Scene.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Message);
                return size;
            }
        }
    }
}
