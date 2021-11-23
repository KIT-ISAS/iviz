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
        
        IService IService.Create() => new AddModule();
        
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
        [DataMember (Name = "module_type")] public string ModuleType; // Module type
        [DataMember (Name = "id")] public string Id; // Requested id to identify this module, or empty to autogenerate
    
        /// Constructor for empty message.
        public AddModuleRequest()
        {
            ModuleType = string.Empty;
            Id = string.Empty;
        }
        
        /// Explicit constructor.
        public AddModuleRequest(string ModuleType, string Id)
        {
            this.ModuleType = ModuleType;
            this.Id = Id;
        }
        
        /// Constructor with buffer.
        internal AddModuleRequest(ref Buffer b)
        {
            ModuleType = b.DeserializeString();
            Id = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AddModuleRequest(ref b);
        
        AddModuleRequest IDeserializable<AddModuleRequest>.RosDeserialize(ref Buffer b) => new AddModuleRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ModuleType);
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (ModuleType is null) throw new System.NullReferenceException(nameof(ModuleType));
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(ModuleType) + BuiltIns.GetStringSize(Id);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddModuleResponse : IResponse, IDeserializable<AddModuleResponse>
    {
        [DataMember (Name = "success")] public bool Success; // Whether the retrieval succeeded
        [DataMember (Name = "message")] public string Message; // An error message if success is false
        [DataMember (Name = "id")] public string Id; // An id identifying this module, or empty if error
    
        /// Constructor for empty message.
        public AddModuleResponse()
        {
            Message = string.Empty;
            Id = string.Empty;
        }
        
        /// Explicit constructor.
        public AddModuleResponse(bool Success, string Message, string Id)
        {
            this.Success = Success;
            this.Message = Message;
            this.Id = Id;
        }
        
        /// Constructor with buffer.
        internal AddModuleResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
            Id = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AddModuleResponse(ref b);
        
        AddModuleResponse IDeserializable<AddModuleResponse>.RosDeserialize(ref Buffer b) => new AddModuleResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
        }
    
        public int RosMessageLength => 9 + BuiltIns.GetStringSize(Message) + BuiltIns.GetStringSize(Id);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
