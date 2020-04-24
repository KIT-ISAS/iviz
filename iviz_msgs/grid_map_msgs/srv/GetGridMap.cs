namespace Iviz.Msgs.grid_map_msgs
{
    public class GetGridMap : IService
    {
        public sealed class Request : IRequest
        {
            // Frame id of the submap position request.
            public string frame_id;
            
            // Requested submap position in x-direction [m].
            public double position_x;
            
            // Requested submap position in y-direction [m].
            public double position_y;
            
            // Requested submap length in x-direction [m].
            public double length_x;
            
            // Requested submap width in y-direction [m].
            public double length_y;
            
            // Requested layers. If empty, get all layers.
            public string[] layers;
            
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                frame_id = "";
                layers = System.Array.Empty<string>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out frame_id, ref ptr, end);
                BuiltIns.Deserialize(out position_x, ref ptr, end);
                BuiltIns.Deserialize(out position_y, ref ptr, end);
                BuiltIns.Deserialize(out length_x, ref ptr, end);
                BuiltIns.Deserialize(out length_y, ref ptr, end);
                BuiltIns.Deserialize(out layers, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(frame_id, ref ptr, end);
                BuiltIns.Serialize(position_x, ref ptr, end);
                BuiltIns.Serialize(position_y, ref ptr, end);
                BuiltIns.Serialize(length_x, ref ptr, end);
                BuiltIns.Serialize(length_y, ref ptr, end);
                BuiltIns.Serialize(layers, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 44;
                size += frame_id.Length;
                for (int i = 0; i < layers.Length; i++)
                {
                    size += layers[i].Length;
                }
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            
            // Submap
            public grid_map_msgs.GridMap map;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
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
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "grid_map_msgs/GetGridMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "802c2cbc7b10fada2d44db75ddb8c738";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public GetGridMap()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetGridMap(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new GetGridMap();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
