using System.Runtime.Serialization;

namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class GetGridMapInfo : IService
    {
        /// <summary> Request message. </summary>
        public GetGridMapInfoRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public GetGridMapInfoResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetGridMapInfo()
        {
            Request = new GetGridMapInfoRequest();
            Response = new GetGridMapInfoResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetGridMapInfo(GetGridMapInfoRequest request)
        {
            Request = request;
            Response = new GetGridMapInfoResponse();
        }
        
        public IService Create() => new GetGridMapInfo();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetGridMapInfoRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetGridMapInfoResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "grid_map_msgs/GetGridMapInfo";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "a0be1719725f7fd7b490db4d64321ff2";
    }

    public sealed class GetGridMapInfoRequest : Internal.EmptyRequest
    {
    }

    public sealed class GetGridMapInfoResponse : IResponse
    {
        
        // Grid map info
        public grid_map_msgs.GridMapInfo info { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetGridMapInfoResponse()
        {
            info = new grid_map_msgs.GridMapInfo();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetGridMapInfoResponse(grid_map_msgs.GridMapInfo info)
        {
            this.info = info ?? throw new System.ArgumentNullException(nameof(info));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetGridMapInfoResponse(Buffer b)
        {
            this.info = new grid_map_msgs.GridMapInfo(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetGridMapInfoResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.info.Serialize(b);
        }
        
        public void Validate()
        {
            if (info is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += info.RosMessageLength;
                return size;
            }
        }
    }
}
