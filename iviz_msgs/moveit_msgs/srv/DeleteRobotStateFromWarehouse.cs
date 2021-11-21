using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/DeleteRobotStateFromWarehouse")]
    public sealed class DeleteRobotStateFromWarehouse : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public DeleteRobotStateFromWarehouseRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public DeleteRobotStateFromWarehouseResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public DeleteRobotStateFromWarehouse()
        {
            Request = new DeleteRobotStateFromWarehouseRequest();
            Response = DeleteRobotStateFromWarehouseResponse.Singleton;
        }
        
        /// <summary> Setter constructor. </summary>
        public DeleteRobotStateFromWarehouse(DeleteRobotStateFromWarehouseRequest request)
        {
            Request = request;
            Response = DeleteRobotStateFromWarehouseResponse.Singleton;
        }
        
        IService IService.Create() => new DeleteRobotStateFromWarehouse();
        
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/DeleteRobotStateFromWarehouse";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "dab44354403f811c40b84964e068219c";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class DeleteRobotStateFromWarehouseRequest : IRequest<DeleteRobotStateFromWarehouse, DeleteRobotStateFromWarehouseResponse>, IDeserializable<DeleteRobotStateFromWarehouseRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "robot")] public string Robot;
    
        /// <summary> Constructor for empty message. </summary>
        public DeleteRobotStateFromWarehouseRequest()
        {
            Name = string.Empty;
            Robot = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public DeleteRobotStateFromWarehouseRequest(string Name, string Robot)
        {
            this.Name = Name;
            this.Robot = Robot;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal DeleteRobotStateFromWarehouseRequest(ref Buffer b)
        {
            Name = b.DeserializeString();
            Robot = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DeleteRobotStateFromWarehouseRequest(ref b);
        }
        
        DeleteRobotStateFromWarehouseRequest IDeserializable<DeleteRobotStateFromWarehouseRequest>.RosDeserialize(ref Buffer b)
        {
            return new DeleteRobotStateFromWarehouseRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
    
        /// <summary> Constructor for empty message. </summary>
        public DeleteRobotStateFromWarehouseResponse()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal DeleteRobotStateFromWarehouseResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        DeleteRobotStateFromWarehouseResponse IDeserializable<DeleteRobotStateFromWarehouseResponse>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly DeleteRobotStateFromWarehouseResponse Singleton = new DeleteRobotStateFromWarehouseResponse();
    
        public void RosSerialize(ref Buffer b)
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
