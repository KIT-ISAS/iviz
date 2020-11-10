using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/UpdateModule")]
    public sealed class UpdateModule : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public UpdateModuleRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public UpdateModuleResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public UpdateModule()
        {
            Request = new UpdateModuleRequest();
            Response = new UpdateModuleResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/UpdateModule";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "9b8bbde938619f17558ceabafe5f3a13";
    }

    public sealed class UpdateModuleRequest : IRequest, IDeserializable<UpdateModuleRequest>
    {
        // Updates a module
        [DataMember (Name = "id")] public string Id { get; set; } // Id of the module
        [DataMember (Name = "fields")] public string[] Fields { get; set; } // The fields to be updated
        [DataMember (Name = "config")] public string Config { get; set; } // Configuration encoded in JSON
    
        /// <summary> Constructor for empty message. </summary>
        public UpdateModuleRequest()
        {
            Id = "";
            Fields = System.Array.Empty<string>();
            Config = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public UpdateModuleRequest(string Id, string[] Fields, string Config)
        {
            this.Id = Id;
            this.Fields = Fields;
            this.Config = Config;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UpdateModuleRequest(ref Buffer b)
        {
            Id = b.DeserializeString();
            Fields = b.DeserializeStringArray();
            Config = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UpdateModuleRequest(ref b);
        }
        
        UpdateModuleRequest IDeserializable<UpdateModuleRequest>.RosDeserialize(ref Buffer b)
        {
            return new UpdateModuleRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Id);
            b.SerializeArray(Fields, 0);
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
                size += BuiltIns.UTF8.GetByteCount(Id);
                size += 4 * Fields.Length;
                foreach (string s in Fields)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += BuiltIns.UTF8.GetByteCount(Config);
                return size;
            }
        }
    }

    public sealed class UpdateModuleResponse : IResponse, IDeserializable<UpdateModuleResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; } // Whether the retrieval succeeded
        [DataMember (Name = "message")] public string Message { get; set; } // An error message if success is false
    
        /// <summary> Constructor for empty message. </summary>
        public UpdateModuleResponse()
        {
            Message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public UpdateModuleResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UpdateModuleResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UpdateModuleResponse(ref b);
        }
        
        UpdateModuleResponse IDeserializable<UpdateModuleResponse>.RosDeserialize(ref Buffer b)
        {
            return new UpdateModuleResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(Message);
                return size;
            }
        }
    }
}
