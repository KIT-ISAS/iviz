using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class AddModule : IService
    {
        /// Request message.
        [DataMember] public AddModuleRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public AddModuleResponse Response { get; set; }
        
        /// Empty constructor.
        public AddModule()
        {
            Request = new AddModuleRequest();
            Response = new AddModuleResponse();
        }
        
        /// Setter constructor.
        public AddModule(AddModuleRequest request)
        {
            Request = request;
            Response = new AddModuleResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (AddModuleRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (AddModuleResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/AddModule";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "9c05ee8917d1e268371f79ef13584f64";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddModuleRequest : IRequest<AddModule, AddModuleResponse>, IDeserializable<AddModuleRequest>
    {
        // Adds a module by type
        /// <summary> Module type </summary>
        [DataMember (Name = "module_type")] public string ModuleType;
        /// <summary> Requested id to identify this module, or empty to autogenerate </summary>
        [DataMember (Name = "id")] public string Id;
    
        /// Constructor for empty message.
        public AddModuleRequest()
        {
            ModuleType = "";
            Id = "";
        }
        
        /// Explicit constructor.
        public AddModuleRequest(string ModuleType, string Id)
        {
            this.ModuleType = ModuleType;
            this.Id = Id;
        }
        
        /// Constructor with buffer.
        public AddModuleRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out ModuleType);
            b.DeserializeString(out Id);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new AddModuleRequest(ref b);
        
        public AddModuleRequest RosDeserialize(ref ReadBuffer b) => new AddModuleRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(ModuleType);
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (ModuleType is null) BuiltIns.ThrowNullReference();
            if (Id is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(ModuleType) + BuiltIns.GetStringSize(Id);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddModuleResponse : IResponse, IDeserializable<AddModuleResponse>
    {
        /// <summary> Whether the retrieval succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> An error message if success is false </summary>
        [DataMember (Name = "message")] public string Message;
        /// <summary> An id identifying this module, or empty if error </summary>
        [DataMember (Name = "id")] public string Id;
    
        /// Constructor for empty message.
        public AddModuleResponse()
        {
            Message = "";
            Id = "";
        }
        
        /// Explicit constructor.
        public AddModuleResponse(bool Success, string Message, string Id)
        {
            this.Success = Success;
            this.Message = Message;
            this.Id = Id;
        }
        
        /// Constructor with buffer.
        public AddModuleResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
            b.DeserializeString(out Id);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new AddModuleResponse(ref b);
        
        public AddModuleResponse RosDeserialize(ref ReadBuffer b) => new AddModuleResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Message is null) BuiltIns.ThrowNullReference();
            if (Id is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 9 + BuiltIns.GetStringSize(Message) + BuiltIns.GetStringSize(Id);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
