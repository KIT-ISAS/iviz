using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class ServiceNode : IService<ServiceNodeRequest, ServiceNodeResponse>
    {
        /// Request message.
        [DataMember] public ServiceNodeRequest Request;
        
        /// Response message.
        [DataMember] public ServiceNodeResponse Response;
        
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
        
        public const string ServiceType = "rosapi_msgs/ServiceNode";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "bd2a0a45fd7a73a86c8d6051d5a6db8a";
        
        public IService Generate() => new ServiceNode();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceNodeRequest : IRequest<ServiceNode, ServiceNodeResponse>, IDeserializable<ServiceNodeRequest>
    {
        [DataMember (Name = "service")] public string Service;
    
        public ServiceNodeRequest()
        {
            Service = "";
        }
        
        public ServiceNodeRequest(string Service)
        {
            this.Service = Service;
        }
        
        public ServiceNodeRequest(ref ReadBuffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ServiceNodeRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Service = b.DeserializeString();
        }
        
        public ServiceNodeRequest RosDeserialize(ref ReadBuffer b) => new ServiceNodeRequest(ref b);
        
        public ServiceNodeRequest RosDeserialize(ref ReadBuffer2 b) => new ServiceNodeRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Service);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Service);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Service, nameof(Service));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Service);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Service);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceNodeResponse : IResponse, IDeserializable<ServiceNodeResponse>
    {
        [DataMember (Name = "node")] public string Node;
    
        public ServiceNodeResponse()
        {
            Node = "";
        }
        
        public ServiceNodeResponse(string Node)
        {
            this.Node = Node;
        }
        
        public ServiceNodeResponse(ref ReadBuffer b)
        {
            Node = b.DeserializeString();
        }
        
        public ServiceNodeResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Node = b.DeserializeString();
        }
        
        public ServiceNodeResponse RosDeserialize(ref ReadBuffer b) => new ServiceNodeResponse(ref b);
        
        public ServiceNodeResponse RosDeserialize(ref ReadBuffer2 b) => new ServiceNodeResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Node);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Node);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Node, nameof(Node));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Node);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Node);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
