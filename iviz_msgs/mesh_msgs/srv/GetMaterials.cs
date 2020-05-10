using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetMaterials : IService
    {
        /// <summary> Request message. </summary>
        public GetMaterialsRequest Request { get; set; }
        
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
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetMaterialsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetMaterialsResponse)value;
        }
        
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
        public string uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMaterialsRequest()
        {
            uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMaterialsRequest(string uuid)
        {
            this.uuid = uuid ?? throw new System.ArgumentNullException(nameof(uuid));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetMaterialsRequest(Buffer b)
        {
            this.uuid = b.DeserializeString();
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetMaterialsRequest(b);
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

    public sealed class GetMaterialsResponse : IResponse
    {
        public mesh_msgs.MeshMaterialsStamped mesh_materials_stamped { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMaterialsResponse()
        {
            mesh_materials_stamped = new mesh_msgs.MeshMaterialsStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMaterialsResponse(mesh_msgs.MeshMaterialsStamped mesh_materials_stamped)
        {
            this.mesh_materials_stamped = mesh_materials_stamped ?? throw new System.ArgumentNullException(nameof(mesh_materials_stamped));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetMaterialsResponse(Buffer b)
        {
            this.mesh_materials_stamped = new mesh_msgs.MeshMaterialsStamped(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetMaterialsResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.mesh_materials_stamped.Serialize(b);
        }
        
        public void Validate()
        {
            if (mesh_materials_stamped is null) throw new System.NullReferenceException();
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
