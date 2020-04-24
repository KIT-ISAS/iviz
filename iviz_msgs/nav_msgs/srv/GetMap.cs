namespace Iviz.Msgs.nav_msgs
{
    public class GetMap : IService
    {
        public sealed class Request : IRequest
        {
            // Get the map as a nav_msgs/OccupancyGrid
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        
            public int GetLength() => 0;
        }

        public sealed class Response : IResponse
        {
            public nav_msgs.OccupancyGrid map;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
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
        
            public int GetLength()
            {
                int size = 0;
                size += map.GetLength();
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "nav_msgs/GetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public GetMap()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetMap(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new GetMap();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
