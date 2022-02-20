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
        public TopicsRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TopicsRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly TopicsRequest Singleton = new TopicsRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
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
        public TopicsResponse(ref ReadBuffer b)
        {
            Topics_ = b.DeserializeStringArray();
            Types = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TopicsResponse(ref b);
        
        public TopicsResponse RosDeserialize(ref ReadBuffer b) => new TopicsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Topics_);
            b.SerializeArray(Types);
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
