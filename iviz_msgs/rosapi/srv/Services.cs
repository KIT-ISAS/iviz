using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
    public sealed class Services : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ServicesRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ServicesResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Services()
        {
            Request = new ServicesRequest();
            Response = new ServicesResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Services(ServicesRequest request)
        {
            Request = request;
            Response = new ServicesResponse();
        }
        
        IService IService.Create() => new Services();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServicesRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServicesResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/Services";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "e44a7e7bcb900acadbcc28b132378f0c";
    }

    public sealed class ServicesRequest : IRequest
    {
        
    
        /// <summary> Constructor for empty message. </summary>
        public ServicesRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServicesRequest(Buffer b)
        {
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new ServicesRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 0;
    }

    public sealed class ServicesResponse : IResponse
    {
        [DataMember] public string[] services { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServicesResponse()
        {
            services = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServicesResponse(string[] services)
        {
            this.services = services ?? throw new System.ArgumentNullException(nameof(services));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServicesResponse(Buffer b)
        {
            this.services = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new ServicesResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.services, 0);
        }
        
        public void Validate()
        {
            if (services is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * services.Length;
                for (int i = 0; i < services.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(services[i]);
                }
                return size;
            }
        }
    }
}
