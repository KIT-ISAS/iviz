using System.Runtime.Serialization;

namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class GetGridMapInfo : IService
    {
        /// <summary> Request message. </summary>
        public GetGridMapInfoRequest Request { get; }
        
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
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "grid_map_msgs/GetGridMapInfo";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "a0be1719725f7fd7b490db4d64321ff2";
    }

    public sealed class GetGridMapInfoRequest : Internal.EmptyRequest
    {
    }

    public sealed class GetGridMapInfoResponse : IResponse
    {
        
        // Grid map info
        public grid_map_msgs.GridMapInfo info;
    
        /// <summary> Constructor for empty message. </summary>
        public GetGridMapInfoResponse()
        {
            info = new grid_map_msgs.GridMapInfo();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            info.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            info.Serialize(ref ptr, end);
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
