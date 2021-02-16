using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/GetMaterials")]
    public sealed class GetMaterials : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetMaterialsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetMaterialsResponse Response { get; set; }
        
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "mesh_msgs/GetMaterials";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "f9e04e76772e6c10688525f021cfc500";
    }

    [DataContract]
    public sealed class GetMaterialsRequest : IRequest<GetMaterials, GetMaterialsResponse>, IDeserializable<GetMaterialsRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMaterialsRequest()
        {
            Uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMaterialsRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMaterialsRequest(ref Buffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMaterialsRequest(ref b);
        }
        
        GetMaterialsRequest IDeserializable<GetMaterialsRequest>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
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

    [DataContract]
    public sealed class GetMaterialsResponse : IResponse, IDeserializable<GetMaterialsResponse>
    {
        [DataMember (Name = "mesh_materials_stamped")] public MeshMsgs.MeshMaterialsStamped MeshMaterialsStamped { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMaterialsResponse()
        {
            MeshMaterialsStamped = new MeshMsgs.MeshMaterialsStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMaterialsResponse(MeshMsgs.MeshMaterialsStamped MeshMaterialsStamped)
        {
            this.MeshMaterialsStamped = MeshMaterialsStamped;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMaterialsResponse(ref Buffer b)
        {
            MeshMaterialsStamped = new MeshMsgs.MeshMaterialsStamped(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMaterialsResponse(ref b);
        }
        
        GetMaterialsResponse IDeserializable<GetMaterialsResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            MeshMaterialsStamped.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MeshMaterialsStamped is null) throw new System.NullReferenceException(nameof(MeshMaterialsStamped));
            MeshMaterialsStamped.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += MeshMaterialsStamped.RosMessageLength;
                return size;
            }
        }
    }
}
