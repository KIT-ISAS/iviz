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
            Fields = System.Array.Empty<string>();
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
            b.DeserializeString(out Id);
            b.DeserializeStringArray(out Fields);
            b.DeserializeString(out Config);
        }
        
        public UpdateModuleRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Id);
            b.Align4();
            b.DeserializeStringArray(out Fields);
            b.Align4();
            b.DeserializeString(out Config);
        }
        
        public UpdateModuleRequest RosDeserialize(ref ReadBuffer b) => new UpdateModuleRequest(ref b);
        
        public UpdateModuleRequest RosDeserialize(ref ReadBuffer2 b) => new UpdateModuleRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            b.SerializeArray(Fields);
            b.Serialize(Config);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Id);
            b.SerializeArray(Fields);
            b.Serialize(Config);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Fields is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i] is null) BuiltIns.ThrowNullReference(nameof(Fields), i);
            }
            if (Config is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetArraySize(Fields);
                size += WriteBuffer.GetStringSize(Config);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Id);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Fields);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Config);
            return c;
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
            b.DeserializeString(out Message);
        }
        
        public UpdateModuleResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            b.DeserializeString(out Message);
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
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Message is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + WriteBuffer.GetStringSize(Message);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Success
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Message);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
