using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/UpdateRobot")]
    public sealed class UpdateRobot : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public UpdateRobotRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public UpdateRobotResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public UpdateRobot()
        {
            Request = new UpdateRobotRequest();
            Response = new UpdateRobotResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/UpdateRobot";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "7e6f65767dc26bdb9812dffefc4efaa1";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateRobotRequest : IRequest<UpdateRobot, UpdateRobotResponse>, IDeserializable<UpdateRobotRequest>
    {
        [DataMember (Name = "operation")] public int Operation { get; set; }
        [DataMember (Name = "id")] public string Id { get; set; }
        [DataMember (Name = "configuration")] public IvizMsgs.RobotConfiguration Configuration { get; set; }
        [DataMember (Name = "valid_fields")] public string[] ValidFields { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public UpdateRobotRequest()
        {
            Id = string.Empty;
            Configuration = new IvizMsgs.RobotConfiguration();
            ValidFields = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public UpdateRobotRequest(int Operation, string Id, IvizMsgs.RobotConfiguration Configuration, string[] ValidFields)
        {
            this.Operation = Operation;
            this.Id = Id;
            this.Configuration = Configuration;
            this.ValidFields = ValidFields;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UpdateRobotRequest(ref Buffer b)
        {
            Operation = b.Deserialize<int>();
            Id = b.DeserializeString();
            Configuration = new IvizMsgs.RobotConfiguration(ref b);
            ValidFields = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UpdateRobotRequest(ref b);
        }
        
        UpdateRobotRequest IDeserializable<UpdateRobotRequest>.RosDeserialize(ref Buffer b)
        {
            return new UpdateRobotRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Operation);
            b.Serialize(Id);
            Configuration.RosSerialize(ref b);
            b.SerializeArray(ValidFields, 0);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Id);
                size += Configuration.RosMessageLength;
                size += 4 * ValidFields.Length;
                foreach (string s in ValidFields)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateRobotResponse : IResponse, IDeserializable<UpdateRobotResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
        [DataMember (Name = "message")] public string Message { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public UpdateRobotResponse()
        {
            Message = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public UpdateRobotResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UpdateRobotResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UpdateRobotResponse(ref b);
        }
        
        UpdateRobotResponse IDeserializable<UpdateRobotResponse>.RosDeserialize(ref Buffer b)
        {
            return new UpdateRobotResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void Dispose()
        {
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
    
        public override string ToString() => Extensions.ToString(this);
    }
}
