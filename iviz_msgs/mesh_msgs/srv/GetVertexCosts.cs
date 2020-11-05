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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "mesh_msgs/GetVertexCosts";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "d0fc06ce39b58884e8cdf147765b9d6b";
    }

    public sealed class GetVertexCostsRequest : IRequest, IDeserializable<GetVertexCostsRequest>
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
        internal GetVertexCostsRequest(ref Buffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetVertexCostsRequest(ref b);
        }
        
        GetVertexCostsRequest IDeserializable<GetVertexCostsRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetVertexCostsRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
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

    public sealed class GetVertexCostsResponse : IResponse, IDeserializable<GetVertexCostsResponse>
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
        internal GetVertexCostsResponse(ref Buffer b)
        {
            MeshVertexCostsStamped = new MeshMsgs.MeshVertexCostsStamped(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetVertexCostsResponse(ref b);
        }
        
        GetVertexCostsResponse IDeserializable<GetVertexCostsResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetVertexCostsResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            MeshVertexCostsStamped.RosSerialize(ref b);
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
