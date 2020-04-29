using System.Runtime.Serialization;

namespace Iviz.Msgs.nav_msgs
{
    public sealed class GetMap : IService
    {
        /// <summary> Request message. </summary>
        public GetMapRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public GetMapResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetMap()
        {
            Request = new GetMapRequest();
            Response = new GetMapResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetMap(GetMapRequest request)
        {
            Request = request;
            Response = new GetMapResponse();
        }
        
        public IService Create() => new GetMap();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "nav_msgs/GetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
    }

    public sealed class GetMapRequest : IRequest
    {
        // Get the map as a nav_msgs/OccupancyGrid
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 0;
    }

    public sealed class GetMapResponse : IResponse
    {
        public nav_msgs.OccupancyGrid map;
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapResponse()
        {
            map = new nav_msgs.OccupancyGrid();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            map.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            map.Serialize(ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += map.RosMessageLength;
                return size;
            }
        }
    }
}
