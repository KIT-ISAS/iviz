using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class DeleteRobotStateFromWarehouse : IService
    {
        /// Request message.
        [DataMember] public DeleteRobotStateFromWarehouseRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public DeleteRobotStateFromWarehouseResponse Response { get; set; }
        
        /// Empty constructor.
        public DeleteRobotStateFromWarehouse()
        {
            Request = new DeleteRobotStateFromWarehouseRequest();
            Response = DeleteRobotStateFromWarehouseResponse.Singleton;
        }
        
        /// Setter constructor.
        public DeleteRobotStateFromWarehouse(DeleteRobotStateFromWarehouseRequest request)
        {
            Request = request;
            Response = DeleteRobotStateFromWarehouseResponse.Singleton;
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (DeleteRobotStateFromWarehouseRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (DeleteRobotStateFromWarehouseResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/DeleteRobotStateFromWarehouse";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "dab44354403f811c40b84964e068219c";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class DeleteRobotStateFromWarehouseRequest : IRequest<DeleteRobotStateFromWarehouse, DeleteRobotStateFromWarehouseResponse>, IDeserializable<DeleteRobotStateFromWarehouseRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "robot")] public string Robot;
    
        /// Constructor for empty message.
        public DeleteRobotStateFromWarehouseRequest()
        {
            Name = "";
            Robot = "";
        }
        
        /// Explicit constructor.
        public DeleteRobotStateFromWarehouseRequest(string Name, string Robot)
        {
            this.Name = Name;
            this.Robot = Robot;
        }
        
        /// Constructor with buffer.
        public DeleteRobotStateFromWarehouseRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            Robot = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new DeleteRobotStateFromWarehouseRequest(ref b);
        
        public DeleteRobotStateFromWarehouseRequest RosDeserialize(ref ReadBuffer b) => new DeleteRobotStateFromWarehouseRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Robot);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Robot is null) throw new System.NullReferenceException(nameof(Robot));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name) + BuiltIns.GetStringSize(Robot);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class DeleteRobotStateFromWarehouseResponse : IResponse, IDeserializable<DeleteRobotStateFromWarehouseResponse>
    {
    
        /// Constructor for empty message.
        public DeleteRobotStateFromWarehouseResponse()
        {
        }
        
        /// Constructor with buffer.
        public DeleteRobotStateFromWarehouseResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public DeleteRobotStateFromWarehouseResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly DeleteRobotStateFromWarehouseResponse Singleton = new DeleteRobotStateFromWarehouseResponse();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
