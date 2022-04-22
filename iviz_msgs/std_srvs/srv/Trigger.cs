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
        public TriggerRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TriggerRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static TriggerRequest? singleton;
        public static TriggerRequest Singleton => singleton ??= new TriggerRequest();
    
        public void RosSerialize(ref WriteBuffer b)
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

    [DataContract]
    public sealed class TriggerResponse : IResponse, IDeserializable<TriggerResponse>
    {
        /// <summary> Indicate successful run of triggered service </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> Informational, e.g. for error messages </summary>
        [DataMember (Name = "message")] public string Message;
    
        /// Constructor for empty message.
        public TriggerResponse()
        {
            Message = "";
        }
        
        /// Explicit constructor.
        public TriggerResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        public TriggerResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TriggerResponse(ref b);
        
        public TriggerResponse RosDeserialize(ref ReadBuffer b) => new TriggerResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Message is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + BuiltIns.GetStringSize(Message);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
