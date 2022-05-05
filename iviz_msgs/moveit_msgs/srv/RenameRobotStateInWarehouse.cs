using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "moveit_msgs/RenameRobotStateInWarehouse";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "073dc05c3fd313b947cea483c25c46b0";
        
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
            OldName = "";
            NewName = "";
            Robot = "";
        }
        
        /// Explicit constructor.
        public RenameRobotStateInWarehouseRequest(string OldName, string NewName, string Robot)
        {
            this.OldName = OldName;
            this.NewName = NewName;
            this.Robot = Robot;
        }
        
        /// Constructor with buffer.
        public RenameRobotStateInWarehouseRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out OldName);
            b.DeserializeString(out NewName);
            b.DeserializeString(out Robot);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new RenameRobotStateInWarehouseRequest(ref b);
        
        public RenameRobotStateInWarehouseRequest RosDeserialize(ref ReadBuffer b) => new RenameRobotStateInWarehouseRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(OldName);
            b.Serialize(NewName);
            b.Serialize(Robot);
        }
        
        public void RosValidate()
        {
            if (OldName is null) BuiltIns.ThrowNullReference();
            if (NewName is null) BuiltIns.ThrowNullReference();
            if (Robot is null) BuiltIns.ThrowNullReference();
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
        public RenameRobotStateInWarehouseResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public RenameRobotStateInWarehouseResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static RenameRobotStateInWarehouseResponse? singleton;
        public static RenameRobotStateInWarehouseResponse Singleton => singleton ??= new RenameRobotStateInWarehouseResponse();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
