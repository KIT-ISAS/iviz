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
            Type = "";
        }
        
        /// Explicit constructor.
        public TopicsForTypeRequest(string Type)
        {
            this.Type = Type;
        }
        
        /// Constructor with buffer.
        public TopicsForTypeRequest(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TopicsForTypeRequest(ref b);
        
        public TopicsForTypeRequest RosDeserialize(ref ReadBuffer b) => new TopicsForTypeRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Type);
        }
        
        public void RosValidate()
        {
            if (Type is null) BuiltIns.ThrowNullReference();
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
        public TopicsForTypeResponse(ref ReadBuffer b)
        {
            Topics = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TopicsForTypeResponse(ref b);
        
        public TopicsForTypeResponse RosDeserialize(ref ReadBuffer b) => new TopicsForTypeResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Topics);
        }
        
        public void RosValidate()
        {
            if (Topics is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Topics.Length; i++)
            {
                if (Topics[i] is null) BuiltIns.ThrowNullReference($"{nameof(Topics)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Topics);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
