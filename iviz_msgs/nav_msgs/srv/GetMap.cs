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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "nav_msgs/GetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
    }

    public sealed class GetMapRequest : IRequest
    {
        // Get the map as a nav_msgs/OccupancyGrid
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetMapRequest(Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetMapRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 0;
    }

    public sealed class GetMapResponse : IResponse
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
        internal GetMapResponse(Buffer b)
        {
            Map = new NavMsgs.OccupancyGrid(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetMapResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
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
