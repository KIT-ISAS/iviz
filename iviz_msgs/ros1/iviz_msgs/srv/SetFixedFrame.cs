using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class SetFixedFrame : IService
    {
        /// Request message.
        [DataMember] public SetFixedFrameRequest Request;
        
        /// Response message.
        [DataMember] public SetFixedFrameResponse Response;
        
        /// Empty constructor.
        public SetFixedFrame()
        {
            Request = new SetFixedFrameRequest();
            Response = new SetFixedFrameResponse();
        }
        
        /// Setter constructor.
        public SetFixedFrame(SetFixedFrameRequest request)
        {
            Request = request;
            Response = new SetFixedFrameResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetFixedFrameRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetFixedFrameResponse)value;
        }
        
        public const string ServiceType = "iviz_msgs/SetFixedFrame";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "7b2e77c05fb1342786184d949a9f06ed";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetFixedFrameRequest : IRequest<SetFixedFrame, SetFixedFrameResponse>, IDeserializable<SetFixedFrameRequest>
    {
        // Sets the fixed frame
        /// <summary> Id of the frame </summary>
        [DataMember (Name = "id")] public string Id;
    
        public SetFixedFrameRequest()
        {
            Id = "";
        }
        
        public SetFixedFrameRequest(string Id)
        {
            this.Id = Id;
        }
        
        public SetFixedFrameRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Id);
        }
        
        public SetFixedFrameRequest(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Id);
        }
        
        public SetFixedFrameRequest RosDeserialize(ref ReadBuffer b) => new SetFixedFrameRequest(ref b);
        
        public SetFixedFrameRequest RosDeserialize(ref ReadBuffer2 b) => new SetFixedFrameRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Id);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Id);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetFixedFrameResponse : IResponse, IDeserializable<SetFixedFrameResponse>
    {
        /// <summary> Whether the operation succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> An error message if success is false </summary>
        [DataMember (Name = "message")] public string Message;
    
        public SetFixedFrameResponse()
        {
            Message = "";
        }
        
        public SetFixedFrameResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        public SetFixedFrameResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
        }
        
        public SetFixedFrameResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
        }
        
        public SetFixedFrameResponse RosDeserialize(ref ReadBuffer b) => new SetFixedFrameResponse(ref b);
        
        public SetFixedFrameResponse RosDeserialize(ref ReadBuffer2 b) => new SetFixedFrameResponse(ref b);
    
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
