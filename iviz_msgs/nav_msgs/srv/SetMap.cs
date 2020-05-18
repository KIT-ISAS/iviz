using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract (Name = "nav_msgs/SetMap")]
    public sealed class SetMap : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SetMapRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SetMapResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SetMap()
        {
            Request = new SetMapRequest();
            Response = new SetMapResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetMap(SetMapRequest request)
        {
            Request = request;
            Response = new SetMapResponse();
        }
        
        IService IService.Create() => new SetMap();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetMapRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetMapResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "nav_msgs/SetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "c36922319011e63ed7784112ad4fdd32";
    }

    public sealed class SetMapRequest : IRequest
    {
        // Set a new map together with an initial pose
        [DataMember (Name = "map")] public NavMsgs.OccupancyGrid Map { get; set; }
        [DataMember (Name = "initial_pose")] public GeometryMsgs.PoseWithCovarianceStamped InitialPose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SetMapRequest()
        {
            Map = new NavMsgs.OccupancyGrid();
            InitialPose = new GeometryMsgs.PoseWithCovarianceStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetMapRequest(NavMsgs.OccupancyGrid Map, GeometryMsgs.PoseWithCovarianceStamped InitialPose)
        {
            this.Map = Map;
            this.InitialPose = InitialPose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetMapRequest(Buffer b)
        {
            Map = new NavMsgs.OccupancyGrid(b);
            InitialPose = new GeometryMsgs.PoseWithCovarianceStamped(b);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new SetMapRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Map);
            b.Serialize(InitialPose);
        }
        
        public void Validate()
        {
            if (Map is null) throw new System.NullReferenceException();
            Map.Validate();
            if (InitialPose is null) throw new System.NullReferenceException();
            InitialPose.Validate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Map.RosMessageLength;
                size += InitialPose.RosMessageLength;
                return size;
            }
        }
    }

    public sealed class SetMapResponse : IResponse
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SetMapResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetMapResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetMapResponse(Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new SetMapResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.Success);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 1;
    }
}
