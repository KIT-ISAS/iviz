using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/SaveRobotStateToWarehouse")]
    public sealed class SaveRobotStateToWarehouse : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SaveRobotStateToWarehouseRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SaveRobotStateToWarehouseResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SaveRobotStateToWarehouse()
        {
            Request = new SaveRobotStateToWarehouseRequest();
            Response = new SaveRobotStateToWarehouseResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/SaveRobotStateToWarehouse";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "555cb479e433361a8f0a29f1cd7f3ad2";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SaveRobotStateToWarehouseRequest : IRequest<SaveRobotStateToWarehouse, SaveRobotStateToWarehouseResponse>, IDeserializable<SaveRobotStateToWarehouseRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "robot")] public string Robot;
        [DataMember (Name = "state")] public MoveitMsgs.RobotState State;
    
        /// <summary> Constructor for empty message. </summary>
        public SaveRobotStateToWarehouseRequest()
        {
            Name = string.Empty;
            Robot = string.Empty;
            State = new MoveitMsgs.RobotState();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SaveRobotStateToWarehouseRequest(string Name, string Robot, MoveitMsgs.RobotState State)
        {
            this.Name = Name;
            this.Robot = Robot;
            this.State = State;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SaveRobotStateToWarehouseRequest(ref Buffer b)
        {
            Name = b.DeserializeString();
            Robot = b.DeserializeString();
            State = new MoveitMsgs.RobotState(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SaveRobotStateToWarehouseRequest(ref b);
        }
        
        SaveRobotStateToWarehouseRequest IDeserializable<SaveRobotStateToWarehouseRequest>.RosDeserialize(ref Buffer b)
        {
            return new SaveRobotStateToWarehouseRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
    
        /// <summary> Constructor for empty message. </summary>
        public SaveRobotStateToWarehouseResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public SaveRobotStateToWarehouseResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SaveRobotStateToWarehouseResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SaveRobotStateToWarehouseResponse(ref b);
        }
        
        SaveRobotStateToWarehouseResponse IDeserializable<SaveRobotStateToWarehouseResponse>.RosDeserialize(ref Buffer b)
        {
            return new SaveRobotStateToWarehouseResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
