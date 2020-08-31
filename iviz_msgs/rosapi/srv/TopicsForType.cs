using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/TopicsForType")]
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
        [DataMember (Name = "type")] public string Type { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsForTypeRequest()
        {
            Type = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicsForTypeRequest(string Type)
        {
            this.Type = Type;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicsForTypeRequest(Buffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new TopicsForTypeRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
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

    public sealed class TopicsForTypeResponse : IResponse
    {
        [DataMember (Name = "topics")] public string[] Topics { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsForTypeResponse()
        {
            Topics = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicsForTypeResponse(string[] Topics)
        {
            this.Topics = Topics;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicsForTypeResponse(Buffer b)
        {
            Topics = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new TopicsForTypeResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(Topics, 0);
        }
        
        public void RosValidate()
        {
            if (Topics is null) throw new System.NullReferenceException(nameof(Topics));
            for (int i = 0; i < Topics.Length; i++)
            {
                if (Topics[i] is null) throw new System.NullReferenceException($"{nameof(Topics)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * Topics.Length;
                for (int i = 0; i < Topics.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(Topics[i]);
                }
                return size;
            }
        }
    }
}
