using System.Runtime.Serialization;

namespace Iviz.Msgs.nav_msgs
{
    public sealed class SetMap : IService
    {
        /// <summary> Request message. </summary>
        public SetMapRequest Request { get; }
        
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
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "nav_msgs/SetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "c36922319011e63ed7784112ad4fdd32";
    }

    public sealed class SetMapRequest : IRequest
    {
        // Set a new map together with an initial pose
        public nav_msgs.OccupancyGrid map;
        public geometry_msgs.PoseWithCovarianceStamped initial_pose;
    
        /// <summary> Constructor for empty message. </summary>
        public SetMapRequest()
        {
            map = new nav_msgs.OccupancyGrid();
            initial_pose = new geometry_msgs.PoseWithCovarianceStamped();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            map.Deserialize(ref ptr, end);
            initial_pose.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            map.Serialize(ref ptr, end);
            initial_pose.Serialize(ref ptr, end);
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
        public bool success;
        
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out success, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(success, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 1;
    }
}
