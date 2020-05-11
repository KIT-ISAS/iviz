using System.Runtime.Serialization;

namespace Iviz.Msgs.nav_msgs
{
    public sealed class GetMap : IService
    {
        /// <summary> Request message. </summary>
        public GetMapRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public GetMapResponse Response { get; set; }
        
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
        
        public IService Create() => new GetMap();
        
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
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "nav_msgs/GetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
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
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetMapRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 0;
    }

    public sealed class GetMapResponse : IResponse
    {
        public nav_msgs.OccupancyGrid map { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapResponse()
        {
            map = new nav_msgs.OccupancyGrid();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapResponse(nav_msgs.OccupancyGrid map)
        {
            this.map = map ?? throw new System.ArgumentNullException(nameof(map));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetMapResponse(Buffer b)
        {
            this.map = new nav_msgs.OccupancyGrid(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetMapResponse(b);
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
