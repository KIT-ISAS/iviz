using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class Services : IService
    {
        /// Request message.
        [DataMember] public ServicesRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ServicesResponse Response { get; set; }
        
        /// Empty constructor.
        public Services()
        {
            Request = ServicesRequest.Singleton;
            Response = new ServicesResponse();
        }
        
        /// Setter constructor.
        public Services(ServicesRequest request)
        {
            Request = request;
            Response = new ServicesResponse();
        }
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/Services";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "e44a7e7bcb900acadbcc28b132378f0c";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServicesRequest : IRequest<Services, ServicesResponse>, IDeserializable<ServicesRequest>
    {
    
        /// Constructor for empty message.
        public ServicesRequest()
        {
        }
        
        /// Constructor with buffer.
        public ServicesRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public ServicesRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static ServicesRequest? singleton;
        public static ServicesRequest Singleton => singleton ??= new ServicesRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServicesResponse : IResponse, IDeserializable<ServicesResponse>
    {
        [DataMember (Name = "services")] public string[] Services_;
    
        /// Constructor for empty message.
        public ServicesResponse()
        {
            Services_ = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public ServicesResponse(string[] Services_)
        {
            this.Services_ = Services_;
        }
        
        /// Constructor with buffer.
        public ServicesResponse(ref ReadBuffer b)
        {
            Services_ = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ServicesResponse(ref b);
        
        public ServicesResponse RosDeserialize(ref ReadBuffer b) => new ServicesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Services_);
        }
        
        public void RosValidate()
        {
            if (Services_ is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Services_.Length; i++)
            {
                if (Services_[i] is null) BuiltIns.ThrowNullReference($"{nameof(Services_)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Services_);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
