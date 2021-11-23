using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class RenameRobotStateInWarehouse : IService
    {
        /// Request message.
        [DataMember] public RenameRobotStateInWarehouseRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public RenameRobotStateInWarehouseResponse Response { get; set; }
        
        /// Empty constructor.
        public RenameRobotStateInWarehouse()
        {
            Request = new RenameRobotStateInWarehouseRequest();
            Response = RenameRobotStateInWarehouseResponse.Singleton;
        }
        
        /// Setter constructor.
        public RenameRobotStateInWarehouse(RenameRobotStateInWarehouseRequest request)
        {
            Request = request;
            Response = RenameRobotStateInWarehouseResponse.Singleton;
        }
        
        IService IService.Create() => new RenameRobotStateInWarehouse();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (RenameRobotStateInWarehouseRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (RenameRobotStateInWarehouseResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/RenameRobotStateInWarehouse";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "073dc05c3fd313b947cea483c25c46b0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class RenameRobotStateInWarehouseRequest : IRequest<RenameRobotStateInWarehouse, RenameRobotStateInWarehouseResponse>, IDeserializable<RenameRobotStateInWarehouseRequest>
    {
        [DataMember (Name = "old_name")] public string OldName;
        [DataMember (Name = "new_name")] public string NewName;
        [DataMember (Name = "robot")] public string Robot;
    
        /// Constructor for empty message.
        public RenameRobotStateInWarehouseRequest()
        {
            OldName = string.Empty;
            NewName = string.Empty;
            Robot = string.Empty;
        }
        
        /// Explicit constructor.
        public RenameRobotStateInWarehouseRequest(string OldName, string NewName, string Robot)
        {
            this.OldName = OldName;
            this.NewName = NewName;
            this.Robot = Robot;
        }
        
        /// Constructor with buffer.
        internal RenameRobotStateInWarehouseRequest(ref Buffer b)
        {
            OldName = b.DeserializeString();
            NewName = b.DeserializeString();
            Robot = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new RenameRobotStateInWarehouseRequest(ref b);
        
        RenameRobotStateInWarehouseRequest IDeserializable<RenameRobotStateInWarehouseRequest>.RosDeserialize(ref Buffer b) => new RenameRobotStateInWarehouseRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(OldName);
            b.Serialize(NewName);
            b.Serialize(Robot);
        }
        
        public void RosValidate()
        {
            if (OldName is null) throw new System.NullReferenceException(nameof(OldName));
            if (NewName is null) throw new System.NullReferenceException(nameof(NewName));
            if (Robot is null) throw new System.NullReferenceException(nameof(Robot));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.GetStringSize(OldName);
                size += BuiltIns.GetStringSize(NewName);
                size += BuiltIns.GetStringSize(Robot);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class RenameRobotStateInWarehouseResponse : IResponse, IDeserializable<RenameRobotStateInWarehouseResponse>
    {
    
        /// Constructor for empty message.
        public RenameRobotStateInWarehouseResponse()
        {
        }
        
        /// Constructor with buffer.
        internal RenameRobotStateInWarehouseResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        RenameRobotStateInWarehouseResponse IDeserializable<RenameRobotStateInWarehouseResponse>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly RenameRobotStateInWarehouseResponse Singleton = new RenameRobotStateInWarehouseResponse();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
