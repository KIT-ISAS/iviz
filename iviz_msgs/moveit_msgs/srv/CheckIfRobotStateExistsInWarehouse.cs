using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/CheckIfRobotStateExistsInWarehouse")]
    public sealed class CheckIfRobotStateExistsInWarehouse : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public CheckIfRobotStateExistsInWarehouseRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public CheckIfRobotStateExistsInWarehouseResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public CheckIfRobotStateExistsInWarehouse()
        {
            Request = new CheckIfRobotStateExistsInWarehouseRequest();
            Response = new CheckIfRobotStateExistsInWarehouseResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public CheckIfRobotStateExistsInWarehouse(CheckIfRobotStateExistsInWarehouseRequest request)
        {
            Request = request;
            Response = new CheckIfRobotStateExistsInWarehouseResponse();
        }
        
        IService IService.Create() => new CheckIfRobotStateExistsInWarehouse();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (CheckIfRobotStateExistsInWarehouseRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (CheckIfRobotStateExistsInWarehouseResponse)value;
        }
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/CheckIfRobotStateExistsInWarehouse";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "3b47364b81dd44f643fa67c9cd127486";
    }

    [DataContract]
    public sealed class CheckIfRobotStateExistsInWarehouseRequest : IRequest<CheckIfRobotStateExistsInWarehouse, CheckIfRobotStateExistsInWarehouseResponse>, IDeserializable<CheckIfRobotStateExistsInWarehouseRequest>
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "robot")] public string Robot { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public CheckIfRobotStateExistsInWarehouseRequest()
        {
            Name = string.Empty;
            Robot = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public CheckIfRobotStateExistsInWarehouseRequest(string Name, string Robot)
        {
            this.Name = Name;
            this.Robot = Robot;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public CheckIfRobotStateExistsInWarehouseRequest(ref Buffer b)
        {
            Name = b.DeserializeString();
            Robot = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new CheckIfRobotStateExistsInWarehouseRequest(ref b);
        }
        
        CheckIfRobotStateExistsInWarehouseRequest IDeserializable<CheckIfRobotStateExistsInWarehouseRequest>.RosDeserialize(ref Buffer b)
        {
            return new CheckIfRobotStateExistsInWarehouseRequest(ref b);
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
    }

    [DataContract]
    public sealed class CheckIfRobotStateExistsInWarehouseResponse : IResponse, IDeserializable<CheckIfRobotStateExistsInWarehouseResponse>
    {
        [DataMember (Name = "exists")] public bool Exists { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public CheckIfRobotStateExistsInWarehouseResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public CheckIfRobotStateExistsInWarehouseResponse(bool Exists)
        {
            this.Exists = Exists;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public CheckIfRobotStateExistsInWarehouseResponse(ref Buffer b)
        {
            Exists = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new CheckIfRobotStateExistsInWarehouseResponse(ref b);
        }
        
        CheckIfRobotStateExistsInWarehouseResponse IDeserializable<CheckIfRobotStateExistsInWarehouseResponse>.RosDeserialize(ref Buffer b)
        {
            return new CheckIfRobotStateExistsInWarehouseResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Exists);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    }
}
