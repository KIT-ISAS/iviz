using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/ServiceResponseDetails")]
    public sealed class ServiceResponseDetails : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ServiceResponseDetailsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ServiceResponseDetailsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ServiceResponseDetails()
        {
            Request = new ServiceResponseDetailsRequest();
            Response = new ServiceResponseDetailsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServiceResponseDetails(ServiceResponseDetailsRequest request)
        {
            Request = request;
            Response = new ServiceResponseDetailsResponse();
        }
        
        IService IService.Create() => new ServiceResponseDetails();
        
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/ServiceResponseDetails";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "f9c88144f6f6bd888dd99d4e0411905d";
    }

    [DataContract]
    public sealed class ServiceResponseDetailsRequest : IRequest<ServiceResponseDetails, ServiceResponseDetailsResponse>, IDeserializable<ServiceResponseDetailsRequest>
    {
        [DataMember (Name = "type")] public string Type { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceResponseDetailsRequest()
        {
            Type = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceResponseDetailsRequest(string Type)
        {
            this.Type = Type;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ServiceResponseDetailsRequest(ref Buffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ServiceResponseDetailsRequest(ref b);
        }
        
        ServiceResponseDetailsRequest IDeserializable<ServiceResponseDetailsRequest>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
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

    [DataContract]
    public sealed class ServiceResponseDetailsResponse : IResponse, IDeserializable<ServiceResponseDetailsResponse>
    {
        [DataMember (Name = "typedefs")] public TypeDef[] Typedefs { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceResponseDetailsResponse()
        {
            Typedefs = System.Array.Empty<TypeDef>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceResponseDetailsResponse(TypeDef[] Typedefs)
        {
            this.Typedefs = Typedefs;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ServiceResponseDetailsResponse(ref Buffer b)
        {
            Typedefs = b.DeserializeArray<TypeDef>();
            for (int i = 0; i < Typedefs.Length; i++)
            {
                Typedefs[i] = new TypeDef(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ServiceResponseDetailsResponse(ref b);
        }
        
        ServiceResponseDetailsResponse IDeserializable<ServiceResponseDetailsResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Typedefs)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    }
}
