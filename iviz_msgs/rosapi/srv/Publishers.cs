using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class Publishers : IService
    {
        /// Request message.
        [DataMember] public PublishersRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public PublishersResponse Response { get; set; }
        
        /// Empty constructor.
        public Publishers()
        {
            Request = new PublishersRequest();
            Response = new PublishersResponse();
        }
        
        /// Setter constructor.
        public Publishers(PublishersRequest request)
        {
            Request = request;
            Response = new PublishersResponse();
        }
        
        IService IService.Create() => new Publishers();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (PublishersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (PublishersResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/Publishers";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "cb37f09944e7ba1fc08ee38f7a94291d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class PublishersRequest : IRequest<Publishers, PublishersResponse>, IDeserializable<PublishersRequest>
    {
        [DataMember (Name = "topic")] public string Topic;
    
        /// Constructor for empty message.
        public PublishersRequest()
        {
            Topic = string.Empty;
        }
        
        /// Explicit constructor.
        public PublishersRequest(string Topic)
        {
            this.Topic = Topic;
        }
        
        /// Constructor with buffer.
        public PublishersRequest(ref ReadBuffer b)
        {
            Topic = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PublishersRequest(ref b);
        
        public PublishersRequest RosDeserialize(ref ReadBuffer b) => new PublishersRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    public sealed class PublishersResponse : IResponse, IDeserializable<PublishersResponse>
    {
        [DataMember (Name = "publishers")] public string[] Publishers_;
    
        /// Constructor for empty message.
        public PublishersResponse()
        {
            Publishers_ = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public PublishersResponse(string[] Publishers_)
        {
            this.Publishers_ = Publishers_;
        }
        
        /// Constructor with buffer.
        public PublishersResponse(ref ReadBuffer b)
        {
            Publishers_ = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PublishersResponse(ref b);
        
        public PublishersResponse RosDeserialize(ref ReadBuffer b) => new PublishersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Publishers_);
        }
        
        public void RosValidate()
        {
            if (Publishers_ is null) throw new System.NullReferenceException(nameof(Publishers_));
            for (int i = 0; i < Publishers_.Length; i++)
            {
                if (Publishers_[i] is null) throw new System.NullReferenceException($"{nameof(Publishers_)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Publishers_);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
