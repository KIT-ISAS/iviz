using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class TopicsAndRawTypes : IService
    {
        /// <summary> Request message. </summary>
        public TopicsAndRawTypesRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public TopicsAndRawTypesResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TopicsAndRawTypes()
        {
            Request = new TopicsAndRawTypesRequest();
            Response = new TopicsAndRawTypesResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TopicsAndRawTypes(TopicsAndRawTypesRequest request)
        {
            Request = request;
            Response = new TopicsAndRawTypesResponse();
        }
        
        public IService Create() => new TopicsAndRawTypes();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TopicsAndRawTypesRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TopicsAndRawTypesResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/TopicsAndRawTypes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "e1432466c8f64316723276ba07c59d12";
    }

    public sealed class TopicsAndRawTypesRequest : IRequest
    {
        
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsAndRawTypesRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicsAndRawTypesRequest(Buffer b)
        {
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TopicsAndRawTypesRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 0;
    }

    public sealed class TopicsAndRawTypesResponse : IResponse
    {
        public string[] topics { get; set; }
        public string[] types { get; set; }
        public string[] typedefs_full_text { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsAndRawTypesResponse()
        {
            topics = System.Array.Empty<string>();
            types = System.Array.Empty<string>();
            typedefs_full_text = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicsAndRawTypesResponse(string[] topics, string[] types, string[] typedefs_full_text)
        {
            this.topics = topics ?? throw new System.ArgumentNullException(nameof(topics));
            this.types = types ?? throw new System.ArgumentNullException(nameof(types));
            this.typedefs_full_text = typedefs_full_text ?? throw new System.ArgumentNullException(nameof(typedefs_full_text));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicsAndRawTypesResponse(Buffer b)
        {
            this.topics = BuiltIns.DeserializeStringArray(b, 0);
            this.types = BuiltIns.DeserializeStringArray(b, 0);
            this.typedefs_full_text = BuiltIns.DeserializeStringArray(b, 0);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TopicsAndRawTypesResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.topics, b, 0);
            BuiltIns.Serialize(this.types, b, 0);
            BuiltIns.Serialize(this.typedefs_full_text, b, 0);
        }
        
        public void Validate()
        {
            if (topics is null) throw new System.NullReferenceException();
            if (types is null) throw new System.NullReferenceException();
            if (typedefs_full_text is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += 4 * topics.Length;
                for (int i = 0; i < topics.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(topics[i]);
                }
                size += 4 * types.Length;
                for (int i = 0; i < types.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(types[i]);
                }
                size += 4 * typedefs_full_text.Length;
                for (int i = 0; i < typedefs_full_text.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(typedefs_full_text[i]);
                }
                return size;
            }
        }
    }
}
