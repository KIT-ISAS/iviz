using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/ServicesForType")]
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
        [DataMember (Name = "type")] public string Type { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServicesForTypeRequest()
        {
            Type = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServicesForTypeRequest(string Type)
        {
            this.Type = Type;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServicesForTypeRequest(Buffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new ServicesForTypeRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Type);
        }
        
        public void RosValidate()
        {
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Type);
                return size;
            }
        }
    }

    public sealed class ServicesForTypeResponse : IResponse
    {
        [DataMember (Name = "services")] public string[] Services { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServicesForTypeResponse()
        {
            Services = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServicesForTypeResponse(string[] Services)
        {
            this.Services = Services;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServicesForTypeResponse(Buffer b)
        {
            Services = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new ServicesForTypeResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(Services, 0);
        }
        
        public void RosValidate()
        {
            if (Services is null) throw new System.NullReferenceException(nameof(Services));
            for (int i = 0; i < Services.Length; i++)
            {
                if (Services[i] is null) throw new System.NullReferenceException($"{nameof(Services)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * Services.Length;
                for (int i = 0; i < Services.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(Services[i]);
                }
                return size;
            }
        }
    }
}
