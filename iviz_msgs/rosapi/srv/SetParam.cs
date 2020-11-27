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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/SetParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "bc6ccc4a57f61779c8eaae61e9f422e0";
    }

    [DataContract]
    public sealed class SetParamRequest : IRequest, IDeserializable<SetParamRequest>
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
        public SetParamRequest(ref Buffer b)
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
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Value is null) throw new System.NullReferenceException(nameof(Value));
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

    [DataContract]
    public sealed class SetParamResponse : Internal.EmptyResponse
    {
    }
}
