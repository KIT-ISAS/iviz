using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/GetModel")]
    public sealed class GetModel : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetModelRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetModelResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetModel()
        {
            Request = new GetModelRequest();
            Response = new GetModelResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetModel(GetModelRequest request)
        {
            Request = request;
            Response = new GetModelResponse();
        }
        
        IService IService.Create() => new GetModel();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetModelRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetModelResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/GetModel";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "1ecf151077caeb5374628177bdd0c42f";
    }

    public sealed class GetModelRequest : IRequest
    {
        [DataMember (Name = "uri")] public string Uri { get; set; }
        [DataMember (Name = "model")] public Model Model { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetModelRequest()
        {
            Uri = "";
            Model = new Model();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetModelRequest(string Uri, Model Model)
        {
            this.Uri = Uri;
            this.Model = Model;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetModelRequest(Buffer b)
        {
            Uri = b.DeserializeString();
            Model = new Model(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetModelRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Uri);
            Model.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (Uri is null) throw new System.NullReferenceException();
            if (Model is null) throw new System.NullReferenceException();
            Model.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Uri);
                size += Model.RosMessageLength;
                return size;
            }
        }
    }

    public sealed class GetModelResponse : IResponse
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
        [DataMember (Name = "message")] public string Message { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetModelResponse()
        {
            Message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetModelResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetModelResponse(Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetModelResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(Message);
                return size;
            }
        }
    }
}
