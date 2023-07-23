using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class ServiceHost : IService<ServiceHostRequest, ServiceHostResponse>
    {
        /// Request message.
        [DataMember] public ServiceHostRequest Request;
        
        /// Response message.
        [DataMember] public ServiceHostResponse Response;
        
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
        
        public const string ServiceType = "rosapi_msgs/ServiceHost";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "a1b60006f8ee69637c856c94dd192f5a";
        
        public IService Generate() => new ServiceHost();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceHostRequest : IRequest<ServiceHost, ServiceHostResponse>, IDeserializable<ServiceHostRequest>
    {
        [DataMember (Name = "service")] public string Service;
    
        public ServiceHostRequest()
        {
            Service = "";
        }
        
        public ServiceHostRequest(string Service)
        {
            this.Service = Service;
        }
        
        public ServiceHostRequest(ref ReadBuffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ServiceHostRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Service = b.DeserializeString();
        }
        
        public ServiceHostRequest RosDeserialize(ref ReadBuffer b) => new ServiceHostRequest(ref b);
        
        public ServiceHostRequest RosDeserialize(ref ReadBuffer2 b) => new ServiceHostRequest(ref b);
    
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
    public sealed class ServiceHostResponse : IResponse, IDeserializable<ServiceHostResponse>
    {
        [DataMember (Name = "host")] public string Host;
    
        public ServiceHostResponse()
        {
            Host = "";
        }
        
        public ServiceHostResponse(string Host)
        {
            this.Host = Host;
        }
        
        public ServiceHostResponse(ref ReadBuffer b)
        {
            Host = b.DeserializeString();
        }
        
        public ServiceHostResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Host = b.DeserializeString();
        }
        
        public ServiceHostResponse RosDeserialize(ref ReadBuffer b) => new ServiceHostResponse(ref b);
        
        public ServiceHostResponse RosDeserialize(ref ReadBuffer2 b) => new ServiceHostResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Host);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Host);
        }
        
        public void RosValidate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Host);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Host);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
