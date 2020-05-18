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
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "value")] public string Value { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SetParamRequest()
        {
            Name = "";
            Value = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetParamRequest(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetParamRequest(Buffer b)
        {
            Name = b.DeserializeString();
            Value = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new SetParamRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.Name);
            b.Serialize(this.Value);
        }
        
        public void Validate()
        {
            if (Name is null) throw new System.NullReferenceException();
            if (Value is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Value);
                return size;
            }
        }
    }

    public sealed class SetParamResponse : Internal.EmptyResponse
    {
    }
}
