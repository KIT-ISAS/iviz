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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/ServiceProviders";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "f30b41d5e347454ae5483ee95eef5cc6";
    }

    public sealed class ServiceProvidersRequest : IRequest
    {
        [DataMember (Name = "service")] public string Service { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceProvidersRequest()
        {
            Service = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceProvidersRequest(string Service)
        {
            this.Service = Service;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServiceProvidersRequest(Buffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new ServiceProvidersRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Service);
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

    public sealed class ServiceProvidersResponse : IResponse
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
        internal ServiceProvidersResponse(Buffer b)
        {
            Providers = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new ServiceProvidersResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
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
