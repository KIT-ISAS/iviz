using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "nav_msgs/GetMap";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "6cdd0a18e0aff5b0a3ca2326a89b54ff";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMapRequest : IRequest<GetMap, GetMapResponse>, IDeserializableRos1<GetMapRequest>
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
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetMapRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static GetMapRequest? singleton;
        public static GetMapRequest Singleton => singleton ??= new GetMapRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMapResponse : IResponse, IDeserializableRos1<GetMapResponse>
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
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetMapResponse(ref b);
        
        public GetMapResponse RosDeserialize(ref ReadBuffer b) => new GetMapResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Map is null) BuiltIns.ThrowNullReference();
            Map.RosValidate();
        }
    
        public int RosMessageLength => 0 + Map.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
