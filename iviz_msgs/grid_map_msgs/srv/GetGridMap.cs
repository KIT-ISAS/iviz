using System.Runtime.Serialization;

namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class GetGridMap : IService
    {
        /// <summary> Request message. </summary>
        public GetGridMapRequest Request { get; set; }
        
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
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetGridMapRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetGridMapResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "grid_map_msgs/GetGridMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "802c2cbc7b10fada2d44db75ddb8c738";
    }

    public sealed class GetGridMapRequest : IRequest
    {
        // Frame id of the submap position request.
        public string frame_id { get; set; }
        
        // Requested submap position in x-direction [m].
        public double position_x { get; set; }
        
        // Requested submap position in y-direction [m].
        public double position_y { get; set; }
        
        // Requested submap length in x-direction [m].
        public double length_x { get; set; }
        
        // Requested submap width in y-direction [m].
        public double length_y { get; set; }
        
        // Requested layers. If empty, get all layers.
        public string[] layers { get; set; }
        
    
        /// <summary> Constructor for empty message. </summary>
        public GetGridMapRequest()
        {
            frame_id = "";
            layers = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetGridMapRequest(string frame_id, double position_x, double position_y, double length_x, double length_y, string[] layers)
        {
            this.frame_id = frame_id ?? throw new System.ArgumentNullException(nameof(frame_id));
            this.position_x = position_x;
            this.position_y = position_y;
            this.length_x = length_x;
            this.length_y = length_y;
            this.layers = layers ?? throw new System.ArgumentNullException(nameof(layers));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetGridMapRequest(Buffer b)
        {
            this.frame_id = b.DeserializeString();
            this.position_x = b.Deserialize<double>();
            this.position_y = b.Deserialize<double>();
            this.length_x = b.Deserialize<double>();
            this.length_y = b.Deserialize<double>();
            this.layers = b.DeserializeStringArray(0);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetGridMapRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.frame_id);
            b.Serialize(this.position_x);
            b.Serialize(this.position_y);
            b.Serialize(this.length_x);
            b.Serialize(this.length_y);
            b.SerializeArray(this.layers, 0);
        }
        
        public void Validate()
        {
            if (frame_id is null) throw new System.NullReferenceException();
            if (layers is null) throw new System.NullReferenceException();
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
        public grid_map_msgs.GridMap map { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetGridMapResponse()
        {
            map = new grid_map_msgs.GridMap();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetGridMapResponse(grid_map_msgs.GridMap map)
        {
            this.map = map ?? throw new System.ArgumentNullException(nameof(map));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetGridMapResponse(Buffer b)
        {
            this.map = new grid_map_msgs.GridMap(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetGridMapResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.map.Serialize(b);
        }
        
        public void Validate()
        {
            if (map is null) throw new System.NullReferenceException();
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
