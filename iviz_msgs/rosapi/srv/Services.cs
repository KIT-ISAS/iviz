using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/Services")]
    public sealed class Services : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ServicesRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ServicesResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Services()
        {
            Request = ServicesRequest.Singleton;
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/Services";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "e44a7e7bcb900acadbcc28b132378f0c";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServicesRequest : IRequest<Services, ServicesResponse>, IDeserializable<ServicesRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public ServicesRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServicesRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        ServicesRequest IDeserializable<ServicesRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly ServicesRequest Singleton = new ServicesRequest();
    
        public void RosSerialize(ref Buffer b)
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
    
        /// <summary> Constructor for empty message. </summary>
        public ServicesResponse()
        {
            Services_ = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ServicesResponse(string[] Services_)
        {
            this.Services_ = Services_;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ServicesResponse(ref Buffer b)
        {
            Services_ = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ServicesResponse(ref b);
        }
        
        ServicesResponse IDeserializable<ServicesResponse>.RosDeserialize(ref Buffer b)
        {
            return new ServicesResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Services_, 0);
        }
        
        public void RosValidate()
        {
            if (Services_ is null) throw new System.NullReferenceException(nameof(Services_));
            for (int i = 0; i < Services_.Length; i++)
            {
                if (Services_[i] is null) throw new System.NullReferenceException($"{nameof(Services_)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Services_);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
