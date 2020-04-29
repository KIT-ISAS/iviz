using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetTexture : IService
    {
        /// <summary> Request message. </summary>
        public GetTextureRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public GetTextureResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetTexture()
        {
            Request = new GetTextureRequest();
            Response = new GetTextureResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetTexture(GetTextureRequest request)
        {
            Request = request;
            Response = new GetTextureResponse();
        }
        
        public IService Create() => new GetTexture();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "mesh_msgs/GetTexture";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "48823554c65f6c317f12f79207ce78ac";
    }

    public sealed class GetTextureRequest : IRequest
    {
        public string uuid;
        public uint texture_index;
    
        /// <summary> Constructor for empty message. </summary>
        public GetTextureRequest()
        {
            uuid = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out uuid, ref ptr, end);
            BuiltIns.Deserialize(out texture_index, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(uuid, ref ptr, end);
            BuiltIns.Serialize(texture_index, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(uuid);
                return size;
            }
        }
    }

    public sealed class GetTextureResponse : IResponse
    {
        public mesh_msgs.MeshTexture texture;
    
        /// <summary> Constructor for empty message. </summary>
        public GetTextureResponse()
        {
            texture = new mesh_msgs.MeshTexture();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            texture.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            texture.Serialize(ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += texture.RosMessageLength;
                return size;
            }
        }
    }
}
