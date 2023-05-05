using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class UpdateModule : IService
    {
        /// Request message.
        [DataMember] public UpdateModuleRequest Request;
        
        /// Response message.
        [DataMember] public UpdateModuleResponse Response;
        
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
        
        public const string ServiceType = "iviz_msgs/UpdateModule";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "9b8bbde938619f17558ceabafe5f3a13";
        
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
    
        public UpdateModuleRequest()
        {
            Id = "";
            Fields = EmptyArray<string>.Value;
            Config = "";
        }
        
        public UpdateModuleRequest(string Id, string[] Fields, string Config)
        {
            this.Id = Id;
            this.Fields = Fields;
            this.Config = Config;
        }
        
        public UpdateModuleRequest(ref ReadBuffer b)
        {
            Id = b.DeserializeString();
            Fields = b.DeserializeStringArray();
            Config = b.DeserializeString();
        }
        
        public UpdateModuleRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Id = b.DeserializeString();
            b.Align4();
            Fields = b.DeserializeStringArray();
            b.Align4();
            Config = b.DeserializeString();
        }
        
        public UpdateModuleRequest RosDeserialize(ref ReadBuffer b) => new UpdateModuleRequest(ref b);
        
        public UpdateModuleRequest RosDeserialize(ref ReadBuffer2 b) => new UpdateModuleRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            b.Serialize(Fields.Length);
            b.SerializeArray(Fields);
            b.Serialize(Config);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Id);
            b.Align4();
            b.Serialize(Fields.Length);
            b.SerializeArray(Fields);
            b.Align4();
            b.Serialize(Config);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Id, nameof(Id));
            BuiltIns.ThrowIfNull(Fields, nameof(Fields));
            BuiltIns.ThrowIfNull(Config, nameof(Config));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 12;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetArraySize(Fields);
                size += WriteBuffer.GetStringSize(Config);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Fields);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Config);
            return size;
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
    
        public UpdateModuleResponse()
        {
            Message = "";
        }
        
        public UpdateModuleResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        public UpdateModuleResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
        }
        
        public UpdateModuleResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            Message = b.DeserializeString();
        }
        
        public UpdateModuleResponse RosDeserialize(ref ReadBuffer b) => new UpdateModuleResponse(ref b);
        
        public UpdateModuleResponse RosDeserialize(ref ReadBuffer2 b) => new UpdateModuleResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Align4();
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Message, nameof(Message));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 5;
                size += WriteBuffer.GetStringSize(Message);
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
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
