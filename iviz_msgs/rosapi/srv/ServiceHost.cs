using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class ServiceHost : IService
    {
        /// Request message.
        [DataMember] public ServiceHostRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ServiceHostResponse Response { get; set; }
        
        /// Empty constructor.
        public ServiceHost()
        {
            Request = new ServiceHostRequest();
            Response = new ServiceHostResponse();
        }
        
        /// Setter constructor.
        public ServiceHost(ServiceHostRequest request)
        {
            Request = request;
            Response = new ServiceHostResponse();
        }
        
        IService IService.Create() => new ServiceHost();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServiceHostRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServiceHostResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/ServiceHost";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "a1b60006f8ee69637c856c94dd192f5a";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceHostRequest : IRequest<ServiceHost, ServiceHostResponse>, IDeserializable<ServiceHostRequest>
    {
        [DataMember (Name = "service")] public string Service;
    
        /// Constructor for empty message.
        public ServiceHostRequest()
        {
            Service = string.Empty;
        }
        
        /// Explicit constructor.
        public ServiceHostRequest(string Service)
        {
            this.Service = Service;
        }
        
        /// Constructor with buffer.
        internal ServiceHostRequest(ref Buffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ServiceHostRequest(ref b);
        
        ServiceHostRequest IDeserializable<ServiceHostRequest>.RosDeserialize(ref Buffer b) => new ServiceHostRequest(ref b);
    
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
    public sealed class ServiceHostResponse : IResponse, IDeserializable<ServiceHostResponse>
    {
        [DataMember (Name = "host")] public string Host;
    
        /// Constructor for empty message.
        public ServiceHostResponse()
        {
            Host = string.Empty;
        }
        
        /// Explicit constructor.
        public ServiceHostResponse(string Host)
        {
            this.Host = Host;
        }
        
        /// Constructor with buffer.
        internal ServiceHostResponse(ref Buffer b)
        {
            Host = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ServiceHostResponse(ref b);
        
        ServiceHostResponse IDeserializable<ServiceHostResponse>.RosDeserialize(ref Buffer b) => new ServiceHostResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Host);
        }
        
        public void RosValidate()
        {
            if (Host is null) throw new System.NullReferenceException(nameof(Host));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Host);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
