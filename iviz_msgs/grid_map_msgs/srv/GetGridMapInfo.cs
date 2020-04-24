namespace Iviz.Msgs.grid_map_msgs
{
    public class GetGridMapInfo : IService
    {
        public sealed class Request : IRequest
        {
        
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
            
            // Grid map info
            public grid_map_msgs.GridMapInfo info;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                info = new grid_map_msgs.GridMapInfo();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                info.Deserialize(ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                info.Serialize(ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 0;
                size += info.GetLength();
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "grid_map_msgs/GetGridMapInfo";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "a0be1719725f7fd7b490db4d64321ff2";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public GetGridMapInfo()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetGridMapInfo(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new GetGridMapInfo();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
