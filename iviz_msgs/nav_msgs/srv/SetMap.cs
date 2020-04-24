namespace Iviz.Msgs.nav_msgs
{
    public class SetMap : IService
    {
        public sealed class Request : IRequest
        {
            // Set a new map together with an initial pose
            public nav_msgs.OccupancyGrid map;
            public geometry_msgs.PoseWithCovarianceStamped initial_pose;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
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
        
            public int GetLength()
            {
                int size = 0;
                size += map.GetLength();
                size += initial_pose.GetLength();
                return size;
            }
        }

        public sealed class Response : IResponse
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
        
            public int GetLength() => 1;
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "nav_msgs/SetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "c36922319011e63ed7784112ad4fdd32";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public SetMap()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetMap(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new SetMap();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
