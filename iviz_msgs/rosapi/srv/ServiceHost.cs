using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class ServiceHost : IService
    {
        /// <summary> Request message. </summary>
        public ServiceHostRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public ServiceHostResponse Response { get; set; }
        
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
        
        public IService Create() => new ServiceHost();
        
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
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/ServiceHost";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "a1b60006f8ee69637c856c94dd192f5a";
    }

    public sealed class ServiceHostRequest : IRequest
    {
        public string service { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceHostRequest()
        {
            service = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceHostRequest(string service)
        {
            this.service = service ?? throw new System.ArgumentNullException(nameof(service));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServiceHostRequest(Buffer b)
        {
            this.service = BuiltIns.DeserializeString(b);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new ServiceHostRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.service, b);
        }
        
        public void Validate()
        {
            if (service is null) throw new System.NullReferenceException();
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

    public sealed class ServiceHostResponse : IResponse
    {
        public string host { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceHostResponse()
        {
            host = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceHostResponse(string host)
        {
            this.host = host ?? throw new System.ArgumentNullException(nameof(host));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServiceHostResponse(Buffer b)
        {
            this.host = BuiltIns.DeserializeString(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new ServiceHostResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.host, b);
        }
        
        public void Validate()
        {
            if (host is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(host);
                return size;
            }
        }
    }
}
