using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class ListRobotStatesInWarehouse : IService
    {
        /// Request message.
        [DataMember] public ListRobotStatesInWarehouseRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ListRobotStatesInWarehouseResponse Response { get; set; }
        
        /// Empty constructor.
        public ListRobotStatesInWarehouse()
        {
            Request = new ListRobotStatesInWarehouseRequest();
            Response = new ListRobotStatesInWarehouseResponse();
        }
        
        /// Setter constructor.
        public ListRobotStatesInWarehouse(ListRobotStatesInWarehouseRequest request)
        {
            Request = request;
            Response = new ListRobotStatesInWarehouseResponse();
        }
        
        IService IService.Create() => new ListRobotStatesInWarehouse();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ListRobotStatesInWarehouseRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ListRobotStatesInWarehouseResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/ListRobotStatesInWarehouse";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "dc02fa289e68670e9d392985a0235ee6";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ListRobotStatesInWarehouseRequest : IRequest<ListRobotStatesInWarehouse, ListRobotStatesInWarehouseResponse>, IDeserializable<ListRobotStatesInWarehouseRequest>
    {
        [DataMember (Name = "regex")] public string Regex;
        [DataMember (Name = "robot")] public string Robot;
    
        /// Constructor for empty message.
        public ListRobotStatesInWarehouseRequest()
        {
            Regex = string.Empty;
            Robot = string.Empty;
        }
        
        /// Explicit constructor.
        public ListRobotStatesInWarehouseRequest(string Regex, string Robot)
        {
            this.Regex = Regex;
            this.Robot = Robot;
        }
        
        /// Constructor with buffer.
        internal ListRobotStatesInWarehouseRequest(ref Buffer b)
        {
            Regex = b.DeserializeString();
            Robot = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ListRobotStatesInWarehouseRequest(ref b);
        
        ListRobotStatesInWarehouseRequest IDeserializable<ListRobotStatesInWarehouseRequest>.RosDeserialize(ref Buffer b) => new ListRobotStatesInWarehouseRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Regex);
            b.Serialize(Robot);
        }
        
        public void RosValidate()
        {
            if (Regex is null) throw new System.NullReferenceException(nameof(Regex));
            if (Robot is null) throw new System.NullReferenceException(nameof(Robot));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Regex) + BuiltIns.GetStringSize(Robot);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ListRobotStatesInWarehouseResponse : IResponse, IDeserializable<ListRobotStatesInWarehouseResponse>
    {
        [DataMember (Name = "states")] public string[] States;
    
        /// Constructor for empty message.
        public ListRobotStatesInWarehouseResponse()
        {
            States = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public ListRobotStatesInWarehouseResponse(string[] States)
        {
            this.States = States;
        }
        
        /// Constructor with buffer.
        internal ListRobotStatesInWarehouseResponse(ref Buffer b)
        {
            States = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ListRobotStatesInWarehouseResponse(ref b);
        
        ListRobotStatesInWarehouseResponse IDeserializable<ListRobotStatesInWarehouseResponse>.RosDeserialize(ref Buffer b) => new ListRobotStatesInWarehouseResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(States);
        }
        
        public void RosValidate()
        {
            if (States is null) throw new System.NullReferenceException(nameof(States));
            for (int i = 0; i < States.Length; i++)
            {
                if (States[i] is null) throw new System.NullReferenceException($"{nameof(States)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(States);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
