using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/ServiceHost";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "a1b60006f8ee69637c856c94dd192f5a";
    }

    public sealed class ServiceHostRequest : IRequest
    {
        [DataMember] public string service { get; set; }
    
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
            this.service = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new ServiceHostRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.service);
        }
        
        public void Validate()
        {
            if (service is null) throw new System.NullReferenceException();
        }
    
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
        [DataMember] public string host { get; set; }
    
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
            this.host = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new ServiceHostResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.host);
        }
        
        public void Validate()
        {
            if (host is null) throw new System.NullReferenceException();
        }
    
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
