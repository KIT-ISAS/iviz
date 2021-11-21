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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "grid_map_msgs/GetGridMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "802c2cbc7b10fada2d44db75ddb8c738";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetGridMapRequest : IRequest<GetGridMap, GetGridMapResponse>, IDeserializable<GetGridMapRequest>
    {
        // Frame id of the submap position request.
        [DataMember (Name = "frame_id")] public string FrameId;
        // Requested submap position in x-direction [m].
        [DataMember (Name = "position_x")] public double PositionX;
        // Requested submap position in y-direction [m].
        [DataMember (Name = "position_y")] public double PositionY;
        // Requested submap length in x-direction [m].
        [DataMember (Name = "length_x")] public double LengthX;
        // Requested submap width in y-direction [m].
        [DataMember (Name = "length_y")] public double LengthY;
        // Requested layers. If empty, get all layers.
        [DataMember (Name = "layers")] public string[] Layers;
    
        /// <summary> Constructor for empty message. </summary>
        public GetGridMapRequest()
        {
            FrameId = string.Empty;
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
        internal GetGridMapRequest(ref Buffer b)
        {
            FrameId = b.DeserializeString();
            PositionX = b.Deserialize<double>();
            PositionY = b.Deserialize<double>();
            LengthX = b.Deserialize<double>();
            LengthY = b.Deserialize<double>();
            Layers = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetGridMapRequest(ref b);
        }
        
        GetGridMapRequest IDeserializable<GetGridMapRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetGridMapRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
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
    
        public int RosMessageLength => 40 + BuiltIns.GetStringSize(FrameId) + BuiltIns.GetArraySize(Layers);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetGridMapResponse : IResponse, IDeserializable<GetGridMapResponse>
    {
        // Submap
        [DataMember (Name = "map")] public GridMapMsgs.GridMap Map;
    
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
        internal GetGridMapResponse(ref Buffer b)
        {
            Map = new GridMapMsgs.GridMap(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetGridMapResponse(ref b);
        }
        
        GetGridMapResponse IDeserializable<GetGridMapResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetGridMapResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Map is null) throw new System.NullReferenceException(nameof(Map));
            Map.RosValidate();
        }
    
        public int RosMessageLength => 0 + Map.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
