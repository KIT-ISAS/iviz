using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/ServiceProviders")]
    public sealed class ServiceProviders : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ServiceProvidersRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ServiceProvidersResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ServiceProviders()
        {
            Request = new ServiceProvidersRequest();
            Response = new ServiceProvidersResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/ServiceProviders";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "f30b41d5e347454ae5483ee95eef5cc6";
    }

    [DataContract]
    public sealed class ServiceProvidersRequest : IRequest<ServiceProviders, ServiceProvidersResponse>, IDeserializable<ServiceProvidersRequest>
    {
        [DataMember (Name = "service")] public string Service { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceProvidersRequest()
        {
            Service = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceProvidersRequest(string Service)
        {
            this.Service = Service;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ServiceProvidersRequest(ref Buffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ServiceProvidersRequest(ref b);
        }
        
        ServiceProvidersRequest IDeserializable<ServiceProvidersRequest>.RosDeserialize(ref Buffer b)
        {
            return new ServiceProvidersRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Service);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Service is null) throw new System.NullReferenceException(nameof(Service));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Service);
                return size;
            }
        }
    }

    [DataContract]
    public sealed class ServiceProvidersResponse : IResponse, IDeserializable<ServiceProvidersResponse>
    {
        [DataMember (Name = "providers")] public string[] Providers { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceProvidersResponse()
        {
            Providers = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceProvidersResponse(string[] Providers)
        {
            this.Providers = Providers;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ServiceProvidersResponse(ref Buffer b)
        {
            Providers = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ServiceProvidersResponse(ref b);
        }
        
        ServiceProvidersResponse IDeserializable<ServiceProvidersResponse>.RosDeserialize(ref Buffer b)
        {
            return new ServiceProvidersResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Providers, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Providers is null) throw new System.NullReferenceException(nameof(Providers));
            for (int i = 0; i < Providers.Length; i++)
            {
                if (Providers[i] is null) throw new System.NullReferenceException($"{nameof(Providers)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * Providers.Length;
                foreach (string s in Providers)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    }
}
