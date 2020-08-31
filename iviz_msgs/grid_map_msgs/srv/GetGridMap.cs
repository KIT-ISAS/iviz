using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract (Name = "grid_map_msgs/GetGridMap")]
    public sealed class GetGridMap : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetGridMapRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetGridMapResponse Response { get; set; }
        
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
        
        IService IService.Create() => new GetGridMap();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "grid_map_msgs/GetGridMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "802c2cbc7b10fada2d44db75ddb8c738";
    }

    public sealed class GetGridMapRequest : IRequest
    {
        // Frame id of the submap position request.
        [DataMember (Name = "frame_id")] public string FrameId { get; set; }
        // Requested submap position in x-direction [m].
        [DataMember (Name = "position_x")] public double PositionX { get; set; }
        // Requested submap position in y-direction [m].
        [DataMember (Name = "position_y")] public double PositionY { get; set; }
        // Requested submap length in x-direction [m].
        [DataMember (Name = "length_x")] public double LengthX { get; set; }
        // Requested submap width in y-direction [m].
        [DataMember (Name = "length_y")] public double LengthY { get; set; }
        // Requested layers. If empty, get all layers.
        [DataMember (Name = "layers")] public string[] Layers { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetGridMapRequest()
        {
            FrameId = "";
            Layers = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetGridMapRequest(string FrameId, double PositionX, double PositionY, double LengthX, double LengthY, string[] Layers)
        {
            this.FrameId = FrameId;
            this.PositionX = PositionX;
            this.PositionY = PositionY;
            this.LengthX = LengthX;
            this.LengthY = LengthY;
            this.Layers = Layers;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetGridMapRequest(Buffer b)
        {
            FrameId = b.DeserializeString();
            PositionX = b.Deserialize<double>();
            PositionY = b.Deserialize<double>();
            LengthX = b.Deserialize<double>();
            LengthY = b.Deserialize<double>();
            Layers = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetGridMapRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(FrameId);
            b.Serialize(PositionX);
            b.Serialize(PositionY);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            b.SerializeArray(Layers, 0);
        }
        
        public void RosValidate()
        {
            if (FrameId is null) throw new System.NullReferenceException(nameof(FrameId));
            if (Layers is null) throw new System.NullReferenceException(nameof(Layers));
            for (int i = 0; i < Layers.Length; i++)
            {
                if (Layers[i] is null) throw new System.NullReferenceException($"{nameof(Layers)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 40;
                size += BuiltIns.UTF8.GetByteCount(FrameId);
                size += 4 * Layers.Length;
                for (int i = 0; i < Layers.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(Layers[i]);
                }
                return size;
            }
        }
    }

    public sealed class GetGridMapResponse : IResponse
    {
        // Submap
        [DataMember (Name = "map")] public GridMapMsgs.GridMap Map { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetGridMapResponse()
        {
            Map = new GridMapMsgs.GridMap();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetGridMapResponse(GridMapMsgs.GridMap Map)
        {
            this.Map = Map;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetGridMapResponse(Buffer b)
        {
            Map = new GridMapMsgs.GridMap(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetGridMapResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Map.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (Map is null) throw new System.NullReferenceException(nameof(Map));
            Map.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Map.RosMessageLength;
                return size;
            }
        }
    }
}
