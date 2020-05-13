using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
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
            Response = new SetParamResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetParam(SetParamRequest request)
        {
            Request = request;
            Response = new SetParamResponse();
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/SetParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "bc6ccc4a57f61779c8eaae61e9f422e0";
    }

    public sealed class SetParamRequest : IRequest
    {
        [DataMember] public string name { get; set; }
        [DataMember] public string value { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SetParamRequest()
        {
            name = "";
            value = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetParamRequest(string name, string value)
        {
            this.name = name ?? throw new System.ArgumentNullException(nameof(name));
            this.value = value ?? throw new System.ArgumentNullException(nameof(value));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetParamRequest(Buffer b)
        {
            this.name = b.DeserializeString();
            this.value = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new SetParamRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.name);
            b.Serialize(this.value);
        }
        
        public void Validate()
        {
            if (name is null) throw new System.NullReferenceException();
            if (value is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(name);
                size += BuiltIns.UTF8.GetByteCount(value);
                return size;
            }
        }
    }

    public sealed class SetParamResponse : Internal.EmptyResponse
    {
    }
}
