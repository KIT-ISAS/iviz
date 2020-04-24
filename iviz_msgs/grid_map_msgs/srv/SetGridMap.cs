namespace Iviz.Msgs.grid_map_msgs
{
    public class SetGridMap : IService
    {
        public sealed class Request : IRequest
        {
            // map
            public grid_map_msgs.GridMap map;
            
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                map = new grid_map_msgs.GridMap();
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

        public sealed class Response : IResponse
        {
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        
            public int GetLength() => 0;
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "grid_map_msgs/SetGridMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "4f8e24cfd42bc1470fe765b7516ff7e5";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public SetGridMap()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetGridMap(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new SetGridMap();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
