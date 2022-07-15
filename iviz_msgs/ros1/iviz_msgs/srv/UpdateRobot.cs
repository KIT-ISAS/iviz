using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class UpdateRobot : IService
    {
        /// Request message.
        [DataMember] public UpdateRobotRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public UpdateRobotResponse Response { get; set; }
        
        /// Empty constructor.
        public UpdateRobot()
        {
            Request = new UpdateRobotRequest();
            Response = new UpdateRobotResponse();
        }
        
        /// Setter constructor.
        public UpdateRobot(UpdateRobotRequest request)
        {
            Request = request;
            Response = new UpdateRobotResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (UpdateRobotRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (UpdateRobotResponse)value;
        }
        
        public const string ServiceType = "iviz_msgs/UpdateRobot";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "7e6f65767dc26bdb9812dffefc4efaa1";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateRobotRequest : IRequest<UpdateRobot, UpdateRobotResponse>, IDeserializable<UpdateRobotRequest>
    {
        [DataMember (Name = "operation")] public int Operation;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "configuration")] public IvizMsgs.RobotConfiguration Configuration;
        [DataMember (Name = "valid_fields")] public string[] ValidFields;
    
        /// Constructor for empty message.
        public UpdateRobotRequest()
        {
            Id = "";
            Configuration = new IvizMsgs.RobotConfiguration();
            ValidFields = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public UpdateRobotRequest(int Operation, string Id, IvizMsgs.RobotConfiguration Configuration, string[] ValidFields)
        {
            this.Operation = Operation;
            this.Id = Id;
            this.Configuration = Configuration;
            this.ValidFields = ValidFields;
        }
        
        /// Constructor with buffer.
        public UpdateRobotRequest(ref ReadBuffer b)
        {
            b.Deserialize(out Operation);
            b.DeserializeString(out Id);
            Configuration = new IvizMsgs.RobotConfiguration(ref b);
            b.DeserializeStringArray(out ValidFields);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UpdateRobotRequest(ref b);
        
        public UpdateRobotRequest RosDeserialize(ref ReadBuffer b) => new UpdateRobotRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Operation);
            b.Serialize(Id);
            Configuration.RosSerialize(ref b);
            b.SerializeArray(ValidFields);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Configuration is null) BuiltIns.ThrowNullReference();
            Configuration.RosValidate();
            if (ValidFields is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < ValidFields.Length; i++)
            {
                if (ValidFields[i] is null) BuiltIns.ThrowNullReference(nameof(ValidFields), i);
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.GetStringSize(Id);
                size += Configuration.RosMessageLength;
                size += BuiltIns.GetArraySize(ValidFields);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateRobotResponse : IResponse, IDeserializable<UpdateRobotResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
    
        /// Constructor for empty message.
        public UpdateRobotResponse()
        {
            Message = "";
        }
        
        /// Explicit constructor.
        public UpdateRobotResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        public UpdateRobotResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UpdateRobotResponse(ref b);
        
        public UpdateRobotResponse RosDeserialize(ref ReadBuffer b) => new UpdateRobotResponse(ref b);
    
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
