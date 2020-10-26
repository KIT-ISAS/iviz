using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/TopicsAndRawTypes")]
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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/TopicsAndRawTypes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "e1432466c8f64316723276ba07c59d12";
    }

    public sealed class TopicsAndRawTypesRequest : IRequest, IDeserializable<TopicsAndRawTypesRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsAndRawTypesRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicsAndRawTypesRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TopicsAndRawTypesRequest(ref b);
        }
        
        TopicsAndRawTypesRequest IDeserializable<TopicsAndRawTypesRequest>.RosDeserialize(ref Buffer b)
        {
            return new TopicsAndRawTypesRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 0;
    }

    public sealed class TopicsAndRawTypesResponse : IResponse, IDeserializable<TopicsAndRawTypesResponse>
    {
        [DataMember (Name = "topics")] public string[] Topics { get; set; }
        [DataMember (Name = "types")] public string[] Types { get; set; }
        [DataMember (Name = "typedefs_full_text")] public string[] TypedefsFullText { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsAndRawTypesResponse()
        {
            Topics = System.Array.Empty<string>();
            Types = System.Array.Empty<string>();
            TypedefsFullText = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicsAndRawTypesResponse(string[] Topics, string[] Types, string[] TypedefsFullText)
        {
            this.Topics = Topics;
            this.Types = Types;
            this.TypedefsFullText = TypedefsFullText;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicsAndRawTypesResponse(ref Buffer b)
        {
            Topics = b.DeserializeStringArray();
            Types = b.DeserializeStringArray();
            TypedefsFullText = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TopicsAndRawTypesResponse(ref b);
        }
        
        TopicsAndRawTypesResponse IDeserializable<TopicsAndRawTypesResponse>.RosDeserialize(ref Buffer b)
        {
            return new TopicsAndRawTypesResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Topics, 0);
            b.SerializeArray(Types, 0);
            b.SerializeArray(TypedefsFullText, 0);
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
                size += 4 * Topics.Length;
                foreach (string s in Topics)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 4 * Types.Length;
                foreach (string s in Types)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 4 * TypedefsFullText.Length;
                foreach (string s in TypedefsFullText)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    }
}
