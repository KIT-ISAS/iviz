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
            Service = "";
        }
        
        /// Explicit constructor.
        public ServiceProvidersRequest(string Service)
        {
            this.Service = Service;
        }
        
        /// Constructor with buffer.
        public ServiceProvidersRequest(ref ReadBuffer b)
        {
            Service = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ServiceProvidersRequest(ref b);
        
        public ServiceProvidersRequest RosDeserialize(ref ReadBuffer b) => new ServiceProvidersRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Service);
        }
        
        public void RosValidate()
        {
            if (Service is null) BuiltIns.ThrowNullReference(nameof(Service));
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
        public ServiceProvidersResponse(ref ReadBuffer b)
        {
            Providers = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ServiceProvidersResponse(ref b);
        
        public ServiceProvidersResponse RosDeserialize(ref ReadBuffer b) => new ServiceProvidersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Providers);
        }
        
        public void RosValidate()
        {
            if (Providers is null) BuiltIns.ThrowNullReference(nameof(Providers));
            for (int i = 0; i < Providers.Length; i++)
            {
                if (Providers[i] is null) BuiltIns.ThrowNullReference($"{nameof(Providers)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Providers);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
