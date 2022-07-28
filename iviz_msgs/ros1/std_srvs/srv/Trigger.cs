using System.Runtime.Serialization;

namespace Iviz.Msgs.StdSrvs
{
    [DataContract]
    public sealed class Trigger : IService
    {
        /// Request message.
        [DataMember] public TriggerRequest Request;
        
        /// Response message.
        [DataMember] public TriggerResponse Response;
        
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
        
        public const string ServiceType = "std_srvs/Trigger";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "937c9679a518e3a18d831e57125ea522";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TriggerRequest : IRequest<Trigger, TriggerResponse>, IDeserializable<TriggerRequest>
    {
    
        public TriggerRequest()
        {
        }
        
        public TriggerRequest(ref ReadBuffer b)
        {
        }
        
        public TriggerRequest(ref ReadBuffer2 b)
        {
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TriggerRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public TriggerRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static TriggerRequest? singleton;
        public static TriggerRequest Singleton => singleton ??= new TriggerRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public void AddRos2MessageLength(ref int c) { }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TriggerResponse : IResponse, IDeserializable<TriggerResponse>
    {
        /// <summary> Indicate successful run of triggered service </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> Informational, e.g. for error messages </summary>
        [DataMember (Name = "message")] public string Message;
    
        public TriggerResponse()
        {
            Message = "";
        }
        
        public TriggerResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        public TriggerResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
        }
        
        public TriggerResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TriggerResponse(ref b);
        
        public TriggerResponse RosDeserialize(ref ReadBuffer b) => new TriggerResponse(ref b);
        
        public TriggerResponse RosDeserialize(ref ReadBuffer2 b) => new TriggerResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Message is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + WriteBuffer.GetStringSize(Message);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Success);
            WriteBuffer2.AddLength(ref c, Message);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
