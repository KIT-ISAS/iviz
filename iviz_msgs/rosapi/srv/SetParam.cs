using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class SetParam : IService
    {
        /// Request message.
        [DataMember] public SetParamRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SetParamResponse Response { get; set; }
        
        /// Empty constructor.
        public SetParam()
        {
            Request = new SetParamRequest();
            Response = SetParamResponse.Singleton;
        }
        
        /// Setter constructor.
        public SetParam(SetParamRequest request)
        {
            Request = request;
            Response = SetParamResponse.Singleton;
        }
        
        IService IService.Create() => new SetParam();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetParamRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetParamResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/SetParam";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "bc6ccc4a57f61779c8eaae61e9f422e0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetParamRequest : IRequest<SetParam, SetParamResponse>, IDeserializable<SetParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public string Value;
    
        /// Constructor for empty message.
        public SetParamRequest()
        {
            Name = string.Empty;
            Value = string.Empty;
        }
        
        /// Explicit constructor.
        public SetParamRequest(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// Constructor with buffer.
        internal SetParamRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            Value = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SetParamRequest(ref b);
        
        public SetParamRequest RosDeserialize(ref ReadBuffer b) => new SetParamRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Value is null) throw new System.NullReferenceException(nameof(Value));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name) + BuiltIns.GetStringSize(Value);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetParamResponse : IResponse, IDeserializable<SetParamResponse>
    {
    
        /// Constructor for empty message.
        public SetParamResponse()
        {
        }
        
        /// Constructor with buffer.
        internal SetParamResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public SetParamResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly SetParamResponse Singleton = new SetParamResponse();
    
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
}
