using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class ResetModule : IService
    {
        /// Request message.
        [DataMember] public ResetModuleRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ResetModuleResponse Response { get; set; }
        
        /// Empty constructor.
        public ResetModule()
        {
            Request = new ResetModuleRequest();
            Response = new ResetModuleResponse();
        }
        
        /// Setter constructor.
        public ResetModule(ResetModuleRequest request)
        {
            Request = request;
            Response = new ResetModuleResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ResetModuleRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ResetModuleResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/ResetModule";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "7b2e77c05fb1342786184d949a9f06ed";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ResetModuleRequest : IRequest<ResetModule, ResetModuleResponse>, IDeserializable<ResetModuleRequest>
    {
        // Resets a module. What this entails depends on the specific module.
        /// <summary> Id of the module </summary>
        [DataMember (Name = "id")] public string Id;
    
        /// Constructor for empty message.
        public ResetModuleRequest()
        {
            Id = "";
        }
        
        /// Explicit constructor.
        public ResetModuleRequest(string Id)
        {
            this.Id = Id;
        }
        
        /// Constructor with buffer.
        public ResetModuleRequest(ref ReadBuffer b)
        {
            Id = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ResetModuleRequest(ref b);
        
        public ResetModuleRequest RosDeserialize(ref ReadBuffer b) => new ResetModuleRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Id);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ResetModuleResponse : IResponse, IDeserializable<ResetModuleResponse>
    {
        /// <summary> Whether the operation succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> An error message if success is false </summary>
        [DataMember (Name = "message")] public string Message;
    
        /// Constructor for empty message.
        public ResetModuleResponse()
        {
            Message = "";
        }
        
        /// Explicit constructor.
        public ResetModuleResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        public ResetModuleResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ResetModuleResponse(ref b);
        
        public ResetModuleResponse RosDeserialize(ref ReadBuffer b) => new ResetModuleResponse(ref b);
    
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
