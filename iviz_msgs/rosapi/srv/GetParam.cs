using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
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
        [DataMember] public string name { get; set; }
        [DataMember] public string @default { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetParamRequest()
        {
            name = "";
            @default = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetParamRequest(string name, string @default)
        {
            this.name = name ?? throw new System.ArgumentNullException(nameof(name));
            this.@default = @default ?? throw new System.ArgumentNullException(nameof(@default));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetParamRequest(Buffer b)
        {
            this.name = b.DeserializeString();
            this.@default = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new GetParamRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.name);
            b.Serialize(this.@default);
        }
        
        public void Validate()
        {
            if (name is null) throw new System.NullReferenceException();
            if (@default is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(name);
                size += BuiltIns.UTF8.GetByteCount(@default);
                return size;
            }
        }
    }

    public sealed class GetParamResponse : IResponse
    {
        [DataMember] public string value { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetParamResponse()
        {
            value = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetParamResponse(string value)
        {
            this.value = value ?? throw new System.ArgumentNullException(nameof(value));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetParamResponse(Buffer b)
        {
            this.value = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new GetParamResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.value);
        }
        
        public void Validate()
        {
            if (value is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(value);
                return size;
            }
        }
    }
}
