using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetUUID : IService
    {
        /// <summary> Request message. </summary>
        public GetUUIDRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public GetUUIDResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetUUID()
        {
            Request = new GetUUIDRequest();
            Response = new GetUUIDResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetUUID(GetUUIDRequest request)
        {
            Request = request;
            Response = new GetUUIDResponse();
        }
        
        public IService Create() => new GetUUID();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "mesh_msgs/GetUUID";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "18ad0215778d252d8f14959901273e8d";
    }

    public sealed class GetUUIDRequest : Internal.EmptyRequest
    {
    }

    public sealed class GetUUIDResponse : IResponse
    {
        public string uuid;
    
        /// <summary> Constructor for empty message. </summary>
        public GetUUIDResponse()
        {
            uuid = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out uuid, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(uuid, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(uuid);
                return size;
            }
        }
    }
}
