using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
    public sealed class TopicsForType : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TopicsForTypeRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TopicsForTypeResponse Response { get; set; }
        
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/TopicsForType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "56f77ff6da756dd27c1ed16ec721072a";
    }

    public sealed class TopicsForTypeRequest : IRequest
    {
        [DataMember] public string type { get; set; }
    
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
            this.type = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TopicsForTypeRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.type);
        }
        
        public void Validate()
        {
            if (type is null) throw new System.NullReferenceException();
        }
    
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
        [DataMember] public string[] topics { get; set; }
    
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
            this.topics = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TopicsForTypeResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.topics, 0);
        }
        
        public void Validate()
        {
            if (topics is null) throw new System.NullReferenceException();
        }
    
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
