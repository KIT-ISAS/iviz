using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
    public sealed class ServicesForType : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ServicesForTypeRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ServicesForTypeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ServicesForType()
        {
            Request = new ServicesForTypeRequest();
            Response = new ServicesForTypeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServicesForType(ServicesForTypeRequest request)
        {
            Request = request;
            Response = new ServicesForTypeResponse();
        }
        
        IService IService.Create() => new ServicesForType();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServicesForTypeRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServicesForTypeResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/ServicesForType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "93e9fe8ae5a9136008e260fe510bd2b0";
    }

    public sealed class ServicesForTypeRequest : IRequest
    {
        [DataMember] public string type { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServicesForTypeRequest()
        {
            type = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServicesForTypeRequest(string type)
        {
            this.type = type ?? throw new System.ArgumentNullException(nameof(type));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServicesForTypeRequest(Buffer b)
        {
            this.type = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new ServicesForTypeRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.type);
        }
        
        public void Validate()
        {
            if (type is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(type);
                return size;
            }
        }
    }

    public sealed class ServicesForTypeResponse : IResponse
    {
        [DataMember] public string[] services { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServicesForTypeResponse()
        {
            services = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServicesForTypeResponse(string[] services)
        {
            this.services = services ?? throw new System.ArgumentNullException(nameof(services));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServicesForTypeResponse(Buffer b)
        {
            this.services = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new ServicesForTypeResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
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
