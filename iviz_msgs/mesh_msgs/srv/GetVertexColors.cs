using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetVertexColors : IService
    {
        /// <summary> Request message. </summary>
        public GetVertexColorsRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public GetVertexColorsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetVertexColors()
        {
            Request = new GetVertexColorsRequest();
            Response = new GetVertexColorsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetVertexColors(GetVertexColorsRequest request)
        {
            Request = request;
            Response = new GetVertexColorsResponse();
        }
        
        public IService Create() => new GetVertexColors();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "mesh_msgs/GetVertexColors";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "f9925939094ed9c8a413184db9bca5b3";
    }

    public sealed class GetVertexColorsRequest : IRequest
    {
        public string uuid;
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexColorsRequest()
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

    public sealed class GetVertexColorsResponse : IResponse
    {
        public mesh_msgs.MeshVertexColorsStamped mesh_vertex_colors_stamped;
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexColorsResponse()
        {
            mesh_vertex_colors_stamped = new mesh_msgs.MeshVertexColorsStamped();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            mesh_vertex_colors_stamped.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            mesh_vertex_colors_stamped.Serialize(ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += mesh_vertex_colors_stamped.RosMessageLength;
                return size;
            }
        }
    }
}
