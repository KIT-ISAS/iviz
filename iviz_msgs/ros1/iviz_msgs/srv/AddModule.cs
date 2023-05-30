using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class AddModule : IService
    {
        /// Request message.
        [DataMember] public AddModuleRequest Request;
        
        /// Response message.
        [DataMember] public AddModuleResponse Response;
        
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
        
        public const string ServiceType = "iviz_msgs/AddModule";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "9c05ee8917d1e268371f79ef13584f64";
        
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
    
        public AddModuleRequest()
        {
            ModuleType = "";
            Id = "";
        }
        
        public AddModuleRequest(string ModuleType, string Id)
        {
            this.ModuleType = ModuleType;
            this.Id = Id;
        }
        
        public AddModuleRequest(ref ReadBuffer b)
        {
            ModuleType = b.DeserializeString();
            Id = b.DeserializeString();
        }
        
        public AddModuleRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            ModuleType = b.DeserializeString();
            b.Align4();
            Id = b.DeserializeString();
        }
        
        public AddModuleRequest RosDeserialize(ref ReadBuffer b) => new AddModuleRequest(ref b);
        
        public AddModuleRequest RosDeserialize(ref ReadBuffer2 b) => new AddModuleRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(ModuleType);
            b.Serialize(Id);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(ModuleType);
            b.Align4();
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(ModuleType, nameof(ModuleType));
            BuiltIns.ThrowIfNull(Id, nameof(Id));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += WriteBuffer.GetStringSize(ModuleType);
                size += WriteBuffer.GetStringSize(Id);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, ModuleType);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            return size;
        }
    
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
    
        public AddModuleResponse()
        {
            Message = "";
            Id = "";
        }
        
        public AddModuleResponse(bool Success, string Message, string Id)
        {
            this.Success = Success;
            this.Message = Message;
            this.Id = Id;
        }
        
        public AddModuleResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
            Id = b.DeserializeString();
        }
        
        public AddModuleResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            Message = b.DeserializeString();
            b.Align4();
            Id = b.DeserializeString();
        }
        
        public AddModuleResponse RosDeserialize(ref ReadBuffer b) => new AddModuleResponse(ref b);
        
        public AddModuleResponse RosDeserialize(ref ReadBuffer2 b) => new AddModuleResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.Serialize(Id);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Align4();
            b.Serialize(Message);
            b.Align4();
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Message, nameof(Message));
            BuiltIns.ThrowIfNull(Id, nameof(Id));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 9;
                size += WriteBuffer.GetStringSize(Message);
                size += WriteBuffer.GetStringSize(Id);
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
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
