using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class TopicType : IService
    {
        /// <summary> Request message. </summary>
        public TopicTypeRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public TopicTypeResponse Response { get; set; }
        
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
        
        public IService Create() => new TopicType();
        
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
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/TopicType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "0d30b3f53a0fd5036523a7141e524ddf";
    }

    public sealed class TopicTypeRequest : IRequest
    {
        public string topic { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicTypeRequest()
        {
            topic = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicTypeRequest(string topic)
        {
            this.topic = topic ?? throw new System.ArgumentNullException(nameof(topic));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicTypeRequest(Buffer b)
        {
            this.topic = b.DeserializeString();
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TopicTypeRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.topic);
        }
        
        public void Validate()
        {
            if (topic is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(topic);
                return size;
            }
        }
    }

    public sealed class TopicTypeResponse : IResponse
    {
        public string type { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicTypeResponse()
        {
            type = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicTypeResponse(string type)
        {
            this.type = type ?? throw new System.ArgumentNullException(nameof(type));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicTypeResponse(Buffer b)
        {
            this.type = b.DeserializeString();
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TopicTypeResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.type);
        }
        
        public void Validate()
        {
            if (type is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(type);
                return size;
            }
        }
    }
}
