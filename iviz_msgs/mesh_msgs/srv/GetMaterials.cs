using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetMaterials : IService
    {
        /// Request message.
        [DataMember] public GetMaterialsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetMaterialsResponse Response { get; set; }
        
        /// Empty constructor.
        public GetMaterials()
        {
            Request = new GetMaterialsRequest();
            Response = new GetMaterialsResponse();
        }
        
        /// Setter constructor.
        public GetMaterials(GetMaterialsRequest request)
        {
            Request = request;
            Response = new GetMaterialsResponse();
        }
        
        IService IService.Create() => new GetMaterials();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "mesh_msgs/GetMaterials";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "f9e04e76772e6c10688525f021cfc500";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMaterialsRequest : IRequest<GetMaterials, GetMaterialsResponse>, IDeserializable<GetMaterialsRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        /// Constructor for empty message.
        public GetMaterialsRequest()
        {
            Uuid = string.Empty;
        }
        
        /// Explicit constructor.
        public GetMaterialsRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// Constructor with buffer.
        internal GetMaterialsRequest(ref Buffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetMaterialsRequest(ref b);
        
        GetMaterialsRequest IDeserializable<GetMaterialsRequest>.RosDeserialize(ref Buffer b) => new GetMaterialsRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
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
    public sealed class GetMaterialsResponse : IResponse, IDeserializable<GetMaterialsResponse>
    {
        [DataMember (Name = "mesh_materials_stamped")] public MeshMsgs.MeshMaterialsStamped MeshMaterialsStamped;
    
        /// Constructor for empty message.
        public GetMaterialsResponse()
        {
            MeshMaterialsStamped = new MeshMsgs.MeshMaterialsStamped();
        }
        
        /// Explicit constructor.
        public GetMaterialsResponse(MeshMsgs.MeshMaterialsStamped MeshMaterialsStamped)
        {
            this.MeshMaterialsStamped = MeshMaterialsStamped;
        }
        
        /// Constructor with buffer.
        internal GetMaterialsResponse(ref Buffer b)
        {
            MeshMaterialsStamped = new MeshMsgs.MeshMaterialsStamped(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetMaterialsResponse(ref b);
        
        GetMaterialsResponse IDeserializable<GetMaterialsResponse>.RosDeserialize(ref Buffer b) => new GetMaterialsResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            MeshMaterialsStamped.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MeshMaterialsStamped is null) throw new System.NullReferenceException(nameof(MeshMaterialsStamped));
            MeshMaterialsStamped.RosValidate();
        }
    
        public int RosMessageLength => 0 + MeshMaterialsStamped.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
