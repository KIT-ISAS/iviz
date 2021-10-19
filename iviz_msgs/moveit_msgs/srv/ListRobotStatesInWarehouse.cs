using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/ListRobotStatesInWarehouse")]
    public sealed class ListRobotStatesInWarehouse : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ListRobotStatesInWarehouseRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ListRobotStatesInWarehouseResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ListRobotStatesInWarehouse()
        {
            Request = new ListRobotStatesInWarehouseRequest();
            Response = new ListRobotStatesInWarehouseResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/ListRobotStatesInWarehouse";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "dc02fa289e68670e9d392985a0235ee6";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ListRobotStatesInWarehouseRequest : IRequest<ListRobotStatesInWarehouse, ListRobotStatesInWarehouseResponse>, IDeserializable<ListRobotStatesInWarehouseRequest>
    {
        [DataMember (Name = "regex")] public string Regex;
        [DataMember (Name = "robot")] public string Robot;
    
        /// <summary> Constructor for empty message. </summary>
        public ListRobotStatesInWarehouseRequest()
        {
            Regex = string.Empty;
            Robot = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ListRobotStatesInWarehouseRequest(string Regex, string Robot)
        {
            this.Regex = Regex;
            this.Robot = Robot;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ListRobotStatesInWarehouseRequest(ref Buffer b)
        {
            Regex = b.DeserializeString();
            Robot = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ListRobotStatesInWarehouseRequest(ref b);
        }
        
        ListRobotStatesInWarehouseRequest IDeserializable<ListRobotStatesInWarehouseRequest>.RosDeserialize(ref Buffer b)
        {
            return new ListRobotStatesInWarehouseRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Regex);
            b.Serialize(Robot);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Regex is null) throw new System.NullReferenceException(nameof(Regex));
            if (Robot is null) throw new System.NullReferenceException(nameof(Robot));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Regex);
                size += BuiltIns.UTF8.GetByteCount(Robot);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ListRobotStatesInWarehouseResponse : IResponse, IDeserializable<ListRobotStatesInWarehouseResponse>
    {
        [DataMember (Name = "states")] public string[] States;
    
        /// <summary> Constructor for empty message. </summary>
        public ListRobotStatesInWarehouseResponse()
        {
            States = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ListRobotStatesInWarehouseResponse(string[] States)
        {
            this.States = States;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ListRobotStatesInWarehouseResponse(ref Buffer b)
        {
            States = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ListRobotStatesInWarehouseResponse(ref b);
        }
        
        ListRobotStatesInWarehouseResponse IDeserializable<ListRobotStatesInWarehouseResponse>.RosDeserialize(ref Buffer b)
        {
            return new ListRobotStatesInWarehouseResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(States, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (States is null) throw new System.NullReferenceException(nameof(States));
            for (int i = 0; i < States.Length; i++)
            {
                if (States[i] is null) throw new System.NullReferenceException($"{nameof(States)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * States.Length;
                foreach (string s in States)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
