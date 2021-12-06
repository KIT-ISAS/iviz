using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
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
        
        IService IService.Create() => new UpdateRobot();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/UpdateRobot";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "7e6f65767dc26bdb9812dffefc4efaa1";
        
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
            Id = string.Empty;
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
        internal UpdateRobotRequest(ref ReadBuffer b)
        {
            Operation = b.Deserialize<int>();
            Id = b.DeserializeString();
            Configuration = new IvizMsgs.RobotConfiguration(ref b);
            ValidFields = b.DeserializeStringArray();
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
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Configuration is null) throw new System.NullReferenceException(nameof(Configuration));
            Configuration.RosValidate();
            if (ValidFields is null) throw new System.NullReferenceException(nameof(ValidFields));
            for (int i = 0; i < ValidFields.Length; i++)
            {
                if (ValidFields[i] is null) throw new System.NullReferenceException($"{nameof(ValidFields)}[{i}]");
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
            Message = string.Empty;
        }
        
        /// Explicit constructor.
        public UpdateRobotResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        internal UpdateRobotResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
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
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength => 5 + BuiltIns.GetStringSize(Message);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
