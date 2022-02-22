using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class ServicesForType : IService
    {
        /// Request message.
        [DataMember] public ServicesForTypeRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ServicesForTypeResponse Response { get; set; }
        
        /// Empty constructor.
        public ServicesForType()
        {
            Request = new ServicesForTypeRequest();
            Response = new ServicesForTypeResponse();
        }
        
        /// Setter constructor.
        public ServicesForType(ServicesForTypeRequest request)
        {
            Request = request;
            Response = new ServicesForTypeResponse();
        }
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/ServicesForType";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "93e9fe8ae5a9136008e260fe510bd2b0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServicesForTypeRequest : IRequest<ServicesForType, ServicesForTypeResponse>, IDeserializable<ServicesForTypeRequest>
    {
        [DataMember (Name = "type")] public string Type;
    
        /// Constructor for empty message.
        public ServicesForTypeRequest()
        {
            Type = "";
        }
        
        /// Explicit constructor.
        public ServicesForTypeRequest(string Type)
        {
            this.Type = Type;
        }
        
        /// Constructor with buffer.
        public ServicesForTypeRequest(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ServicesForTypeRequest(ref b);
        
        public ServicesForTypeRequest RosDeserialize(ref ReadBuffer b) => new ServicesForTypeRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Type);
        }
        
        public void RosValidate()
        {
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Type);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServicesForTypeResponse : IResponse, IDeserializable<ServicesForTypeResponse>
    {
        [DataMember (Name = "services")] public string[] Services;
    
        /// Constructor for empty message.
        public ServicesForTypeResponse()
        {
            Services = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public ServicesForTypeResponse(string[] Services)
        {
            this.Services = Services;
        }
        
        /// Constructor with buffer.
        public ServicesForTypeResponse(ref ReadBuffer b)
        {
            Services = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ServicesForTypeResponse(ref b);
        
        public ServicesForTypeResponse RosDeserialize(ref ReadBuffer b) => new ServicesForTypeResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Services);
        }
        
        public void RosValidate()
        {
            if (Services is null) throw new System.NullReferenceException(nameof(Services));
            for (int i = 0; i < Services.Length; i++)
            {
                if (Services[i] is null) throw new System.NullReferenceException($"{nameof(Services)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Services);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
