using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class ServiceNode : IService
    {
        /// Request message.
        [DataMember] public ServiceNodeRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ServiceNodeResponse Response { get; set; }
        
        /// Empty constructor.
        public ServiceNode()
        {
            Request = new ServiceNodeRequest();
            Response = new ServiceNodeResponse();
        }
        
        /// Setter constructor.
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/ServiceNode";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "bd2a0a45fd7a73a86c8d6051d5a6db8a";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceNodeRequest : IRequest<ServiceNode, ServiceNodeResponse>, IDeserializable<ServiceNodeRequest>
    {
        [DataMember (Name = "service")] public string Service;
    
        /// Constructor for empty message.
        public ServiceNodeRequest()
        {
            Service = string.Empty;
        }
        
        /// Explicit constructor.
        public ServiceNodeRequest(string Service)
        {
            this.Service = Service;
        }
        
        /// Constructor with buffer.
        internal ServiceNodeRequest(ref Buffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ServiceNodeRequest(ref b);
        
        ServiceNodeRequest IDeserializable<ServiceNodeRequest>.RosDeserialize(ref Buffer b) => new ServiceNodeRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Service);
        }
        
        public void RosValidate()
        {
            if (Service is null) throw new System.NullReferenceException(nameof(Service));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Service);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceNodeResponse : IResponse, IDeserializable<ServiceNodeResponse>
    {
        [DataMember (Name = "node")] public string Node;
    
        /// Constructor for empty message.
        public ServiceNodeResponse()
        {
            Node = string.Empty;
        }
        
        /// Explicit constructor.
        public ServiceNodeResponse(string Node)
        {
            this.Node = Node;
        }
        
        /// Constructor with buffer.
        internal ServiceNodeResponse(ref Buffer b)
        {
            Node = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ServiceNodeResponse(ref b);
        
        ServiceNodeResponse IDeserializable<ServiceNodeResponse>.RosDeserialize(ref Buffer b) => new ServiceNodeResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Node);
        }
        
        public void RosValidate()
        {
            if (Node is null) throw new System.NullReferenceException(nameof(Node));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Node);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
