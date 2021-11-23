using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class AddModuleFromTopic : IService
    {
        /// Request message.
        [DataMember] public AddModuleFromTopicRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public AddModuleFromTopicResponse Response { get; set; }
        
        /// Empty constructor.
        public AddModuleFromTopic()
        {
            Request = new AddModuleFromTopicRequest();
            Response = new AddModuleFromTopicResponse();
        }
        
        /// Setter constructor.
        public AddModuleFromTopic(AddModuleFromTopicRequest request)
        {
            Request = request;
            Response = new AddModuleFromTopicResponse();
        }
        
        IService IService.Create() => new AddModuleFromTopic();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (AddModuleFromTopicRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (AddModuleFromTopicResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/AddModuleFromTopic";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "68ab9eda5fc795e020e1e72fec9f4815";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddModuleFromTopicRequest : IRequest<AddModuleFromTopic, AddModuleFromTopicResponse>, IDeserializable<AddModuleFromTopicRequest>
    {
        // Adds a module
        [DataMember (Name = "topic")] public string Topic; // Name of the topic
        [DataMember (Name = "id")] public string Id; // Requested id to identify this module, or empty to autogenerate
    
        /// Constructor for empty message.
        public AddModuleFromTopicRequest()
        {
            Topic = string.Empty;
            Id = string.Empty;
        }
        
        /// Explicit constructor.
        public AddModuleFromTopicRequest(string Topic, string Id)
        {
            this.Topic = Topic;
            this.Id = Id;
        }
        
        /// Constructor with buffer.
        internal AddModuleFromTopicRequest(ref Buffer b)
        {
            Topic = b.DeserializeString();
            Id = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AddModuleFromTopicRequest(ref b);
        
        AddModuleFromTopicRequest IDeserializable<AddModuleFromTopicRequest>.RosDeserialize(ref Buffer b) => new AddModuleFromTopicRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Topic);
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Topic is null) throw new System.NullReferenceException(nameof(Topic));
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Topic) + BuiltIns.GetStringSize(Id);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddModuleFromTopicResponse : IResponse, IDeserializable<AddModuleFromTopicResponse>
    {
        [DataMember (Name = "success")] public bool Success; // Whether the retrieval succeeded
        [DataMember (Name = "message")] public string Message; // An error message if success is false
        [DataMember (Name = "id")] public string Id; // An id identifying this module
    
        /// Constructor for empty message.
        public AddModuleFromTopicResponse()
        {
            Message = string.Empty;
            Id = string.Empty;
        }
        
        /// Explicit constructor.
        public AddModuleFromTopicResponse(bool Success, string Message, string Id)
        {
            this.Success = Success;
            this.Message = Message;
            this.Id = Id;
        }
        
        /// Constructor with buffer.
        internal AddModuleFromTopicResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
            Id = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AddModuleFromTopicResponse(ref b);
        
        AddModuleFromTopicResponse IDeserializable<AddModuleFromTopicResponse>.RosDeserialize(ref Buffer b) => new AddModuleFromTopicResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
        }
    
        public int RosMessageLength => 9 + BuiltIns.GetStringSize(Message) + BuiltIns.GetStringSize(Id);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
