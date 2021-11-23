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
            Service = string.Empty;
        }
        
        /// Explicit constructor.
        public ServiceTypeRequest(string Service)
        {
            this.Service = Service;
        }
        
        /// Constructor with buffer.
        internal ServiceTypeRequest(ref Buffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ServiceTypeRequest(ref b);
        
        ServiceTypeRequest IDeserializable<ServiceTypeRequest>.RosDeserialize(ref Buffer b) => new ServiceTypeRequest(ref b);
    
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
    public sealed class ServiceTypeResponse : IResponse, IDeserializable<ServiceTypeResponse>
    {
        [DataMember (Name = "type")] public string Type;
    
        /// Constructor for empty message.
        public ServiceTypeResponse()
        {
            Type = string.Empty;
        }
        
        /// Explicit constructor.
        public ServiceTypeResponse(string Type)
        {
            this.Type = Type;
        }
        
        /// Constructor with buffer.
        internal ServiceTypeResponse(ref Buffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ServiceTypeResponse(ref b);
        
        ServiceTypeResponse IDeserializable<ServiceTypeResponse>.RosDeserialize(ref Buffer b) => new ServiceTypeResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
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
