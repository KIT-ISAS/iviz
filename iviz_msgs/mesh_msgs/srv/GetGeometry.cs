using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetGeometry : IService
    {
        /// <summary> Request message. </summary>
        public GetGeometryRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public GetGeometryResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetGeometry()
        {
            Request = new GetGeometryRequest();
            Response = new GetGeometryResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetGeometry(GetGeometryRequest request)
        {
            Request = request;
            Response = new GetGeometryResponse();
        }
        
        public IService Create() => new GetGeometry();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "mesh_msgs/GetGeometry";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "e21c42f8a3978429fcbcd1c03ddeb4e3";
    }

    public sealed class GetGeometryRequest : IRequest
    {
        public string uuid;
    
        /// <summary> Constructor for empty message. </summary>
        public GetGeometryRequest()
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

    public sealed class GetGeometryResponse : IResponse
    {
        public mesh_msgs.MeshGeometryStamped mesh_geometry_stamped;
    
        /// <summary> Constructor for empty message. </summary>
        public GetGeometryResponse()
        {
            mesh_geometry_stamped = new mesh_msgs.MeshGeometryStamped();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            mesh_geometry_stamped.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            mesh_geometry_stamped.Serialize(ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += mesh_geometry_stamped.RosMessageLength;
                return size;
            }
        }
    }
}
