using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class UpdateRobot : IService
    {
        /// Request message.
        [DataMember] public UpdateRobotRequest Request;
        
        /// Response message.
        [DataMember] public UpdateRobotResponse Response;
        
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
    
        public UpdateRobotRequest()
        {
            Id = "";
            Configuration = new IvizMsgs.RobotConfiguration();
            ValidFields = EmptyArray<string>.Value;
        }
        
        public UpdateRobotRequest(int Operation, string Id, IvizMsgs.RobotConfiguration Configuration, string[] ValidFields)
        {
            this.Operation = Operation;
            this.Id = Id;
            this.Configuration = Configuration;
            this.ValidFields = ValidFields;
        }
        
        public UpdateRobotRequest(ref ReadBuffer b)
        {
            b.Deserialize(out Operation);
            Id = b.DeserializeString();
            Configuration = new IvizMsgs.RobotConfiguration(ref b);
            ValidFields = b.DeserializeStringArray();
        }
        
        public UpdateRobotRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Operation);
            Id = b.DeserializeString();
            Configuration = new IvizMsgs.RobotConfiguration(ref b);
            b.Align4();
            ValidFields = b.DeserializeStringArray();
        }
        
        public UpdateRobotRequest RosDeserialize(ref ReadBuffer b) => new UpdateRobotRequest(ref b);
        
        public UpdateRobotRequest RosDeserialize(ref ReadBuffer2 b) => new UpdateRobotRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Operation);
            b.Serialize(Id);
            Configuration.RosSerialize(ref b);
            b.Serialize(ValidFields.Length);
            b.SerializeArray(ValidFields);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Operation);
            b.Serialize(Id);
            Configuration.RosSerialize(ref b);
            b.Align4();
            b.Serialize(ValidFields.Length);
            b.SerializeArray(ValidFields);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference(nameof(Id));
            if (Configuration is null) BuiltIns.ThrowNullReference(nameof(Configuration));
            Configuration.RosValidate();
            if (ValidFields is null) BuiltIns.ThrowNullReference(nameof(ValidFields));
            for (int i = 0; i < ValidFields.Length; i++)
            {
                if (ValidFields[i] is null) BuiltIns.ThrowNullReference(nameof(ValidFields), i);
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 12;
                size += WriteBuffer.GetStringSize(Id);
                size += Configuration.RosMessageLength;
                size += WriteBuffer.GetArraySize(ValidFields);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Operation
            size = WriteBuffer2.AddLength(size, Id);
            size = Configuration.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, ValidFields);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateRobotResponse : IResponse, IDeserializable<UpdateRobotResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
    
        public UpdateRobotResponse()
        {
            Message = "";
        }
        
        public UpdateRobotResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        public UpdateRobotResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
        }
        
        public UpdateRobotResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            Message = b.DeserializeString();
        }
        
        public UpdateRobotResponse RosDeserialize(ref ReadBuffer b) => new UpdateRobotResponse(ref b);
        
        public UpdateRobotResponse RosDeserialize(ref ReadBuffer2 b) => new UpdateRobotResponse(ref b);
    
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
            if (Message is null) BuiltIns.ThrowNullReference(nameof(Message));
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
