using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/GetParam")]
    public sealed class GetParam : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetParamRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetParamResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetParam()
        {
            Request = new GetParamRequest();
            Response = new GetParamResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/GetParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "e36fd90759dbac1c5159140a7fa8c644";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetParamRequest : IRequest<GetParam, GetParamResponse>, IDeserializable<GetParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "default")] public string @default;
    
        /// <summary> Constructor for empty message. </summary>
        public GetParamRequest()
        {
            Name = string.Empty;
            @default = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetParamRequest(string Name, string @default)
        {
            this.Name = Name;
            this.@default = @default;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetParamRequest(ref Buffer b)
        {
            Name = b.DeserializeString();
            @default = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetParamRequest(ref b);
        }
        
        GetParamRequest IDeserializable<GetParamRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetParamRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
    
        /// <summary> Constructor for empty message. </summary>
        public GetParamResponse()
        {
            Value = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetParamResponse(string Value)
        {
            this.Value = Value;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetParamResponse(ref Buffer b)
        {
            Value = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetParamResponse(ref b);
        }
        
        GetParamResponse IDeserializable<GetParamResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetParamResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
