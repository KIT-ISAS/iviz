using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class ServiceProviders : IService<ServiceProvidersRequest, ServiceProvidersResponse>
    {
        /// Request message.
        [DataMember] public ServiceProvidersRequest Request;
        
        /// Response message.
        [DataMember] public ServiceProvidersResponse Response;
        
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
        
        public const string ServiceType = "rosapi_msgs/ServiceProviders";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "f30b41d5e347454ae5483ee95eef5cc6";
        
        public IService Generate() => new ServiceProviders();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceProvidersRequest : IRequest<ServiceProviders, ServiceProvidersResponse>, IDeserializable<ServiceProvidersRequest>
    {
        [DataMember (Name = "service")] public string Service;
    
        public ServiceProvidersRequest()
        {
            Service = "";
        }
        
        public ServiceProvidersRequest(string Service)
        {
            this.Service = Service;
        }
        
        public ServiceProvidersRequest(ref ReadBuffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ServiceProvidersRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Service = b.DeserializeString();
        }
        
        public ServiceProvidersRequest RosDeserialize(ref ReadBuffer b) => new ServiceProvidersRequest(ref b);
        
        public ServiceProvidersRequest RosDeserialize(ref ReadBuffer2 b) => new ServiceProvidersRequest(ref b);
    
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
    public sealed class ServiceProvidersResponse : IResponse, IDeserializable<ServiceProvidersResponse>
    {
        [DataMember (Name = "providers")] public string[] Providers;
    
        public ServiceProvidersResponse()
        {
            Providers = EmptyArray<string>.Value;
        }
        
        public ServiceProvidersResponse(string[] Providers)
        {
            this.Providers = Providers;
        }
        
        public ServiceProvidersResponse(ref ReadBuffer b)
        {
            Providers = b.DeserializeStringArray();
        }
        
        public ServiceProvidersResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Providers = b.DeserializeStringArray();
        }
        
        public ServiceProvidersResponse RosDeserialize(ref ReadBuffer b) => new ServiceProvidersResponse(ref b);
        
        public ServiceProvidersResponse RosDeserialize(ref ReadBuffer2 b) => new ServiceProvidersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Providers.Length);
            b.SerializeArray(Providers);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Providers.Length);
            b.SerializeArray(Providers);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Providers, nameof(Providers));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Providers);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Providers);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
