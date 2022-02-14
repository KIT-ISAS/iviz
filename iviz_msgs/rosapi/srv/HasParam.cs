using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class HasParam : IService
    {
        /// Request message.
        [DataMember] public HasParamRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public HasParamResponse Response { get; set; }
        
        /// Empty constructor.
        public HasParam()
        {
            Request = new HasParamRequest();
            Response = new HasParamResponse();
        }
        
        /// Setter constructor.
        public HasParam(HasParamRequest request)
        {
            Request = request;
            Response = new HasParamResponse();
        }
        
        IService IService.Create() => new HasParam();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (HasParamRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (HasParamResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/HasParam";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "ed3df286bd6dff9b961770f577454ea9";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class HasParamRequest : IRequest<HasParam, HasParamResponse>, IDeserializable<HasParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
    
        /// Constructor for empty message.
        public HasParamRequest()
        {
            Name = "";
        }
        
        /// Explicit constructor.
        public HasParamRequest(string Name)
        {
            this.Name = Name;
        }
        
        /// Constructor with buffer.
        public HasParamRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new HasParamRequest(ref b);
        
        public HasParamRequest RosDeserialize(ref ReadBuffer b) => new HasParamRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Name);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class HasParamResponse : IResponse, IDeserializable<HasParamResponse>
    {
        [DataMember (Name = "exists")] public bool Exists;
    
        /// Constructor for empty message.
        public HasParamResponse()
        {
        }
        
        /// Explicit constructor.
        public HasParamResponse(bool Exists)
        {
            this.Exists = Exists;
        }
        
        /// Constructor with buffer.
        public HasParamResponse(ref ReadBuffer b)
        {
            Exists = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new HasParamResponse(ref b);
        
        public HasParamResponse RosDeserialize(ref ReadBuffer b) => new HasParamResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Exists);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
