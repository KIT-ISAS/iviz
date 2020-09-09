using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/TopicType")]
    public sealed class TopicType : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TopicTypeRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TopicTypeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TopicType()
        {
            Request = new TopicTypeRequest();
            Response = new TopicTypeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TopicType(TopicTypeRequest request)
        {
            Request = request;
            Response = new TopicTypeResponse();
        }
        
        IService IService.Create() => new TopicType();
        
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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/TopicType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "0d30b3f53a0fd5036523a7141e524ddf";
    }

    public sealed class TopicTypeRequest : IRequest
    {
        [DataMember (Name = "topic")] public string Topic { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicTypeRequest()
        {
            Topic = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicTypeRequest(string Topic)
        {
            this.Topic = Topic;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicTypeRequest(Buffer b)
        {
            Topic = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new TopicTypeRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
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

    public sealed class TopicTypeResponse : IResponse
    {
        [DataMember (Name = "type")] public string Type { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicTypeResponse()
        {
            Type = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicTypeResponse(string Type)
        {
            this.Type = Type;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicTypeResponse(Buffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new TopicTypeResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Type);
        }
        
        public void RosValidate()
        {
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Type);
                return size;
            }
        }
    }
}
