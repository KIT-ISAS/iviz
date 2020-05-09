using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetVertexCosts : IService
    {
        /// <summary> Request message. </summary>
        public GetVertexCostsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public GetVertexCostsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetVertexCosts()
        {
            Request = new GetVertexCostsRequest();
            Response = new GetVertexCostsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetVertexCosts(GetVertexCostsRequest request)
        {
            Request = request;
            Response = new GetVertexCostsResponse();
        }
        
        public IService Create() => new GetVertexCosts();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetVertexCostsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetVertexCostsResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "mesh_msgs/GetVertexCosts";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "d0fc06ce39b58884e8cdf147765b9d6b";
    }

    public sealed class GetVertexCostsRequest : IRequest
    {
        public string uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexCostsRequest()
        {
            uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetVertexCostsRequest(string uuid)
        {
            this.uuid = uuid ?? throw new System.ArgumentNullException(nameof(uuid));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetVertexCostsRequest(Buffer b)
        {
            this.uuid = BuiltIns.DeserializeString(b);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetVertexCostsRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.uuid, b);
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

    public sealed class GetVertexCostsResponse : IResponse
    {
        public mesh_msgs.MeshVertexCostsStamped mesh_vertex_costs_stamped { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexCostsResponse()
        {
            mesh_vertex_costs_stamped = new mesh_msgs.MeshVertexCostsStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetVertexCostsResponse(mesh_msgs.MeshVertexCostsStamped mesh_vertex_costs_stamped)
        {
            this.mesh_vertex_costs_stamped = mesh_vertex_costs_stamped ?? throw new System.ArgumentNullException(nameof(mesh_vertex_costs_stamped));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetVertexCostsResponse(Buffer b)
        {
            this.mesh_vertex_costs_stamped = new mesh_msgs.MeshVertexCostsStamped(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetVertexCostsResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.mesh_vertex_costs_stamped.Serialize(b);
        }
        
        public void Validate()
        {
            if (mesh_vertex_costs_stamped is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += mesh_vertex_costs_stamped.RosMessageLength;
                return size;
            }
        }
    }
}
