using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/AddModule")]
    public sealed class AddModule : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public AddModuleRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public AddModuleResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public AddModule()
        {
            Request = new AddModuleRequest();
            Response = new AddModuleResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/AddModule";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "4e3219ce88a082923b30b014872396ae";
    }

    public sealed class AddModuleRequest : IRequest, IDeserializable<AddModuleRequest>
    {
        // Adds a module by type
        [DataMember (Name = "module_type")] public string ModuleType { get; set; } // Module type
    
        /// <summary> Constructor for empty message. </summary>
        public AddModuleRequest()
        {
            ModuleType = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddModuleRequest(string ModuleType)
        {
            this.ModuleType = ModuleType;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddModuleRequest(ref Buffer b)
        {
            ModuleType = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AddModuleRequest(ref b);
        }
        
        AddModuleRequest IDeserializable<AddModuleRequest>.RosDeserialize(ref Buffer b)
        {
            return new AddModuleRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ModuleType);
        }
        
        public void RosValidate()
        {
            if (ModuleType is null) throw new System.NullReferenceException(nameof(ModuleType));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(ModuleType);
                return size;
            }
        }
    }

    public sealed class AddModuleResponse : IResponse, IDeserializable<AddModuleResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; } // Whether the retrieval succeeded
        [DataMember (Name = "message")] public string Message { get; set; } // An error message if success is false
        [DataMember (Name = "id")] public string Id { get; set; } // An id identifying this module
    
        /// <summary> Constructor for empty message. </summary>
        public AddModuleResponse()
        {
            Message = "";
            Id = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddModuleResponse(bool Success, string Message, string Id)
        {
            this.Success = Success;
            this.Message = Message;
            this.Id = Id;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddModuleResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
            Id = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AddModuleResponse(ref b);
        }
        
        AddModuleResponse IDeserializable<AddModuleResponse>.RosDeserialize(ref Buffer b)
        {
            return new AddModuleResponse(ref b);
        }
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 9;
                size += BuiltIns.UTF8.GetByteCount(Message);
                size += BuiltIns.UTF8.GetByteCount(Id);
                return size;
            }
        }
    }
}
