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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "nav_msgs/SetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "c36922319011e63ed7784112ad4fdd32";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetMapRequest : IRequest<SetMap, SetMapResponse>, IDeserializable<SetMapRequest>
    {
        // Set a new map together with an initial pose
        [DataMember (Name = "map")] public NavMsgs.OccupancyGrid Map;
        [DataMember (Name = "initial_pose")] public GeometryMsgs.PoseWithCovarianceStamped InitialPose;
    
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
        internal SetMapRequest(ref Buffer b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
            InitialPose = new GeometryMsgs.PoseWithCovarianceStamped(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SetMapRequest(ref b);
        }
        
        SetMapRequest IDeserializable<SetMapRequest>.RosDeserialize(ref Buffer b)
        {
            return new SetMapRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Map.RosSerialize(ref b);
            InitialPose.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Map is null) throw new System.NullReferenceException(nameof(Map));
            Map.RosValidate();
            if (InitialPose is null) throw new System.NullReferenceException(nameof(InitialPose));
            InitialPose.RosValidate();
        }
    
        public int RosMessageLength => 0 + Map.RosMessageLength + InitialPose.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetMapResponse : IResponse, IDeserializable<SetMapResponse>
    {
        [DataMember (Name = "success")] public bool Success;
    
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
        internal SetMapResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SetMapResponse(ref b);
        }
        
        SetMapResponse IDeserializable<SetMapResponse>.RosDeserialize(ref Buffer b)
        {
            return new SetMapResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
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
    
        public override string ToString() => Extensions.ToString(this);
    }
}
