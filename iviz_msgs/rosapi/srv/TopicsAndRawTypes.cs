using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
    public sealed class TopicsAndRawTypes : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TopicsAndRawTypesRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TopicsAndRawTypesResponse Response { get; set; }
        
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
        
        IService IService.Create() => new TopicsAndRawTypes();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/TopicsAndRawTypes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "e1432466c8f64316723276ba07c59d12";
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
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TopicsAndRawTypesRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 0;
    }

    public sealed class TopicsAndRawTypesResponse : IResponse
    {
        [DataMember] public string[] topics { get; set; }
        [DataMember] public string[] types { get; set; }
        [DataMember] public string[] typedefs_full_text { get; set; }
    
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
            this.topics = b.DeserializeStringArray();
            this.types = b.DeserializeStringArray();
            this.typedefs_full_text = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TopicsAndRawTypesResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.topics, 0);
            b.SerializeArray(this.types, 0);
            b.SerializeArray(this.typedefs_full_text, 0);
        }
        
        public void Validate()
        {
            if (topics is null) throw new System.NullReferenceException();
            for (int i = 0; i < topics.Length; i++)
            {
                if (topics[i] is null) throw new System.NullReferenceException();
            }
            if (types is null) throw new System.NullReferenceException();
            for (int i = 0; i < types.Length; i++)
            {
                if (types[i] is null) throw new System.NullReferenceException();
            }
            if (typedefs_full_text is null) throw new System.NullReferenceException();
            for (int i = 0; i < typedefs_full_text.Length; i++)
            {
                if (typedefs_full_text[i] is null) throw new System.NullReferenceException();
            }
        }
    
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
