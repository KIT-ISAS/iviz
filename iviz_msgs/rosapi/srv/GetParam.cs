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
    }

    [DataContract]
    public sealed class GetParamRequest : IRequest<GetParam, GetParamResponse>, IDeserializable<GetParamRequest>
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "default")] public string @default { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetParamRequest()
        {
            Name = "";
            @default = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetParamRequest(string Name, string @default)
        {
            this.Name = Name;
            this.@default = @default;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetParamRequest(ref Buffer b)
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
            return new(ref b);
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
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(@default);
                return size;
            }
        }
    }

    [DataContract]
    public sealed class GetParamResponse : IResponse, IDeserializable<GetParamResponse>
    {
        [DataMember (Name = "value")] public string Value { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetParamResponse()
        {
            Value = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetParamResponse(string Value)
        {
            this.Value = Value;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetParamResponse(ref Buffer b)
        {
            Value = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetParamResponse(ref b);
        }
        
        GetParamResponse IDeserializable<GetParamResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
            if (Value is null) throw new System.NullReferenceException(nameof(Value));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Value);
                return size;
            }
        }
    }
}
