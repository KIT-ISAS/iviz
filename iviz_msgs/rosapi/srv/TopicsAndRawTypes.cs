using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class TopicsAndRawTypes : IService
    {
        /// Request message.
        [DataMember] public TopicsAndRawTypesRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TopicsAndRawTypesResponse Response { get; set; }
        
        /// Empty constructor.
        public TopicsAndRawTypes()
        {
            Request = TopicsAndRawTypesRequest.Singleton;
            Response = new TopicsAndRawTypesResponse();
        }
        
        /// Setter constructor.
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/TopicsAndRawTypes";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "e1432466c8f64316723276ba07c59d12";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TopicsAndRawTypesRequest : IRequest<TopicsAndRawTypes, TopicsAndRawTypesResponse>, IDeserializable<TopicsAndRawTypesRequest>
    {
    
        /// Constructor for empty message.
        public TopicsAndRawTypesRequest()
        {
        }
        
        /// Constructor with buffer.
        public TopicsAndRawTypesRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TopicsAndRawTypesRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly TopicsAndRawTypesRequest Singleton = new TopicsAndRawTypesRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TopicsAndRawTypesResponse : IResponse, IDeserializable<TopicsAndRawTypesResponse>
    {
        [DataMember (Name = "topics")] public string[] Topics;
        [DataMember (Name = "types")] public string[] Types;
        [DataMember (Name = "typedefs_full_text")] public string[] TypedefsFullText;
    
        /// Constructor for empty message.
        public TopicsAndRawTypesResponse()
        {
            Topics = System.Array.Empty<string>();
            Types = System.Array.Empty<string>();
            TypedefsFullText = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public TopicsAndRawTypesResponse(string[] Topics, string[] Types, string[] TypedefsFullText)
        {
            this.Topics = Topics;
            this.Types = Types;
            this.TypedefsFullText = TypedefsFullText;
        }
        
        /// Constructor with buffer.
        public TopicsAndRawTypesResponse(ref ReadBuffer b)
        {
            Topics = b.DeserializeStringArray();
            Types = b.DeserializeStringArray();
            TypedefsFullText = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TopicsAndRawTypesResponse(ref b);
        
        public TopicsAndRawTypesResponse RosDeserialize(ref ReadBuffer b) => new TopicsAndRawTypesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Topics);
            b.SerializeArray(Types);
            b.SerializeArray(TypedefsFullText);
        }
        
        public void RosValidate()
        {
            if (Topics is null) throw new System.NullReferenceException(nameof(Topics));
            for (int i = 0; i < Topics.Length; i++)
            {
                if (Topics[i] is null) throw new System.NullReferenceException($"{nameof(Topics)}[{i}]");
            }
            if (Types is null) throw new System.NullReferenceException(nameof(Types));
            for (int i = 0; i < Types.Length; i++)
            {
                if (Types[i] is null) throw new System.NullReferenceException($"{nameof(Types)}[{i}]");
            }
            if (TypedefsFullText is null) throw new System.NullReferenceException(nameof(TypedefsFullText));
            for (int i = 0; i < TypedefsFullText.Length; i++)
            {
                if (TypedefsFullText[i] is null) throw new System.NullReferenceException($"{nameof(TypedefsFullText)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.GetArraySize(Topics);
                size += BuiltIns.GetArraySize(Types);
                size += BuiltIns.GetArraySize(TypedefsFullText);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
