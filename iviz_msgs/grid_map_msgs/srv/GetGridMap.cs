using System.Runtime.Serialization;

namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class GetGridMap : IService
    {
        /// <summary> Request message. </summary>
        public GetGridMapRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public GetGridMapResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetGridMap()
        {
            Request = new GetGridMapRequest();
            Response = new GetGridMapResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetGridMap(GetGridMapRequest request)
        {
            Request = request;
            Response = new GetGridMapResponse();
        }
        
        public IService Create() => new GetGridMap();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "grid_map_msgs/GetGridMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "802c2cbc7b10fada2d44db75ddb8c738";
    }

    public sealed class GetGridMapRequest : IRequest
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
        public GetGridMapRequest()
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
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 40;
                size += BuiltIns.UTF8.GetByteCount(frame_id);
                size += 4 * layers.Length;
                for (int i = 0; i < layers.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(layers[i]);
                }
                return size;
            }
        }
    }

    public sealed class GetGridMapResponse : IResponse
    {
        
        // Submap
        public grid_map_msgs.GridMap map;
    
        /// <summary> Constructor for empty message. </summary>
        public GetGridMapResponse()
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
