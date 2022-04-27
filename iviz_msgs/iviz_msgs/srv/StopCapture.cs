using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class StopCapture : IService
    {
        /// Request message.
        [DataMember] public StopCaptureRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public StopCaptureResponse Response { get; set; }
        
        /// Empty constructor.
        public StopCapture()
        {
            Request = StopCaptureRequest.Singleton;
            Response = new StopCaptureResponse();
        }
        
        /// Setter constructor.
        public StopCapture(StopCaptureRequest request)
        {
            Request = request;
            Response = new StopCaptureResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (StopCaptureRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (StopCaptureResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/StopCapture";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "937c9679a518e3a18d831e57125ea522";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class StopCaptureRequest : IRequest<StopCapture, StopCaptureResponse>, IDeserializable<StopCaptureRequest>
    {
    
        /// Constructor for empty message.
        public StopCaptureRequest()
        {
        }
        
        /// Constructor with buffer.
        public StopCaptureRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public StopCaptureRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static StopCaptureRequest? singleton;
        public static StopCaptureRequest Singleton => singleton ??= new StopCaptureRequest();
    
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
    public sealed class StopCaptureResponse : IResponse, IDeserializable<StopCaptureResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
    
        /// Constructor for empty message.
        public StopCaptureResponse()
        {
            Message = "";
        }
        
        /// Explicit constructor.
        public StopCaptureResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        public StopCaptureResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new StopCaptureResponse(ref b);
        
        public StopCaptureResponse RosDeserialize(ref ReadBuffer b) => new StopCaptureResponse(ref b);
    
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
