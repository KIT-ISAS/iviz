using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class UpdateModule : IService
    {
        /// Request message.
        [DataMember] public UpdateModuleRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public UpdateModuleResponse Response { get; set; }
        
        /// Empty constructor.
        public UpdateModule()
        {
            Request = new UpdateModuleRequest();
            Response = new UpdateModuleResponse();
        }
        
        /// Setter constructor.
        public UpdateModule(UpdateModuleRequest request)
        {
            Request = request;
            Response = new UpdateModuleResponse();
        }
        
        IService IService.Create() => new UpdateModule();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (UpdateModuleRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (UpdateModuleResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/UpdateModule";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "9b8bbde938619f17558ceabafe5f3a13";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateModuleRequest : IRequest<UpdateModule, UpdateModuleResponse>, IDeserializable<UpdateModuleRequest>
    {
        // Updates a module
        /// <summary> Id of the module </summary>
        [DataMember (Name = "id")] public string Id;
        /// <summary> The fields to be updated </summary>
        [DataMember (Name = "fields")] public string[] Fields;
        /// <summary> Configuration encoded in JSON </summary>
        [DataMember (Name = "config")] public string Config;
    
        /// Constructor for empty message.
        public UpdateModuleRequest()
        {
            Id = string.Empty;
            Fields = System.Array.Empty<string>();
            Config = string.Empty;
        }
        
        /// Explicit constructor.
        public UpdateModuleRequest(string Id, string[] Fields, string Config)
        {
            this.Id = Id;
            this.Fields = Fields;
            this.Config = Config;
        }
        
        /// Constructor with buffer.
        public UpdateModuleRequest(ref ReadBuffer b)
        {
            Id = b.DeserializeString();
            Fields = b.DeserializeStringArray();
            Config = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UpdateModuleRequest(ref b);
        
        public UpdateModuleRequest RosDeserialize(ref ReadBuffer b) => new UpdateModuleRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            b.SerializeArray(Fields);
            b.Serialize(Config);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Fields is null) throw new System.NullReferenceException(nameof(Fields));
            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i] is null) throw new System.NullReferenceException($"{nameof(Fields)}[{i}]");
            }
            if (Config is null) throw new System.NullReferenceException(nameof(Config));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.GetStringSize(Id);
                size += BuiltIns.GetArraySize(Fields);
                size += BuiltIns.GetStringSize(Config);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateModuleResponse : IResponse, IDeserializable<UpdateModuleResponse>
    {
        /// <summary> Whether the retrieval succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> An error message if success is false </summary>
        [DataMember (Name = "message")] public string Message;
    
        /// Constructor for empty message.
        public UpdateModuleResponse()
        {
            Message = string.Empty;
        }
        
        /// Explicit constructor.
        public UpdateModuleResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        public UpdateModuleResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UpdateModuleResponse(ref b);
        
        public UpdateModuleResponse RosDeserialize(ref ReadBuffer b) => new UpdateModuleResponse(ref b);
    
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
