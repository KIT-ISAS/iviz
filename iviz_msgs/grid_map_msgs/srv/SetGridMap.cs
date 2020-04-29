using System.Runtime.Serialization;

namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class SetGridMap : IService
    {
        /// <summary> Request message. </summary>
        public SetGridMapRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public SetGridMapResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SetGridMap()
        {
            Request = new SetGridMapRequest();
            Response = new SetGridMapResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetGridMap(SetGridMapRequest request)
        {
            Request = request;
            Response = new SetGridMapResponse();
        }
        
        public IService Create() => new SetGridMap();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "grid_map_msgs/SetGridMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "4f8e24cfd42bc1470fe765b7516ff7e5";
    }

    public sealed class SetGridMapRequest : IRequest
    {
        // map
        public grid_map_msgs.GridMap map;
        
    
        /// <summary> Constructor for empty message. </summary>
        public SetGridMapRequest()
        {
            map = new grid_map_msgs.GridMap();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            map.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            map.Serialize(ref ptr, end);
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

    public sealed class SetGridMapResponse : Internal.EmptyResponse
    {
    }
}
