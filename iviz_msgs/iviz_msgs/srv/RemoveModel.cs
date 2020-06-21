using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/RemoveModel")]
    public sealed class RemoveModel : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public RemoveModelRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public RemoveModelResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public RemoveModel()
        {
            Request = new RemoveModelRequest();
            Response = new RemoveModelResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public RemoveModel(RemoveModelRequest request)
        {
            Request = request;
            Response = new RemoveModelResponse();
        }
        
        IService IService.Create() => new RemoveModel();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (RemoveModelRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (RemoveModelResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/RemoveModel";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "2fd339ea2d03285fde50359ddd9709b9";
    }

    public sealed class RemoveModelRequest : IRequest
    {
        [DataMember (Name = "uri")] public string Uri { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public RemoveModelRequest()
        {
            Uri = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public RemoveModelRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal RemoveModelRequest(Buffer b)
        {
            Uri = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new RemoveModelRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            if (Uri is null) throw new System.NullReferenceException();
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

    public sealed class RemoveModelResponse : IResponse
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
        [DataMember (Name = "message")] public string Message { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public RemoveModelResponse()
        {
            Message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public RemoveModelResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal RemoveModelResponse(Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new RemoveModelResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
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
