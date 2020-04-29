using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetMaterials : IService
    {
        /// <summary> Request message. </summary>
        public GetMaterialsRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public GetMaterialsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetMaterials()
        {
            Request = new GetMaterialsRequest();
            Response = new GetMaterialsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetMaterials(GetMaterialsRequest request)
        {
            Request = request;
            Response = new GetMaterialsResponse();
        }
        
        public IService Create() => new GetMaterials();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "mesh_msgs/GetMaterials";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "f9e04e76772e6c10688525f021cfc500";
    }

    public sealed class GetMaterialsRequest : IRequest
    {
        public string uuid;
    
        /// <summary> Constructor for empty message. </summary>
        public GetMaterialsRequest()
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

    public sealed class GetMaterialsResponse : IResponse
    {
        public mesh_msgs.MeshMaterialsStamped mesh_materials_stamped;
    
        /// <summary> Constructor for empty message. </summary>
        public GetMaterialsResponse()
        {
            mesh_materials_stamped = new mesh_msgs.MeshMaterialsStamped();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            mesh_materials_stamped.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            mesh_materials_stamped.Serialize(ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += mesh_materials_stamped.RosMessageLength;
                return size;
            }
        }
    }
}
