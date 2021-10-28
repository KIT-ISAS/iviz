using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/SetParam")]
    public sealed class SetParam : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SetParamRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SetParamResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SetParam()
        {
            Request = new SetParamRequest();
            Response = SetParamResponse.Singleton;
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/SetParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "bc6ccc4a57f61779c8eaae61e9f422e0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetParamRequest : IRequest<SetParam, SetParamResponse>, IDeserializable<SetParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public string Value;
    
        /// <summary> Constructor for empty message. </summary>
        public SetParamRequest()
        {
            Name = string.Empty;
            Value = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetParamRequest(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetParamRequest(ref Buffer b)
        {
            Name = b.DeserializeString();
            Value = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SetParamRequest(ref b);
        }
        
        SetParamRequest IDeserializable<SetParamRequest>.RosDeserialize(ref Buffer b)
        {
            return new SetParamRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Value);
        }
        
        public void Dispose()
        {
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
    
        /// <summary> Constructor for empty message. </summary>
        public SetParamResponse()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetParamResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        SetParamResponse IDeserializable<SetParamResponse>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly SetParamResponse Singleton = new SetParamResponse();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void Dispose()
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
}
