using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class StartCapture : IService
    {
        /// Request message.
        [DataMember] public StartCaptureRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public StartCaptureResponse Response { get; set; }
        
        /// Empty constructor.
        public StartCapture()
        {
            Request = new StartCaptureRequest();
            Response = new StartCaptureResponse();
        }
        
        /// Setter constructor.
        public StartCapture(StartCaptureRequest request)
        {
            Request = request;
            Response = new StartCaptureResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (StartCaptureRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (StartCaptureResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/StartCapture";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "ddc13484ad3a5f74f6f36b363081b7e2";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class StartCaptureRequest : IRequest<StartCapture, StartCaptureResponse>, IDeserializable<StartCaptureRequest>
    {
        [DataMember (Name = "resolution_x")] public int ResolutionX;
        [DataMember (Name = "resolution_y")] public int ResolutionY;
        [DataMember (Name = "with_holograms")] public bool WithHolograms;
    
        /// Constructor for empty message.
        public StartCaptureRequest()
        {
        }
        
        /// Explicit constructor.
        public StartCaptureRequest(int ResolutionX, int ResolutionY, bool WithHolograms)
        {
            this.ResolutionX = ResolutionX;
            this.ResolutionY = ResolutionY;
            this.WithHolograms = WithHolograms;
        }
        
        /// Constructor with buffer.
        public StartCaptureRequest(ref ReadBuffer b)
        {
            b.Deserialize(out ResolutionX);
            b.Deserialize(out ResolutionY);
            b.Deserialize(out WithHolograms);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new StartCaptureRequest(ref b);
        
        public StartCaptureRequest RosDeserialize(ref ReadBuffer b) => new StartCaptureRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(ResolutionX);
            b.Serialize(ResolutionY);
            b.Serialize(WithHolograms);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 9;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class StartCaptureResponse : IResponse, IDeserializable<StartCaptureResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
    
        /// Constructor for empty message.
        public StartCaptureResponse()
        {
            Message = "";
        }
        
        /// Explicit constructor.
        public StartCaptureResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        public StartCaptureResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new StartCaptureResponse(ref b);
        
        public StartCaptureResponse RosDeserialize(ref ReadBuffer b) => new StartCaptureResponse(ref b);
    
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
