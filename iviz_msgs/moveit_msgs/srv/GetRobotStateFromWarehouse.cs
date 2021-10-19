using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/GetRobotStateFromWarehouse")]
    public sealed class GetRobotStateFromWarehouse : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetRobotStateFromWarehouseRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetRobotStateFromWarehouseResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetRobotStateFromWarehouse()
        {
            Request = new GetRobotStateFromWarehouseRequest();
            Response = new GetRobotStateFromWarehouseResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetRobotStateFromWarehouse(GetRobotStateFromWarehouseRequest request)
        {
            Request = request;
            Response = new GetRobotStateFromWarehouseResponse();
        }
        
        IService IService.Create() => new GetRobotStateFromWarehouse();
        
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/GetRobotStateFromWarehouse";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "1e46b17b23bf82bdaafbbc818f6d91b0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetRobotStateFromWarehouseRequest : IRequest<GetRobotStateFromWarehouse, GetRobotStateFromWarehouseResponse>, IDeserializable<GetRobotStateFromWarehouseRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "robot")] public string Robot;
    
        /// <summary> Constructor for empty message. </summary>
        public GetRobotStateFromWarehouseRequest()
        {
            Name = string.Empty;
            Robot = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetRobotStateFromWarehouseRequest(string Name, string Robot)
        {
            this.Name = Name;
            this.Robot = Robot;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetRobotStateFromWarehouseRequest(ref Buffer b)
        {
            Name = b.DeserializeString();
            Robot = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetRobotStateFromWarehouseRequest(ref b);
        }
        
        GetRobotStateFromWarehouseRequest IDeserializable<GetRobotStateFromWarehouseRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetRobotStateFromWarehouseRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Robot);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Robot is null) throw new System.NullReferenceException(nameof(Robot));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Robot);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetRobotStateFromWarehouseResponse : IResponse, IDeserializable<GetRobotStateFromWarehouseResponse>
    {
        [DataMember (Name = "state")] public MoveitMsgs.RobotState State;
    
        /// <summary> Constructor for empty message. </summary>
        public GetRobotStateFromWarehouseResponse()
        {
            State = new MoveitMsgs.RobotState();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetRobotStateFromWarehouseResponse(MoveitMsgs.RobotState State)
        {
            this.State = State;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetRobotStateFromWarehouseResponse(ref Buffer b)
        {
            State = new MoveitMsgs.RobotState(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetRobotStateFromWarehouseResponse(ref b);
        }
        
        GetRobotStateFromWarehouseResponse IDeserializable<GetRobotStateFromWarehouseResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetRobotStateFromWarehouseResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            State.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (State is null) throw new System.NullReferenceException(nameof(State));
            State.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += State.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
