using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/Subscribers")]
    public sealed class Subscribers : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SubscribersRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SubscribersResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Subscribers()
        {
            Request = new SubscribersRequest();
            Response = new SubscribersResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Subscribers(SubscribersRequest request)
        {
            Request = request;
            Response = new SubscribersResponse();
        }
        
        IService IService.Create() => new Subscribers();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SubscribersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SubscribersResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/Subscribers";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "cb387b68f5b29bc1456398ee8476b973";
    }

    public sealed class SubscribersRequest : IRequest, IDeserializable<SubscribersRequest>
    {
        [DataMember (Name = "topic")] public string Topic { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SubscribersRequest()
        {
            Topic = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SubscribersRequest(string Topic)
        {
            this.Topic = Topic;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SubscribersRequest(ref Buffer b)
        {
            Topic = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SubscribersRequest(ref b);
        }
        
        SubscribersRequest IDeserializable<SubscribersRequest>.RosDeserialize(ref Buffer b)
        {
            return new SubscribersRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Topic);
        }
        
        public void RosValidate()
        {
            if (Topic is null) throw new System.NullReferenceException(nameof(Topic));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Topic);
                return size;
            }
        }
    }

    public sealed class SubscribersResponse : IResponse, IDeserializable<SubscribersResponse>
    {
        [DataMember (Name = "subscribers")] public string[] Subscribers_ { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SubscribersResponse()
        {
            Subscribers_ = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SubscribersResponse(string[] Subscribers_)
        {
            this.Subscribers_ = Subscribers_;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SubscribersResponse(ref Buffer b)
        {
            Subscribers_ = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SubscribersResponse(ref b);
        }
        
        SubscribersResponse IDeserializable<SubscribersResponse>.RosDeserialize(ref Buffer b)
        {
            return new SubscribersResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Subscribers_, 0);
        }
        
        public void RosValidate()
        {
            if (Subscribers_ is null) throw new System.NullReferenceException(nameof(Subscribers_));
            for (int i = 0; i < Subscribers_.Length; i++)
            {
                if (Subscribers_[i] is null) throw new System.NullReferenceException($"{nameof(Subscribers_)}[{i}]");
            }
        }
    
        public int RosMessageLength => -2;
    }
}
