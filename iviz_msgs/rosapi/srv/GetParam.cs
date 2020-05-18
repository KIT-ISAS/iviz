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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/GetParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "e36fd90759dbac1c5159140a7fa8c644";
    }

    public sealed class GetParamRequest : IRequest
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
        internal GetParamRequest(Buffer b)
        {
            Name = b.DeserializeString();
            @default = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new GetParamRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.Name);
            b.Serialize(this.@default);
        }
        
        public void Validate()
        {
            if (Name is null) throw new System.NullReferenceException();
            if (@default is null) throw new System.NullReferenceException();
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

    public sealed class GetParamResponse : IResponse
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
        internal GetParamResponse(Buffer b)
        {
            Value = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new GetParamResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.Value);
        }
        
        public void Validate()
        {
            if (Value is null) throw new System.NullReferenceException();
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
