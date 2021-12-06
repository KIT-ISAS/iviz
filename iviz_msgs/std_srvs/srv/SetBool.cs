using System.Runtime.Serialization;

namespace Iviz.Msgs.StdSrvs
{
    [DataContract (Name = RosServiceType)]
    public sealed class SetBool : IService
    {
        /// Request message.
        [DataMember] public SetBoolRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SetBoolResponse Response { get; set; }
        
        /// Empty constructor.
        public SetBool()
        {
            Request = new SetBoolRequest();
            Response = new SetBoolResponse();
        }
        
        /// Setter constructor.
        public SetBool(SetBoolRequest request)
        {
            Request = request;
            Response = new SetBoolResponse();
        }
        
        IService IService.Create() => new SetBool();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetBoolRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetBoolResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "std_srvs/SetBool";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "09fb03525b03e7ea1fd3992bafd87e16";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetBoolRequest : IRequest<SetBool, SetBoolResponse>, IDeserializable<SetBoolRequest>
    {
        [DataMember (Name = "data")] public bool Data; // e.g. for hardware enabling / disabling
    
        /// Constructor for empty message.
        public SetBoolRequest()
        {
        }
        
        /// Explicit constructor.
        public SetBoolRequest(bool Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal SetBoolRequest(ref ReadBuffer b)
        {
            Data = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SetBoolRequest(ref b);
        
        public SetBoolRequest RosDeserialize(ref ReadBuffer b) => new SetBoolRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetBoolResponse : IResponse, IDeserializable<SetBoolResponse>
    {
        [DataMember (Name = "success")] public bool Success; // indicate successful run of triggered service
        [DataMember (Name = "message")] public string Message; // informational, e.g. for error messages
    
        /// Constructor for empty message.
        public SetBoolResponse()
        {
            Message = string.Empty;
        }
        
        /// Explicit constructor.
        public SetBoolResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        internal SetBoolResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SetBoolResponse(ref b);
        
        public SetBoolResponse RosDeserialize(ref ReadBuffer b) => new SetBoolResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength => 5 + BuiltIns.GetStringSize(Message);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
