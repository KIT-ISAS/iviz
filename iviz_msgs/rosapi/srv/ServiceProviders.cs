using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class ServiceProviders : IService
    {
        /// <summary> Request message. </summary>
        public ServiceProvidersRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public ServiceProvidersResponse Response { get; set; }
        
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
        
        public IService Create() => new ServiceProviders();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/ServiceProviders";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "f30b41d5e347454ae5483ee95eef5cc6";
    }

    public sealed class ServiceProvidersRequest : IRequest
    {
        public string service;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceProvidersRequest()
        {
            service = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out service, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(service, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(service);
                return size;
            }
        }
    }

    public sealed class ServiceProvidersResponse : IResponse
    {
        public string[] providers;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceProvidersResponse()
        {
            providers = System.Array.Empty<string>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out providers, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(providers, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * providers.Length;
                for (int i = 0; i < providers.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(providers[i]);
                }
                return size;
            }
        }
    }
}
