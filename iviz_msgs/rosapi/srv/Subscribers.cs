using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class Subscribers : IService
    {
        /// Request message.
        [DataMember] public SubscribersRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SubscribersResponse Response { get; set; }
        
        /// Empty constructor.
        public Subscribers()
        {
            Request = new SubscribersRequest();
            Response = new SubscribersResponse();
        }
        
        /// Setter constructor.
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
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/Subscribers";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "cb387b68f5b29bc1456398ee8476b973";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SubscribersRequest : IRequest<Subscribers, SubscribersResponse>, IDeserializable<SubscribersRequest>
    {
        [DataMember (Name = "topic")] public string Topic;
    
        /// Constructor for empty message.
        public SubscribersRequest()
        {
            Topic = string.Empty;
        }
        
        /// Explicit constructor.
        public SubscribersRequest(string Topic)
        {
            this.Topic = Topic;
        }
        
        /// Constructor with buffer.
        internal SubscribersRequest(ref Buffer b)
        {
            Topic = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new SubscribersRequest(ref b);
        
        SubscribersRequest IDeserializable<SubscribersRequest>.RosDeserialize(ref Buffer b) => new SubscribersRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Topic);
        }
        
        public void RosValidate()
        {
            if (Topic is null) throw new System.NullReferenceException(nameof(Topic));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Topic);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SubscribersResponse : IResponse, IDeserializable<SubscribersResponse>
    {
        [DataMember (Name = "subscribers")] public string[] Subscribers_;
    
        /// Constructor for empty message.
        public SubscribersResponse()
        {
            Subscribers_ = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public SubscribersResponse(string[] Subscribers_)
        {
            this.Subscribers_ = Subscribers_;
        }
        
        /// Constructor with buffer.
        internal SubscribersResponse(ref Buffer b)
        {
            Subscribers_ = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new SubscribersResponse(ref b);
        
        SubscribersResponse IDeserializable<SubscribersResponse>.RosDeserialize(ref Buffer b) => new SubscribersResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Subscribers_);
        }
        
        public void RosValidate()
        {
            if (Subscribers_ is null) throw new System.NullReferenceException(nameof(Subscribers_));
            for (int i = 0; i < Subscribers_.Length; i++)
            {
                if (Subscribers_[i] is null) throw new System.NullReferenceException($"{nameof(Subscribers_)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Subscribers_);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
