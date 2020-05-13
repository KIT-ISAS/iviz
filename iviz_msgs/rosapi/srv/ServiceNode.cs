using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
    public sealed class ServiceNode : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ServiceNodeRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ServiceNodeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ServiceNode()
        {
            Request = new ServiceNodeRequest();
            Response = new ServiceNodeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServiceNode(ServiceNodeRequest request)
        {
            Request = request;
            Response = new ServiceNodeResponse();
        }
        
        IService IService.Create() => new ServiceNode();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServiceNodeRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServiceNodeResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/ServiceNode";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "bd2a0a45fd7a73a86c8d6051d5a6db8a";
    }

    public sealed class ServiceNodeRequest : IRequest
    {
        [DataMember] public string service { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceNodeRequest()
        {
            service = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceNodeRequest(string service)
        {
            this.service = service ?? throw new System.ArgumentNullException(nameof(service));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServiceNodeRequest(Buffer b)
        {
            this.service = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new ServiceNodeRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.service);
        }
        
        public void Validate()
        {
            if (service is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(service);
                return size;
            }
        }
    }

    public sealed class ServiceNodeResponse : IResponse
    {
        [DataMember] public string node { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceNodeResponse()
        {
            node = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceNodeResponse(string node)
        {
            this.node = node ?? throw new System.ArgumentNullException(nameof(node));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServiceNodeResponse(Buffer b)
        {
            this.node = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new ServiceNodeResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.node);
        }
        
        public void Validate()
        {
            if (node is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(node);
                return size;
            }
        }
    }
}
