using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract]
    public sealed class GetGridMap : IService
    {
        /// Request message.
        [DataMember] public GetGridMapRequest Request;
        
        /// Response message.
        [DataMember] public GetGridMapResponse Response;
        
        /// Empty constructor.
        public GetGridMap()
        {
            Request = new GetGridMapRequest();
            Response = new GetGridMapResponse();
        }
        
        /// Setter constructor.
        public GetGridMap(GetGridMapRequest request)
        {
            Request = request;
            Response = new GetGridMapResponse();
        }
        
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
        
        public const string ServiceType = "grid_map_msgs/GetGridMap";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "802c2cbc7b10fada2d44db75ddb8c738";
        
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
    
        public GetGridMapRequest()
        {
            FrameId = "";
            Layers = EmptyArray<string>.Value;
        }
        
        public GetGridMapRequest(ref ReadBuffer b)
        {
            FrameId = b.DeserializeString();
            b.Deserialize(out PositionX);
            b.Deserialize(out PositionY);
            b.Deserialize(out LengthX);
            b.Deserialize(out LengthY);
            Layers = b.DeserializeStringArray();
        }
        
        public GetGridMapRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            FrameId = b.DeserializeString();
            b.Align8();
            b.Deserialize(out PositionX);
            b.Deserialize(out PositionY);
            b.Deserialize(out LengthX);
            b.Deserialize(out LengthY);
            Layers = b.DeserializeStringArray();
        }
        
        public GetGridMapRequest RosDeserialize(ref ReadBuffer b) => new GetGridMapRequest(ref b);
        
        public GetGridMapRequest RosDeserialize(ref ReadBuffer2 b) => new GetGridMapRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(FrameId);
            b.Serialize(PositionX);
            b.Serialize(PositionY);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            b.Serialize(Layers.Length);
            b.SerializeArray(Layers);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(FrameId);
            b.Align8();
            b.Serialize(PositionX);
            b.Serialize(PositionY);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            b.Serialize(Layers.Length);
            b.SerializeArray(Layers);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(FrameId, nameof(FrameId));
            BuiltIns.ThrowIfNull(Layers, nameof(Layers));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 40;
                size += WriteBuffer.GetStringSize(FrameId);
                size += WriteBuffer.GetArraySize(Layers);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, FrameId);
            size = WriteBuffer2.Align8(size);
            size += 8; // PositionX
            size += 8; // PositionY
            size += 8; // LengthX
            size += 8; // LengthY
            size = WriteBuffer2.AddLength(size, Layers);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetGridMapResponse : IResponse, IDeserializable<GetGridMapResponse>
    {
        // Submap
        [DataMember (Name = "map")] public GridMapMsgs.GridMap Map;
    
        public GetGridMapResponse()
        {
            Map = new GridMapMsgs.GridMap();
        }
        
        public GetGridMapResponse(GridMapMsgs.GridMap Map)
        {
            this.Map = Map;
        }
        
        public GetGridMapResponse(ref ReadBuffer b)
        {
            Map = new GridMapMsgs.GridMap(ref b);
        }
        
        public GetGridMapResponse(ref ReadBuffer2 b)
        {
            Map = new GridMapMsgs.GridMap(ref b);
        }
        
        public GetGridMapResponse RosDeserialize(ref ReadBuffer b) => new GetGridMapResponse(ref b);
        
        public GetGridMapResponse RosDeserialize(ref ReadBuffer2 b) => new GetGridMapResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Map, nameof(Map));
            Map.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += Map.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Map.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
