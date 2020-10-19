using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/GetModelResource")]
    public sealed class GetModelResource : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetModelResourceRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetModelResourceResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetModelResource()
        {
            Request = new GetModelResourceRequest();
            Response = new GetModelResourceResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/GetModelResource";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "02c42b3fdf08f126e5fad7a629a839e0";
    }

    public sealed class GetModelResourceRequest : IRequest
    {
        // Retrieves a 3D model, and converts it into a format that can be used in iviz
        [DataMember (Name = "uri")] public string Uri { get; set; } // Uri of the file. Example: package://some_package/file.dae
    
        /// <summary> Constructor for empty message. </summary>
        public GetModelResourceRequest()
        {
            Uri = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetModelResourceRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetModelResourceRequest(ref Buffer b)
        {
            Uri = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetModelResourceRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
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

    public sealed class GetModelResourceResponse : IResponse
    {
        [DataMember (Name = "success")] public bool Success { get; set; } // Whether the retrieval succeeded
        [DataMember (Name = "model")] public Model Model { get; set; } // The 3D model
        [DataMember (Name = "message")] public string Message { get; set; } // An error message if success is false
    
        /// <summary> Constructor for empty message. </summary>
        public GetModelResourceResponse()
        {
            Model = new Model();
            Message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetModelResourceResponse(bool Success, Model Model, string Message)
        {
            this.Success = Success;
            this.Model = Model;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetModelResourceResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Model = new Model(ref b);
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetModelResourceResponse(ref b);
        }
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += Model.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Message);
                return size;
            }
        }
    }
}
