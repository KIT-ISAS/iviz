using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class ServiceProviders : IService
    {
        /// Request message.
        [DataMember] public ServiceProvidersRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ServiceProvidersResponse Response { get; set; }
        
        /// Empty constructor.
        public ServiceProviders()
        {
            Request = new ServiceProvidersRequest();
            Response = new ServiceProvidersResponse();
        }
        
        /// Setter constructor.
        public ServiceProviders(ServiceProvidersRequest request)
        {
            Request = request;
            Response = new ServiceProvidersResponse();
        }
        
        IService IService.Create() => new ServiceProviders();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServiceProvidersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServiceProvidersResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/ServiceProviders";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "f30b41d5e347454ae5483ee95eef5cc6";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceProvidersRequest : IRequest<ServiceProviders, ServiceProvidersResponse>, IDeserializable<ServiceProvidersRequest>
    {
        [DataMember (Name = "service")] public string Service;
    
        /// Constructor for empty message.
        public ServiceProvidersRequest()
        {
            Service = string.Empty;
        }
        
        /// Explicit constructor.
        public ServiceProvidersRequest(string Service)
        {
            this.Service = Service;
        }
        
        /// Constructor with buffer.
        internal ServiceProvidersRequest(ref Buffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ServiceProvidersRequest(ref b);
        
        ServiceProvidersRequest IDeserializable<ServiceProvidersRequest>.RosDeserialize(ref Buffer b) => new ServiceProvidersRequest(ref b);
    
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
    public sealed class ServiceProvidersResponse : IResponse, IDeserializable<ServiceProvidersResponse>
    {
        [DataMember (Name = "providers")] public string[] Providers;
    
        /// Constructor for empty message.
        public ServiceProvidersResponse()
        {
            Providers = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public ServiceProvidersResponse(string[] Providers)
        {
            this.Providers = Providers;
        }
        
        /// Constructor with buffer.
        internal ServiceProvidersResponse(ref Buffer b)
        {
            Providers = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ServiceProvidersResponse(ref b);
        
        ServiceProvidersResponse IDeserializable<ServiceProvidersResponse>.RosDeserialize(ref Buffer b) => new ServiceProvidersResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Providers, 0);
        }
        
        public void RosValidate()
        {
            if (Providers is null) throw new System.NullReferenceException(nameof(Providers));
            for (int i = 0; i < Providers.Length; i++)
            {
                if (Providers[i] is null) throw new System.NullReferenceException($"{nameof(Providers)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Providers);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
