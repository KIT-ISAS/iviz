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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
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
        internal ServicesRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ServicesRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 0;
    }

    public sealed class ServicesResponse : IResponse
    {
        [DataMember (Name = "services")] public string[] Services_ { get; set; }
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * Services_.Length;
                foreach (string s in Services_)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    }
}
