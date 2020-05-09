using System.Runtime.Serialization;

namespace Iviz.Msgs.nav_msgs
{
    public sealed class SetMap : IService
    {
        /// <summary> Request message. </summary>
        public SetMapRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public SetMapResponse Response { get; set; }
        
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
        
        public IService Create() => new SetMap();
        
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
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "nav_msgs/SetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "c36922319011e63ed7784112ad4fdd32";
    }

    public sealed class SetMapRequest : IRequest
    {
        // Set a new map together with an initial pose
        public nav_msgs.OccupancyGrid map { get; set; }
        public geometry_msgs.PoseWithCovarianceStamped initial_pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SetMapRequest()
        {
            map = new nav_msgs.OccupancyGrid();
            initial_pose = new geometry_msgs.PoseWithCovarianceStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetMapRequest(nav_msgs.OccupancyGrid map, geometry_msgs.PoseWithCovarianceStamped initial_pose)
        {
            this.map = map ?? throw new System.ArgumentNullException(nameof(map));
            this.initial_pose = initial_pose ?? throw new System.ArgumentNullException(nameof(initial_pose));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetMapRequest(Buffer b)
        {
            this.map = new nav_msgs.OccupancyGrid(b);
            this.initial_pose = new geometry_msgs.PoseWithCovarianceStamped(b);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new SetMapRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.map.Serialize(b);
            this.initial_pose.Serialize(b);
        }
        
        public void Validate()
        {
            if (map is null) throw new System.NullReferenceException();
            if (initial_pose is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += map.RosMessageLength;
                size += initial_pose.RosMessageLength;
                return size;
            }
        }
    }

    public sealed class SetMapResponse : IResponse
    {
        public bool success { get; set; }
        
    
        /// <summary> Constructor for empty message. </summary>
        public SetMapResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetMapResponse(bool success)
        {
            this.success = success;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetMapResponse(Buffer b)
        {
            this.success = BuiltIns.DeserializeStruct<bool>(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new SetMapResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.success, b);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 1;
    }
}
