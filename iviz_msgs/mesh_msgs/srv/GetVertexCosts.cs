using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetVertexCosts : IService
    {
        /// Request message.
        [DataMember] public GetVertexCostsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetVertexCostsResponse Response { get; set; }
        
        /// Empty constructor.
        public GetVertexCosts()
        {
            Request = new GetVertexCostsRequest();
            Response = new GetVertexCostsResponse();
        }
        
        /// Setter constructor.
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
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "mesh_msgs/GetVertexCosts";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "d0fc06ce39b58884e8cdf147765b9d6b";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetVertexCostsRequest : IRequest<GetVertexCosts, GetVertexCostsResponse>, IDeserializable<GetVertexCostsRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        /// Constructor for empty message.
        public GetVertexCostsRequest()
        {
            Uuid = "";
        }
        
        /// Explicit constructor.
        public GetVertexCostsRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// Constructor with buffer.
        public GetVertexCostsRequest(ref ReadBuffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetVertexCostsRequest(ref b);
        
        public GetVertexCostsRequest RosDeserialize(ref ReadBuffer b) => new GetVertexCostsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Uuid);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetVertexCostsResponse : IResponse, IDeserializable<GetVertexCostsResponse>
    {
        [DataMember (Name = "mesh_vertex_costs_stamped")] public MeshMsgs.MeshVertexCostsStamped MeshVertexCostsStamped;
    
        /// Constructor for empty message.
        public GetVertexCostsResponse()
        {
            MeshVertexCostsStamped = new MeshMsgs.MeshVertexCostsStamped();
        }
        
        /// Explicit constructor.
        public GetVertexCostsResponse(MeshMsgs.MeshVertexCostsStamped MeshVertexCostsStamped)
        {
            this.MeshVertexCostsStamped = MeshVertexCostsStamped;
        }
        
        /// Constructor with buffer.
        public GetVertexCostsResponse(ref ReadBuffer b)
        {
            MeshVertexCostsStamped = new MeshMsgs.MeshVertexCostsStamped(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetVertexCostsResponse(ref b);
        
        public GetVertexCostsResponse RosDeserialize(ref ReadBuffer b) => new GetVertexCostsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            MeshVertexCostsStamped.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MeshVertexCostsStamped is null) throw new System.NullReferenceException(nameof(MeshVertexCostsStamped));
            MeshVertexCostsStamped.RosValidate();
        }
    
        public int RosMessageLength => 0 + MeshVertexCostsStamped.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
