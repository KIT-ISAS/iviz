using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class SetFixedFrame : IService
    {
        /// Request message.
        [DataMember] public SetFixedFrameRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SetFixedFrameResponse Response { get; set; }
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/SetFixedFrame";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "7b2e77c05fb1342786184d949a9f06ed";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetFixedFrameRequest : IRequest<SetFixedFrame, SetFixedFrameResponse>, IDeserializable<SetFixedFrameRequest>
    {
        // Sets the fixed frame
        /// <summary> Id of the frame </summary>
        [DataMember (Name = "id")] public string Id;
    
        /// Constructor for empty message.
        public SetFixedFrameRequest()
        {
            Id = "";
        }
        
        /// Explicit constructor.
        public SetFixedFrameRequest(string Id)
        {
            this.Id = Id;
        }
        
        /// Constructor with buffer.
        public SetFixedFrameRequest(ref ReadBuffer b)
        {
            Id = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SetFixedFrameRequest(ref b);
        
        public SetFixedFrameRequest RosDeserialize(ref ReadBuffer b) => new SetFixedFrameRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Id);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetFixedFrameResponse : IResponse, IDeserializable<SetFixedFrameResponse>
    {
        /// <summary> Whether the operation succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> An error message if success is false </summary>
        [DataMember (Name = "message")] public string Message;
    
        /// Constructor for empty message.
        public SetFixedFrameResponse()
        {
            Message = "";
        }
        
        /// Explicit constructor.
        public SetFixedFrameResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        public SetFixedFrameResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SetFixedFrameResponse(ref b);
        
        public SetFixedFrameResponse RosDeserialize(ref ReadBuffer b) => new SetFixedFrameResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength => 5 + BuiltIns.GetStringSize(Message);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
