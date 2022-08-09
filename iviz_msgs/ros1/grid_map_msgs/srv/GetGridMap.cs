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
            Layers = System.Array.Empty<string>();
        }
        
        public GetGridMapRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out FrameId);
            b.Deserialize(out PositionX);
            b.Deserialize(out PositionY);
            b.Deserialize(out LengthX);
            b.Deserialize(out LengthY);
            b.DeserializeStringArray(out Layers);
        }
        
        public GetGridMapRequest(ref ReadBuffer2 b)
        {
            b.DeserializeString(out FrameId);
            b.Deserialize(out PositionX);
            b.Deserialize(out PositionY);
            b.Deserialize(out LengthX);
            b.Deserialize(out LengthY);
            b.DeserializeStringArray(out Layers);
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
            b.SerializeArray(Layers);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(FrameId);
            b.Serialize(PositionX);
            b.Serialize(PositionY);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            b.SerializeArray(Layers);
        }
        
        public void RosValidate()
        {
            if (FrameId is null) BuiltIns.ThrowNullReference();
            if (Layers is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Layers.Length; i++)
            {
                if (Layers[i] is null) BuiltIns.ThrowNullReference(nameof(Layers), i);
            }
        }
    
        public int RosMessageLength => 40 + WriteBuffer.GetStringSize(FrameId) + WriteBuffer.GetArraySize(Layers);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.AddLength(c, FrameId);
            c = WriteBuffer2.Align8(c);
            c += 8;  // PositionX
            c += 8;  // PositionY
            c += 8;  // LengthX
            c += 8;  // LengthY
            c = WriteBuffer2.AddLength(c, Layers);
            return c;
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
            if (Map is null) BuiltIns.ThrowNullReference();
            Map.RosValidate();
        }
    
        public int RosMessageLength => 0 + Map.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Map.AddRos2MessageLength(c);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
