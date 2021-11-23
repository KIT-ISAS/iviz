using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class Topics : IService
    {
        /// Request message.
        [DataMember] public TopicsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TopicsResponse Response { get; set; }
        
        /// Empty constructor.
        public Topics()
        {
            Request = TopicsRequest.Singleton;
            Response = new TopicsResponse();
        }
        
        /// Setter constructor.
        public Topics(TopicsRequest request)
        {
            Request = request;
            Response = new TopicsResponse();
        }
        
        IService IService.Create() => new Topics();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TopicsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TopicsResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/Topics";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "d966d98fc333fa1f3135af765eac1ba8";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TopicsRequest : IRequest<Topics, TopicsResponse>, IDeserializable<TopicsRequest>
    {
    
        /// Constructor for empty message.
        public TopicsRequest()
        {
        }
        
        /// Constructor with buffer.
        internal TopicsRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        TopicsRequest IDeserializable<TopicsRequest>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly TopicsRequest Singleton = new TopicsRequest();
    
        public void RosSerialize(ref Buffer b)
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
    public sealed class TopicsResponse : IResponse, IDeserializable<TopicsResponse>
    {
        [DataMember (Name = "topics")] public string[] Topics_;
        [DataMember (Name = "types")] public string[] Types;
    
        /// Constructor for empty message.
        public TopicsResponse()
        {
            Topics_ = System.Array.Empty<string>();
            Types = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public TopicsResponse(string[] Topics_, string[] Types)
        {
            this.Topics_ = Topics_;
            this.Types = Types;
        }
        
        /// Constructor with buffer.
        internal TopicsResponse(ref Buffer b)
        {
            Topics_ = b.DeserializeStringArray();
            Types = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TopicsResponse(ref b);
        
        TopicsResponse IDeserializable<TopicsResponse>.RosDeserialize(ref Buffer b) => new TopicsResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Topics_, 0);
            b.SerializeArray(Types, 0);
        }
        
        public void RosValidate()
        {
            if (Topics_ is null) throw new System.NullReferenceException(nameof(Topics_));
            for (int i = 0; i < Topics_.Length; i++)
            {
                if (Topics_[i] is null) throw new System.NullReferenceException($"{nameof(Topics_)}[{i}]");
            }
            if (Types is null) throw new System.NullReferenceException(nameof(Types));
            for (int i = 0; i < Types.Length; i++)
            {
                if (Types[i] is null) throw new System.NullReferenceException($"{nameof(Types)}[{i}]");
            }
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetArraySize(Topics_) + BuiltIns.GetArraySize(Types);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
