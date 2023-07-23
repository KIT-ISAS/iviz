using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class StopCapture : IService<StopCaptureRequest, StopCaptureResponse>
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
        
        public IService Generate() => new StopCapture();
        
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
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
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
            Message = b.DeserializeString();
        }
        
        public StopCaptureResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            Message = b.DeserializeString();
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
