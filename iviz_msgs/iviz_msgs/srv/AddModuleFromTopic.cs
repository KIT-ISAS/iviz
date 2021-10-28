using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/AddModuleFromTopic")]
    public sealed class AddModuleFromTopic : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public AddModuleFromTopicRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public AddModuleFromTopicResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public AddModuleFromTopic()
        {
            Request = new AddModuleFromTopicRequest();
            Response = new AddModuleFromTopicResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/AddModuleFromTopic";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "68ab9eda5fc795e020e1e72fec9f4815";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddModuleFromTopicRequest : IRequest<AddModuleFromTopic, AddModuleFromTopicResponse>, IDeserializable<AddModuleFromTopicRequest>
    {
        // Adds a module
        [DataMember (Name = "topic")] public string Topic; // Name of the topic
        [DataMember (Name = "id")] public string Id; // Requested id to identify this module, or empty to autogenerate
    
        /// <summary> Constructor for empty message. </summary>
        public AddModuleFromTopicRequest()
        {
            Topic = string.Empty;
            Id = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddModuleFromTopicRequest(string Topic, string Id)
        {
            this.Topic = Topic;
            this.Id = Id;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddModuleFromTopicRequest(ref Buffer b)
        {
            Topic = b.DeserializeString();
            Id = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AddModuleFromTopicRequest(ref b);
        }
        
        AddModuleFromTopicRequest IDeserializable<AddModuleFromTopicRequest>.RosDeserialize(ref Buffer b)
        {
            return new AddModuleFromTopicRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Topic);
            b.Serialize(Id);
        }
        
        public void Dispose()
        {
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
    
        /// <summary> Constructor for empty message. </summary>
        public AddModuleFromTopicResponse()
        {
            Message = string.Empty;
            Id = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddModuleFromTopicResponse(bool Success, string Message, string Id)
        {
            this.Success = Success;
            this.Message = Message;
            this.Id = Id;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddModuleFromTopicResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
            Id = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AddModuleFromTopicResponse(ref b);
        }
        
        AddModuleFromTopicResponse IDeserializable<AddModuleFromTopicResponse>.RosDeserialize(ref Buffer b)
        {
            return new AddModuleFromTopicResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.Serialize(Id);
        }
        
        public void Dispose()
        {
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
