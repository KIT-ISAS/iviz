using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class StartCapture : IService
    {
        /// Request message.
        [DataMember] public StartCaptureRequest Request;
        
        /// Response message.
        [DataMember] public StartCaptureResponse Response;
        
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
        
        public const string ServiceType = "iviz_msgs/StartCapture";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "ddc13484ad3a5f74f6f36b363081b7e2";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class StartCaptureRequest : IRequest<StartCapture, StartCaptureResponse>, IDeserializable<StartCaptureRequest>
    {
        [DataMember (Name = "resolution_x")] public int ResolutionX;
        [DataMember (Name = "resolution_y")] public int ResolutionY;
        [DataMember (Name = "with_holograms")] public bool WithHolograms;
    
        public StartCaptureRequest()
        {
        }
        
        public StartCaptureRequest(int ResolutionX, int ResolutionY, bool WithHolograms)
        {
            this.ResolutionX = ResolutionX;
            this.ResolutionY = ResolutionY;
            this.WithHolograms = WithHolograms;
        }
        
        public StartCaptureRequest(ref ReadBuffer b)
        {
            b.Deserialize(out ResolutionX);
            b.Deserialize(out ResolutionY);
            b.Deserialize(out WithHolograms);
        }
        
        public StartCaptureRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out ResolutionX);
            b.Deserialize(out ResolutionY);
            b.Deserialize(out WithHolograms);
        }
        
        public StartCaptureRequest RosDeserialize(ref ReadBuffer b) => new StartCaptureRequest(ref b);
        
        public StartCaptureRequest RosDeserialize(ref ReadBuffer2 b) => new StartCaptureRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(ResolutionX);
            b.Serialize(ResolutionY);
            b.Serialize(WithHolograms);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(ResolutionX);
            b.Serialize(ResolutionY);
            b.Serialize(WithHolograms);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 9;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 9;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // ResolutionX
            size += 4; // ResolutionY
            size += 1; // WithHolograms
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class StartCaptureResponse : IResponse, IDeserializable<StartCaptureResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
    
        public StartCaptureResponse()
        {
            Message = "";
        }
        
        public StartCaptureResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        public StartCaptureResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
        }
        
        public StartCaptureResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            Message = b.DeserializeString();
        }
        
        public StartCaptureResponse RosDeserialize(ref ReadBuffer b) => new StartCaptureResponse(ref b);
        
        public StartCaptureResponse RosDeserialize(ref ReadBuffer2 b) => new StartCaptureResponse(ref b);
    
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
            if (Message is null) BuiltIns.ThrowNullReference(nameof(Message));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 5;
                size += WriteBuffer.GetStringSize(Message);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
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
