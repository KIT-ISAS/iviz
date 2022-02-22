using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class TopicType : IService
    {
        /// Request message.
        [DataMember] public TopicTypeRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TopicTypeResponse Response { get; set; }
        
        /// Empty constructor.
        public TopicType()
        {
            Request = new TopicTypeRequest();
            Response = new TopicTypeResponse();
        }
        
        /// Setter constructor.
        public TopicType(TopicTypeRequest request)
        {
            Request = request;
            Response = new TopicTypeResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TopicTypeRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TopicTypeResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/TopicType";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "0d30b3f53a0fd5036523a7141e524ddf";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TopicTypeRequest : IRequest<TopicType, TopicTypeResponse>, IDeserializable<TopicTypeRequest>
    {
        [DataMember (Name = "topic")] public string Topic;
    
        /// Constructor for empty message.
        public TopicTypeRequest()
        {
            Topic = "";
        }
        
        /// Explicit constructor.
        public TopicTypeRequest(string Topic)
        {
            this.Topic = Topic;
        }
        
        /// Constructor with buffer.
        public TopicTypeRequest(ref ReadBuffer b)
        {
            Topic = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TopicTypeRequest(ref b);
        
        public TopicTypeRequest RosDeserialize(ref ReadBuffer b) => new TopicTypeRequest(ref b);
    
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
    public sealed class TopicTypeResponse : IResponse, IDeserializable<TopicTypeResponse>
    {
        [DataMember (Name = "type")] public string Type;
    
        /// Constructor for empty message.
        public TopicTypeResponse()
        {
            Type = "";
        }
        
        /// Explicit constructor.
        public TopicTypeResponse(string Type)
        {
            this.Type = Type;
        }
        
        /// Constructor with buffer.
        public TopicTypeResponse(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TopicTypeResponse(ref b);
        
        public TopicTypeResponse RosDeserialize(ref ReadBuffer b) => new TopicTypeResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
}
