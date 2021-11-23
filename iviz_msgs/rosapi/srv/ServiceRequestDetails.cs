using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class ServiceRequestDetails : IService
    {
        /// Request message.
        [DataMember] public ServiceRequestDetailsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ServiceRequestDetailsResponse Response { get; set; }
        
        /// Empty constructor.
        public ServiceRequestDetails()
        {
            Request = new ServiceRequestDetailsRequest();
            Response = new ServiceRequestDetailsResponse();
        }
        
        /// Setter constructor.
        public ServiceRequestDetails(ServiceRequestDetailsRequest request)
        {
            Request = request;
            Response = new ServiceRequestDetailsResponse();
        }
        
        IService IService.Create() => new ServiceRequestDetails();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServiceRequestDetailsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServiceRequestDetailsResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/ServiceRequestDetails";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "f9c88144f6f6bd888dd99d4e0411905d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceRequestDetailsRequest : IRequest<ServiceRequestDetails, ServiceRequestDetailsResponse>, IDeserializable<ServiceRequestDetailsRequest>
    {
        [DataMember (Name = "type")] public string Type;
    
        /// Constructor for empty message.
        public ServiceRequestDetailsRequest()
        {
            Type = string.Empty;
        }
        
        /// Explicit constructor.
        public ServiceRequestDetailsRequest(string Type)
        {
            this.Type = Type;
        }
        
        /// Constructor with buffer.
        internal ServiceRequestDetailsRequest(ref Buffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ServiceRequestDetailsRequest(ref b);
        
        ServiceRequestDetailsRequest IDeserializable<ServiceRequestDetailsRequest>.RosDeserialize(ref Buffer b) => new ServiceRequestDetailsRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
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
    public sealed class ServiceRequestDetailsResponse : IResponse, IDeserializable<ServiceRequestDetailsResponse>
    {
        [DataMember (Name = "typedefs")] public TypeDef[] Typedefs;
    
        /// Constructor for empty message.
        public ServiceRequestDetailsResponse()
        {
            Typedefs = System.Array.Empty<TypeDef>();
        }
        
        /// Explicit constructor.
        public ServiceRequestDetailsResponse(TypeDef[] Typedefs)
        {
            this.Typedefs = Typedefs;
        }
        
        /// Constructor with buffer.
        internal ServiceRequestDetailsResponse(ref Buffer b)
        {
            Typedefs = b.DeserializeArray<TypeDef>();
            for (int i = 0; i < Typedefs.Length; i++)
            {
                Typedefs[i] = new TypeDef(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ServiceRequestDetailsResponse(ref b);
        
        ServiceRequestDetailsResponse IDeserializable<ServiceRequestDetailsResponse>.RosDeserialize(ref Buffer b) => new ServiceRequestDetailsResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Typedefs, 0);
        }
        
        public void RosValidate()
        {
            if (Typedefs is null) throw new System.NullReferenceException(nameof(Typedefs));
            for (int i = 0; i < Typedefs.Length; i++)
            {
                if (Typedefs[i] is null) throw new System.NullReferenceException($"{nameof(Typedefs)}[{i}]");
                Typedefs[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Typedefs);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
