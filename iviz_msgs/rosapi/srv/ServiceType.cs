using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class ServiceType : IService
    {
        /// Request message.
        [DataMember] public ServiceTypeRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ServiceTypeResponse Response { get; set; }
        
        /// Empty constructor.
        public ServiceType()
        {
            Request = new ServiceTypeRequest();
            Response = new ServiceTypeResponse();
        }
        
        /// Setter constructor.
        public ServiceType(ServiceTypeRequest request)
        {
            Request = request;
            Response = new ServiceTypeResponse();
        }
        
        IService IService.Create() => new ServiceType();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServiceTypeRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServiceTypeResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/ServiceType";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "0e24a2dcdf70e483afc092a35a1f15f7";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceTypeRequest : IRequest<ServiceType, ServiceTypeResponse>, IDeserializable<ServiceTypeRequest>
    {
        [DataMember (Name = "service")] public string Service;
    
        /// Constructor for empty message.
        public ServiceTypeRequest()
        {
            Service = "";
        }
        
        /// Explicit constructor.
        public ServiceTypeRequest(string Service)
        {
            this.Service = Service;
        }
        
        /// Constructor with buffer.
        public ServiceTypeRequest(ref ReadBuffer b)
        {
            Service = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ServiceTypeRequest(ref b);
        
        public ServiceTypeRequest RosDeserialize(ref ReadBuffer b) => new ServiceTypeRequest(ref b);
    
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
    public sealed class ServiceTypeResponse : IResponse, IDeserializable<ServiceTypeResponse>
    {
        [DataMember (Name = "type")] public string Type;
    
        /// Constructor for empty message.
        public ServiceTypeResponse()
        {
            Type = "";
        }
        
        /// Explicit constructor.
        public ServiceTypeResponse(string Type)
        {
            this.Type = Type;
        }
        
        /// Constructor with buffer.
        public ServiceTypeResponse(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ServiceTypeResponse(ref b);
        
        public ServiceTypeResponse RosDeserialize(ref ReadBuffer b) => new ServiceTypeResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Type);
        }
        
        public void RosValidate()
        {
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Type);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
