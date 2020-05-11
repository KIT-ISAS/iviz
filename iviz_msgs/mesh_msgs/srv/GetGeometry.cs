using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetGeometry : IService
    {
        /// <summary> Request message. </summary>
        public GetGeometryRequest Request { get; set; }
        
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
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetGeometryRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetGeometryResponse)value;
        }
        
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
        public string uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetGeometryRequest()
        {
            uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetGeometryRequest(string uuid)
        {
            this.uuid = uuid ?? throw new System.ArgumentNullException(nameof(uuid));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetGeometryRequest(Buffer b)
        {
            this.uuid = b.DeserializeString();
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetGeometryRequest(b);
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

    public sealed class GetGeometryResponse : IResponse
    {
        public mesh_msgs.MeshGeometryStamped mesh_geometry_stamped { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetGeometryResponse()
        {
            mesh_geometry_stamped = new mesh_msgs.MeshGeometryStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetGeometryResponse(mesh_msgs.MeshGeometryStamped mesh_geometry_stamped)
        {
            this.mesh_geometry_stamped = mesh_geometry_stamped ?? throw new System.ArgumentNullException(nameof(mesh_geometry_stamped));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetGeometryResponse(Buffer b)
        {
            this.mesh_geometry_stamped = new mesh_msgs.MeshGeometryStamped(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetGeometryResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.mesh_geometry_stamped.Serialize(b);
        }
        
        public void Validate()
        {
            if (mesh_geometry_stamped is null) throw new System.NullReferenceException();
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
