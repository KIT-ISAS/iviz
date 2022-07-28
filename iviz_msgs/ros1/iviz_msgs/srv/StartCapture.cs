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
            b.Deserialize(out ResolutionX);
            b.Deserialize(out ResolutionY);
            b.Deserialize(out WithHolograms);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new StartCaptureRequest(ref b);
        
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
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, ResolutionX);
            WriteBuffer2.AddLength(ref c, ResolutionY);
            WriteBuffer2.AddLength(ref c, WithHolograms);
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
            b.DeserializeString(out Message);
        }
        
        public StartCaptureResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new StartCaptureResponse(ref b);
        
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
