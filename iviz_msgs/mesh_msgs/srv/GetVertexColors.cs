using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetVertexColors : IService
    {
        /// <summary> Request message. </summary>
        public GetVertexColorsRequest Request { get; set; }
        
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
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetVertexColorsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetVertexColorsResponse)value;
        }
        
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
        public string uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexColorsRequest()
        {
            uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetVertexColorsRequest(string uuid)
        {
            this.uuid = uuid ?? throw new System.ArgumentNullException(nameof(uuid));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetVertexColorsRequest(Buffer b)
        {
            this.uuid = b.DeserializeString();
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetVertexColorsRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.uuid);
        }
        
        public void Validate()
        {
            if (uuid is null) throw new System.NullReferenceException();
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
        public mesh_msgs.MeshVertexColorsStamped mesh_vertex_colors_stamped { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexColorsResponse()
        {
            mesh_vertex_colors_stamped = new mesh_msgs.MeshVertexColorsStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetVertexColorsResponse(mesh_msgs.MeshVertexColorsStamped mesh_vertex_colors_stamped)
        {
            this.mesh_vertex_colors_stamped = mesh_vertex_colors_stamped ?? throw new System.ArgumentNullException(nameof(mesh_vertex_colors_stamped));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetVertexColorsResponse(Buffer b)
        {
            this.mesh_vertex_colors_stamped = new mesh_msgs.MeshVertexColorsStamped(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetVertexColorsResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.mesh_vertex_colors_stamped.Serialize(b);
        }
        
        public void Validate()
        {
            if (mesh_vertex_colors_stamped is null) throw new System.NullReferenceException();
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
