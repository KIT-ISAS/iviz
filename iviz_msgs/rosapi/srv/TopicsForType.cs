using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class TopicsForType : IService
    {
        /// Request message.
        [DataMember] public TopicsForTypeRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TopicsForTypeResponse Response { get; set; }
        
        /// Empty constructor.
        public TopicsForType()
        {
            Request = new TopicsForTypeRequest();
            Response = new TopicsForTypeResponse();
        }
        
        /// Setter constructor.
        public TopicsForType(TopicsForTypeRequest request)
        {
            Request = request;
            Response = new TopicsForTypeResponse();
        }
        
        IService IService.Create() => new TopicsForType();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TopicsForTypeRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TopicsForTypeResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/TopicsForType";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "56f77ff6da756dd27c1ed16ec721072a";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TopicsForTypeRequest : IRequest<TopicsForType, TopicsForTypeResponse>, IDeserializable<TopicsForTypeRequest>
    {
        [DataMember (Name = "type")] public string Type;
    
        /// Constructor for empty message.
        public TopicsForTypeRequest()
        {
            Type = string.Empty;
        }
        
        /// Explicit constructor.
        public TopicsForTypeRequest(string Type)
        {
            this.Type = Type;
        }
        
        /// Constructor with buffer.
        internal TopicsForTypeRequest(ref Buffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TopicsForTypeRequest(ref b);
        
        TopicsForTypeRequest IDeserializable<TopicsForTypeRequest>.RosDeserialize(ref Buffer b) => new TopicsForTypeRequest(ref b);
    
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
    public sealed class TopicsForTypeResponse : IResponse, IDeserializable<TopicsForTypeResponse>
    {
        [DataMember (Name = "topics")] public string[] Topics;
    
        /// Constructor for empty message.
        public TopicsForTypeResponse()
        {
            Topics = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public TopicsForTypeResponse(string[] Topics)
        {
            this.Topics = Topics;
        }
        
        /// Constructor with buffer.
        internal TopicsForTypeResponse(ref Buffer b)
        {
            Topics = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TopicsForTypeResponse(ref b);
        
        TopicsForTypeResponse IDeserializable<TopicsForTypeResponse>.RosDeserialize(ref Buffer b) => new TopicsForTypeResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Topics);
        }
        
        public void RosValidate()
        {
            if (Topics is null) throw new System.NullReferenceException(nameof(Topics));
            for (int i = 0; i < Topics.Length; i++)
            {
                if (Topics[i] is null) throw new System.NullReferenceException($"{nameof(Topics)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Topics);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
