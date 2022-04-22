using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class ServiceResponseDetails : IService
    {
        /// Request message.
        [DataMember] public ServiceResponseDetailsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ServiceResponseDetailsResponse Response { get; set; }
        
        /// Empty constructor.
        public ServiceResponseDetails()
        {
            Request = new ServiceResponseDetailsRequest();
            Response = new ServiceResponseDetailsResponse();
        }
        
        /// Setter constructor.
        public ServiceResponseDetails(ServiceResponseDetailsRequest request)
        {
            Request = request;
            Response = new ServiceResponseDetailsResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServiceResponseDetailsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServiceResponseDetailsResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/ServiceResponseDetails";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "f9c88144f6f6bd888dd99d4e0411905d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceResponseDetailsRequest : IRequest<ServiceResponseDetails, ServiceResponseDetailsResponse>, IDeserializable<ServiceResponseDetailsRequest>
    {
        [DataMember (Name = "type")] public string Type;
    
        /// Constructor for empty message.
        public ServiceResponseDetailsRequest()
        {
            Type = "";
        }
        
        /// Explicit constructor.
        public ServiceResponseDetailsRequest(string Type)
        {
            this.Type = Type;
        }
        
        /// Constructor with buffer.
        public ServiceResponseDetailsRequest(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ServiceResponseDetailsRequest(ref b);
        
        public ServiceResponseDetailsRequest RosDeserialize(ref ReadBuffer b) => new ServiceResponseDetailsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Type);
        }
        
        public void RosValidate()
        {
            if (Type is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Type);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceResponseDetailsResponse : IResponse, IDeserializable<ServiceResponseDetailsResponse>
    {
        [DataMember (Name = "typedefs")] public TypeDef[] Typedefs;
    
        /// Constructor for empty message.
        public ServiceResponseDetailsResponse()
        {
            Typedefs = System.Array.Empty<TypeDef>();
        }
        
        /// Explicit constructor.
        public ServiceResponseDetailsResponse(TypeDef[] Typedefs)
        {
            this.Typedefs = Typedefs;
        }
        
        /// Constructor with buffer.
        public ServiceResponseDetailsResponse(ref ReadBuffer b)
        {
            Typedefs = b.DeserializeArray<TypeDef>();
            for (int i = 0; i < Typedefs.Length; i++)
            {
                Typedefs[i] = new TypeDef(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ServiceResponseDetailsResponse(ref b);
        
        public ServiceResponseDetailsResponse RosDeserialize(ref ReadBuffer b) => new ServiceResponseDetailsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Typedefs);
        }
        
        public void RosValidate()
        {
            if (Typedefs is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Typedefs.Length; i++)
            {
                if (Typedefs[i] is null) BuiltIns.ThrowNullReference($"{nameof(Typedefs)}[{i}]");
                Typedefs[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Typedefs);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
