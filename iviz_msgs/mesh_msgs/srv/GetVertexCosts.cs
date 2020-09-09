using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/GetVertexCosts")]
    public sealed class GetVertexCosts : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetVertexCostsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetVertexCostsResponse Response { get; set; }
        
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
        
        IService IService.Create() => new GetVertexCosts();
        
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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "mesh_msgs/GetVertexCosts";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "d0fc06ce39b58884e8cdf147765b9d6b";
    }

    public sealed class GetVertexCostsRequest : IRequest
    {
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexCostsRequest()
        {
            Uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetVertexCostsRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetVertexCostsRequest(Buffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetVertexCostsRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Uuid);
                return size;
            }
        }
    }

    public sealed class GetVertexCostsResponse : IResponse
    {
        [DataMember (Name = "mesh_vertex_costs_stamped")] public MeshMsgs.MeshVertexCostsStamped MeshVertexCostsStamped { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexCostsResponse()
        {
            MeshVertexCostsStamped = new MeshMsgs.MeshVertexCostsStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetVertexCostsResponse(MeshMsgs.MeshVertexCostsStamped MeshVertexCostsStamped)
        {
            this.MeshVertexCostsStamped = MeshVertexCostsStamped;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetVertexCostsResponse(Buffer b)
        {
            MeshVertexCostsStamped = new MeshMsgs.MeshVertexCostsStamped(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetVertexCostsResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            MeshVertexCostsStamped.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (MeshVertexCostsStamped is null) throw new System.NullReferenceException(nameof(MeshVertexCostsStamped));
            MeshVertexCostsStamped.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += MeshVertexCostsStamped.RosMessageLength;
                return size;
            }
        }
    }
}
