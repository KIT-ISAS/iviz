using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract (Name = "nav_msgs/GetMap")]
    public sealed class GetMap : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetMapRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetMapResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetMap()
        {
            Request = new GetMapRequest();
            Response = new GetMapResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "nav_msgs/GetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
    }

    [DataContract]
    public sealed class GetMapRequest : IRequest, IDeserializable<GetMapRequest>
    {
        // Get the map as a nav_msgs/OccupancyGrid
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMapRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapRequest(ref b);
        }
        
        GetMapRequest IDeserializable<GetMapRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetMapRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class GetMapResponse : IResponse, IDeserializable<GetMapResponse>
    {
        [DataMember (Name = "map")] public NavMsgs.OccupancyGrid Map { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapResponse()
        {
            Map = new NavMsgs.OccupancyGrid();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapResponse(NavMsgs.OccupancyGrid Map)
        {
            this.Map = Map;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMapResponse(ref Buffer b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapResponse(ref b);
        }
        
        GetMapResponse IDeserializable<GetMapResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetMapResponse(ref b);
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
