using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class TopicsForType : IService
    {
        /// <summary> Request message. </summary>
        public TopicsForTypeRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public TopicsForTypeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TopicsForType()
        {
            Request = new TopicsForTypeRequest();
            Response = new TopicsForTypeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TopicsForType(TopicsForTypeRequest request)
        {
            Request = request;
            Response = new TopicsForTypeResponse();
        }
        
        public IService Create() => new TopicsForType();
        
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
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/TopicsForType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "56f77ff6da756dd27c1ed16ec721072a";
    }

    public sealed class TopicsForTypeRequest : IRequest
    {
        public string type { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsForTypeRequest()
        {
            type = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicsForTypeRequest(string type)
        {
            this.type = type ?? throw new System.ArgumentNullException(nameof(type));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicsForTypeRequest(Buffer b)
        {
            this.type = BuiltIns.DeserializeString(b);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TopicsForTypeRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.type, b);
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

    public sealed class TopicsForTypeResponse : IResponse
    {
        public string[] topics { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsForTypeResponse()
        {
            topics = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicsForTypeResponse(string[] topics)
        {
            this.topics = topics ?? throw new System.ArgumentNullException(nameof(topics));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicsForTypeResponse(Buffer b)
        {
            this.topics = BuiltIns.DeserializeStringArray(b, 0);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TopicsForTypeResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.topics, b, 0);
        }
        
        public void Validate()
        {
            if (topics is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * topics.Length;
                for (int i = 0; i < topics.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(topics[i]);
                }
                return size;
            }
        }
    }
}
