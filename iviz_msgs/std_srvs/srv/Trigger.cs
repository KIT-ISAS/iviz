using System.Runtime.Serialization;

namespace Iviz.Msgs.StdSrvs
{
    [DataContract (Name = RosServiceType)]
    public sealed class Trigger : IService
    {
        /// Request message.
        [DataMember] public TriggerRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TriggerResponse Response { get; set; }
        
        /// Empty constructor.
        public Trigger()
        {
            Request = TriggerRequest.Singleton;
            Response = new TriggerResponse();
        }
        
        /// Setter constructor.
        public Trigger(TriggerRequest request)
        {
            Request = request;
            Response = new TriggerResponse();
        }
        
        IService IService.Create() => new Trigger();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TriggerRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TriggerResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "std_srvs/Trigger";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "937c9679a518e3a18d831e57125ea522";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TriggerRequest : IRequest<Trigger, TriggerResponse>, IDeserializable<TriggerRequest>
    {
    
        /// Constructor for empty message.
        public TriggerRequest()
        {
        }
        
        /// Constructor with buffer.
        internal TriggerRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        TriggerRequest IDeserializable<TriggerRequest>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly TriggerRequest Singleton = new TriggerRequest();
    
        public void RosSerialize(ref Buffer b)
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

    [DataContract]
    public sealed class TriggerResponse : IResponse, IDeserializable<TriggerResponse>
    {
        [DataMember (Name = "success")] public bool Success; // indicate successful run of triggered service
        [DataMember (Name = "message")] public string Message; // informational, e.g. for error messages
    
        /// Constructor for empty message.
        public TriggerResponse()
        {
            Message = string.Empty;
        }
        
        /// Explicit constructor.
        public TriggerResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        internal TriggerResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TriggerResponse(ref b);
        
        TriggerResponse IDeserializable<TriggerResponse>.RosDeserialize(ref Buffer b) => new TriggerResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
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
