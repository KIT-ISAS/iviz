using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class GetRobotStateFromWarehouse : IService
    {
        /// Request message.
        [DataMember] public GetRobotStateFromWarehouseRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetRobotStateFromWarehouseResponse Response { get; set; }
        
        /// Empty constructor.
        public GetRobotStateFromWarehouse()
        {
            Request = new GetRobotStateFromWarehouseRequest();
            Response = new GetRobotStateFromWarehouseResponse();
        }
        
        /// Setter constructor.
        public GetRobotStateFromWarehouse(GetRobotStateFromWarehouseRequest request)
        {
            Request = request;
            Response = new GetRobotStateFromWarehouseResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetRobotStateFromWarehouseRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetRobotStateFromWarehouseResponse)value;
        }
        
        public const string ServiceType = "moveit_msgs/GetRobotStateFromWarehouse";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "1e46b17b23bf82bdaafbbc818f6d91b0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetRobotStateFromWarehouseRequest : IRequest<GetRobotStateFromWarehouse, GetRobotStateFromWarehouseResponse>, IDeserializable<GetRobotStateFromWarehouseRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "robot")] public string Robot;
    
        /// Constructor for empty message.
        public GetRobotStateFromWarehouseRequest()
        {
            Name = "";
            Robot = "";
        }
        
        /// Explicit constructor.
        public GetRobotStateFromWarehouseRequest(string Name, string Robot)
        {
            this.Name = Name;
            this.Robot = Robot;
        }
        
        /// Constructor with buffer.
        public GetRobotStateFromWarehouseRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Robot);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetRobotStateFromWarehouseRequest(ref b);
        
        public GetRobotStateFromWarehouseRequest RosDeserialize(ref ReadBuffer b) => new GetRobotStateFromWarehouseRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Robot);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Robot is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name) + BuiltIns.GetStringSize(Robot);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetRobotStateFromWarehouseResponse : IResponse, IDeserializable<GetRobotStateFromWarehouseResponse>
    {
        [DataMember (Name = "state")] public MoveitMsgs.RobotState State;
    
        /// Constructor for empty message.
        public GetRobotStateFromWarehouseResponse()
        {
            State = new MoveitMsgs.RobotState();
        }
        
        /// Explicit constructor.
        public GetRobotStateFromWarehouseResponse(MoveitMsgs.RobotState State)
        {
            this.State = State;
        }
        
        /// Constructor with buffer.
        public GetRobotStateFromWarehouseResponse(ref ReadBuffer b)
        {
            State = new MoveitMsgs.RobotState(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetRobotStateFromWarehouseResponse(ref b);
        
        public GetRobotStateFromWarehouseResponse RosDeserialize(ref ReadBuffer b) => new GetRobotStateFromWarehouseResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            State.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (State is null) BuiltIns.ThrowNullReference();
            State.RosValidate();
        }
    
        public int RosMessageLength => 0 + State.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
