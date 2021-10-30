using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/ServiceType")]
    public sealed class ServiceType : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ServiceTypeRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ServiceTypeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ServiceType()
        {
            Request = new ServiceTypeRequest();
            Response = new ServiceTypeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServiceType(ServiceTypeRequest request)
        {
            Request = request;
            Response = new ServiceTypeResponse();
        }
        
        IService IService.Create() => new ServiceType();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServiceTypeRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServiceTypeResponse)value;
        }
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/ServiceType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "0e24a2dcdf70e483afc092a35a1f15f7";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceTypeRequest : IRequest<ServiceType, ServiceTypeResponse>, IDeserializable<ServiceTypeRequest>
    {
        [DataMember (Name = "service")] public string Service;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceTypeRequest()
        {
            Service = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceTypeRequest(string Service)
        {
            this.Service = Service;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServiceTypeRequest(ref Buffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ServiceTypeRequest(ref b);
        }
        
        ServiceTypeRequest IDeserializable<ServiceTypeRequest>.RosDeserialize(ref Buffer b)
        {
            return new ServiceTypeRequest(ref b);
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
    public sealed class ServiceTypeResponse : IResponse, IDeserializable<ServiceTypeResponse>
    {
        [DataMember (Name = "type")] public string Type;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceTypeResponse()
        {
            Type = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServiceTypeResponse(string Type)
        {
            this.Type = Type;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServiceTypeResponse(ref Buffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ServiceTypeResponse(ref b);
        }
        
        ServiceTypeResponse IDeserializable<ServiceTypeResponse>.RosDeserialize(ref Buffer b)
        {
            return new ServiceTypeResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Type);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Type);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
