using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdSrvs
{
    [DataContract]
    public sealed class Trigger : IService<TriggerRequest, TriggerResponse>
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
        
        public IService Generate() => new Trigger();
        
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
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
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
            Message = b.DeserializeString();
        }
        
        public TriggerResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            Message = b.DeserializeString();
        }
        
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
            b.Align4();
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 5;
                size += WriteBuffer.GetStringSize(Message);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Success
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Message);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
