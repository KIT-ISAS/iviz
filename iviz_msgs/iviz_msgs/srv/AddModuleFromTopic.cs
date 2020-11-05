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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/AddModuleFromTopic";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "f286bf1bf4d1e91c992ad2eae3061a6f";
    }

    public sealed class AddModuleFromTopicRequest : IRequest, IDeserializable<AddModuleFromTopicRequest>
    {
        // Adds a module
        [DataMember (Name = "topic")] public string Topic { get; set; } // Name of the topic
        [DataMember (Name = "configuration")] public string Configuration { get; set; } // Configuration in JSON form
    
        /// <summary> Constructor for empty message. </summary>
        public AddModuleFromTopicRequest()
        {
            Topic = "";
            Configuration = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddModuleFromTopicRequest(string Topic, string Configuration)
        {
            this.Topic = Topic;
            this.Configuration = Configuration;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddModuleFromTopicRequest(ref Buffer b)
        {
            Topic = b.DeserializeString();
            Configuration = b.DeserializeString();
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
            b.Serialize(Configuration);
        }
        
        public void RosValidate()
        {
            if (Topic is null) throw new System.NullReferenceException(nameof(Topic));
            if (Configuration is null) throw new System.NullReferenceException(nameof(Configuration));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Topic);
                size += BuiltIns.UTF8.GetByteCount(Configuration);
                return size;
            }
        }
    }

    public sealed class AddModuleFromTopicResponse : IResponse, IDeserializable<AddModuleFromTopicResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; } // Whether the retrieval succeeded
        [DataMember (Name = "message")] public string Message { get; set; } // An error message if success is false
    
        /// <summary> Constructor for empty message. </summary>
        public AddModuleFromTopicResponse()
        {
            Message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddModuleFromTopicResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddModuleFromTopicResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
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
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(Message);
                return size;
            }
        }
    }
}
