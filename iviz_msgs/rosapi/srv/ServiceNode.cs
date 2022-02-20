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
            Service = "";
        }
        
        /// Explicit constructor.
        public ServiceNodeRequest(string Service)
        {
            this.Service = Service;
        }
        
        /// Constructor with buffer.
        public ServiceNodeRequest(ref ReadBuffer b)
        {
            Service = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ServiceNodeRequest(ref b);
        
        public ServiceNodeRequest RosDeserialize(ref ReadBuffer b) => new ServiceNodeRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
            Node = "";
        }
        
        /// Explicit constructor.
        public ServiceNodeResponse(string Node)
        {
            this.Node = Node;
        }
        
        /// Constructor with buffer.
        public ServiceNodeResponse(ref ReadBuffer b)
        {
            Node = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ServiceNodeResponse(ref b);
        
        public ServiceNodeResponse RosDeserialize(ref ReadBuffer b) => new ServiceNodeResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
