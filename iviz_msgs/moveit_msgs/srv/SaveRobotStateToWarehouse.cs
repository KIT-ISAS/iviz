using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class SaveRobotStateToWarehouse : IService
    {
        /// Request message.
        [DataMember] public SaveRobotStateToWarehouseRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SaveRobotStateToWarehouseResponse Response { get; set; }
        
        /// Empty constructor.
        public SaveRobotStateToWarehouse()
        {
            Request = new SaveRobotStateToWarehouseRequest();
            Response = new SaveRobotStateToWarehouseResponse();
        }
        
        /// Setter constructor.
        public SaveRobotStateToWarehouse(SaveRobotStateToWarehouseRequest request)
        {
            Request = request;
            Response = new SaveRobotStateToWarehouseResponse();
        }
        
        IService IService.Create() => new SaveRobotStateToWarehouse();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SaveRobotStateToWarehouseRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SaveRobotStateToWarehouseResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/SaveRobotStateToWarehouse";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "555cb479e433361a8f0a29f1cd7f3ad2";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SaveRobotStateToWarehouseRequest : IRequest<SaveRobotStateToWarehouse, SaveRobotStateToWarehouseResponse>, IDeserializable<SaveRobotStateToWarehouseRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "robot")] public string Robot;
        [DataMember (Name = "state")] public MoveitMsgs.RobotState State;
    
        /// Constructor for empty message.
        public SaveRobotStateToWarehouseRequest()
        {
            Name = string.Empty;
            Robot = string.Empty;
            State = new MoveitMsgs.RobotState();
        }
        
        /// Explicit constructor.
        public SaveRobotStateToWarehouseRequest(string Name, string Robot, MoveitMsgs.RobotState State)
        {
            this.Name = Name;
            this.Robot = Robot;
            this.State = State;
        }
        
        /// Constructor with buffer.
        internal SaveRobotStateToWarehouseRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            Robot = b.DeserializeString();
            State = new MoveitMsgs.RobotState(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SaveRobotStateToWarehouseRequest(ref b);
        
        public SaveRobotStateToWarehouseRequest RosDeserialize(ref ReadBuffer b) => new SaveRobotStateToWarehouseRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Robot);
            State.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Robot is null) throw new System.NullReferenceException(nameof(Robot));
            if (State is null) throw new System.NullReferenceException(nameof(State));
            State.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetStringSize(Robot);
                size += State.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SaveRobotStateToWarehouseResponse : IResponse, IDeserializable<SaveRobotStateToWarehouseResponse>
    {
        [DataMember (Name = "success")] public bool Success;
    
        /// Constructor for empty message.
        public SaveRobotStateToWarehouseResponse()
        {
        }
        
        /// Explicit constructor.
        public SaveRobotStateToWarehouseResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// Constructor with buffer.
        internal SaveRobotStateToWarehouseResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SaveRobotStateToWarehouseResponse(ref b);
        
        public SaveRobotStateToWarehouseResponse RosDeserialize(ref ReadBuffer b) => new SaveRobotStateToWarehouseResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
