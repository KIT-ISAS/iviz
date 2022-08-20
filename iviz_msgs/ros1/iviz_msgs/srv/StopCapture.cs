using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class StopCapture : IService
    {
        /// Request message.
        [DataMember] public StopCaptureRequest Request;
        
        /// Response message.
        [DataMember] public StopCaptureResponse Response;
        
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
        
        public const string ServiceType = "iviz_msgs/StopCapture";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "937c9679a518e3a18d831e57125ea522";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class StopCaptureRequest : IRequest<StopCapture, StopCaptureResponse>, IDeserializable<StopCaptureRequest>
    {
    
        public StopCaptureRequest()
        {
        }
        
        public StopCaptureRequest(ref ReadBuffer b)
        {
        }
        
        public StopCaptureRequest(ref ReadBuffer2 b)
        {
        }
        
        public StopCaptureRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public StopCaptureRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static StopCaptureRequest? singleton;
        public static StopCaptureRequest Singleton => singleton ??= new StopCaptureRequest();
    
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
        
        public int AddRos2MessageLength(int c) => c;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class StopCaptureResponse : IResponse, IDeserializable<StopCaptureResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
    
        public StopCaptureResponse()
        {
            Message = "";
        }
        
        public StopCaptureResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        public StopCaptureResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
        }
        
        public StopCaptureResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            b.DeserializeString(out Message);
        }
        
        public StopCaptureResponse RosDeserialize(ref ReadBuffer b) => new StopCaptureResponse(ref b);
        
        public StopCaptureResponse RosDeserialize(ref ReadBuffer2 b) => new StopCaptureResponse(ref b);
    
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Success
            c = WriteBuffer2.AddLength(c, Message);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
