using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/ServiceHost")]
    public sealed class ServiceHost : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ServiceHostRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ServiceHostResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ServiceHost()
        {
            Request = new ServiceHostRequest();
            Response = new ServiceHostResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/ServiceHost";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "a1b60006f8ee69637c856c94dd192f5a";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceHostRequest : IRequest<ServiceHost, ServiceHostResponse>, IDeserializable<ServiceHostRequest>
    {
        [DataMember (Name = "service")] public string Service;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceHostRequest()
        {
            Service = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceHostRequest(string Service)
        {
            this.Service = Service;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServiceHostRequest(ref Buffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ServiceHostRequest(ref b);
        }
        
        ServiceHostRequest IDeserializable<ServiceHostRequest>.RosDeserialize(ref Buffer b)
        {
            return new ServiceHostRequest(ref b);
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
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Service);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceHostResponse : IResponse, IDeserializable<ServiceHostResponse>
    {
        [DataMember (Name = "host")] public string Host;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceHostResponse()
        {
            Host = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceHostResponse(string Host)
        {
            this.Host = Host;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServiceHostResponse(ref Buffer b)
        {
            Host = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ServiceHostResponse(ref b);
        }
        
        ServiceHostResponse IDeserializable<ServiceHostResponse>.RosDeserialize(ref Buffer b)
        {
            return new ServiceHostResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Host);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Host is null) throw new System.NullReferenceException(nameof(Host));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Host);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
