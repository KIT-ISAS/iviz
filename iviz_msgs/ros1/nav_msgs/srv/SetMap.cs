using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class SetMap : IService
    {
        /// Request message.
        [DataMember] public SetMapRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SetMapResponse Response { get; set; }
        
        /// Empty constructor.
        public SetMap()
        {
            Request = new SetMapRequest();
            Response = new SetMapResponse();
        }
        
        /// Setter constructor.
        public SetMap(SetMapRequest request)
        {
            Request = request;
            Response = new SetMapResponse();
        }
        
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
        
        public const string ServiceType = "nav_msgs/SetMap";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "c36922319011e63ed7784112ad4fdd32";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetMapRequest : IRequest<SetMap, SetMapResponse>, IDeserializableRos1<SetMapRequest>
    {
        // Set a new map together with an initial pose
        [DataMember (Name = "map")] public NavMsgs.OccupancyGrid Map;
        [DataMember (Name = "initial_pose")] public GeometryMsgs.PoseWithCovarianceStamped InitialPose;
    
        public SetMapRequest()
        {
            Map = new NavMsgs.OccupancyGrid();
            InitialPose = new GeometryMsgs.PoseWithCovarianceStamped();
        }
        
        public SetMapRequest(NavMsgs.OccupancyGrid Map, GeometryMsgs.PoseWithCovarianceStamped InitialPose)
        {
            this.Map = Map;
            this.InitialPose = InitialPose;
        }
        
        public SetMapRequest(ref ReadBuffer b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
            InitialPose = new GeometryMsgs.PoseWithCovarianceStamped(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new SetMapRequest(ref b);
        
        public SetMapRequest RosDeserialize(ref ReadBuffer b) => new SetMapRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Map.RosSerialize(ref b);
            InitialPose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Map is null) BuiltIns.ThrowNullReference();
            Map.RosValidate();
            if (InitialPose is null) BuiltIns.ThrowNullReference();
            InitialPose.RosValidate();
        }
    
        public int RosMessageLength => 0 + Map.RosMessageLength + InitialPose.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetMapResponse : IResponse, IDeserializableRos1<SetMapResponse>
    {
        [DataMember (Name = "success")] public bool Success;
    
        public SetMapResponse()
        {
        }
        
        public SetMapResponse(bool Success)
        {
            this.Success = Success;
        }
        
        public SetMapResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new SetMapResponse(ref b);
        
        public SetMapResponse RosDeserialize(ref ReadBuffer b) => new SetMapResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
