using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetParam : IService
    {
        /// Request message.
        [DataMember] public GetParamRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetParamResponse Response { get; set; }
        
        /// Empty constructor.
        public GetParam()
        {
            Request = new GetParamRequest();
            Response = new GetParamResponse();
        }
        
        /// Setter constructor.
        public GetParam(GetParamRequest request)
        {
            Request = request;
            Response = new GetParamResponse();
        }
        
        IService IService.Create() => new GetParam();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetParamRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetParamResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/GetParam";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "e36fd90759dbac1c5159140a7fa8c644";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetParamRequest : IRequest<GetParam, GetParamResponse>, IDeserializable<GetParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "default")] public string @default;
    
        /// Constructor for empty message.
        public GetParamRequest()
        {
            Name = string.Empty;
            @default = string.Empty;
        }
        
        /// Explicit constructor.
        public GetParamRequest(string Name, string @default)
        {
            this.Name = Name;
            this.@default = @default;
        }
        
        /// Constructor with buffer.
        internal GetParamRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            @default = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetParamRequest(ref b);
        
        public GetParamRequest RosDeserialize(ref ReadBuffer b) => new GetParamRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(@default);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (@default is null) throw new System.NullReferenceException(nameof(@default));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name) + BuiltIns.GetStringSize(@default);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetParamResponse : IResponse, IDeserializable<GetParamResponse>
    {
        [DataMember (Name = "value")] public string Value;
    
        /// Constructor for empty message.
        public GetParamResponse()
        {
            Value = string.Empty;
        }
        
        /// Explicit constructor.
        public GetParamResponse(string Value)
        {
            this.Value = Value;
        }
        
        /// Constructor with buffer.
        internal GetParamResponse(ref ReadBuffer b)
        {
            Value = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetParamResponse(ref b);
        
        public GetParamResponse RosDeserialize(ref ReadBuffer b) => new GetParamResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
            if (Value is null) throw new System.NullReferenceException(nameof(Value));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Value);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
