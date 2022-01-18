using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetMap : IService
    {
        /// Request message.
        [DataMember] public GetMapRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetMapResponse Response { get; set; }
        
        /// Empty constructor.
        public GetMap()
        {
            Request = GetMapRequest.Singleton;
            Response = new GetMapResponse();
        }
        
        /// Setter constructor.
        public GetMap(GetMapRequest request)
        {
            Request = request;
            Response = new GetMapResponse();
        }
        
        IService IService.Create() => new GetMap();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetMapRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetMapResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "nav_msgs/GetMap";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMapRequest : IRequest<GetMap, GetMapResponse>, IDeserializable<GetMapRequest>
    {
        // Get the map as a nav_msgs/OccupancyGrid
    
        /// Constructor for empty message.
        public GetMapRequest()
        {
        }
        
        /// Constructor with buffer.
        public GetMapRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetMapRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly GetMapRequest Singleton = new GetMapRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMapResponse : IResponse, IDeserializable<GetMapResponse>
    {
        [DataMember (Name = "map")] public NavMsgs.OccupancyGrid Map;
    
        /// Constructor for empty message.
        public GetMapResponse()
        {
            Map = new NavMsgs.OccupancyGrid();
        }
        
        /// Explicit constructor.
        public GetMapResponse(NavMsgs.OccupancyGrid Map)
        {
            this.Map = Map;
        }
        
        /// Constructor with buffer.
        public GetMapResponse(ref ReadBuffer b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetMapResponse(ref b);
        
        public GetMapResponse RosDeserialize(ref ReadBuffer b) => new GetMapResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
