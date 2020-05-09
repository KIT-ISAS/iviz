using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetTexture : IService
    {
        /// <summary> Request message. </summary>
        public GetTextureRequest Request { get; set; }
        
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
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetTextureRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetTextureResponse)value;
        }
        
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
        public string uuid { get; set; }
        public uint texture_index { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetTextureRequest()
        {
            uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetTextureRequest(string uuid, uint texture_index)
        {
            this.uuid = uuid ?? throw new System.ArgumentNullException(nameof(uuid));
            this.texture_index = texture_index;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetTextureRequest(Buffer b)
        {
            this.uuid = BuiltIns.DeserializeString(b);
            this.texture_index = BuiltIns.DeserializeStruct<uint>(b);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetTextureRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.uuid, b);
            BuiltIns.Serialize(this.texture_index, b);
        }
        
        public void Validate()
        {
            if (uuid is null) throw new System.NullReferenceException();
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
        public mesh_msgs.MeshTexture texture { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetTextureResponse()
        {
            texture = new mesh_msgs.MeshTexture();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetTextureResponse(mesh_msgs.MeshTexture texture)
        {
            this.texture = texture ?? throw new System.ArgumentNullException(nameof(texture));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetTextureResponse(Buffer b)
        {
            this.texture = new mesh_msgs.MeshTexture(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetTextureResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.texture.Serialize(b);
        }
        
        public void Validate()
        {
            if (texture is null) throw new System.NullReferenceException();
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
