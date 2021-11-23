using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetGeometry : IService
    {
        /// Request message.
        [DataMember] public GetGeometryRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetGeometryResponse Response { get; set; }
        
        /// Empty constructor.
        public GetGeometry()
        {
            Request = new GetGeometryRequest();
            Response = new GetGeometryResponse();
        }
        
        /// Setter constructor.
        public GetGeometry(GetGeometryRequest request)
        {
            Request = request;
            Response = new GetGeometryResponse();
        }
        
        IService IService.Create() => new GetGeometry();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "mesh_msgs/GetGeometry";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "e21c42f8a3978429fcbcd1c03ddeb4e3";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetGeometryRequest : IRequest<GetGeometry, GetGeometryResponse>, IDeserializable<GetGeometryRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        /// Constructor for empty message.
        public GetGeometryRequest()
        {
            Uuid = string.Empty;
        }
        
        /// Explicit constructor.
        public GetGeometryRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// Constructor with buffer.
        internal GetGeometryRequest(ref Buffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetGeometryRequest(ref b);
        
        GetGeometryRequest IDeserializable<GetGeometryRequest>.RosDeserialize(ref Buffer b) => new GetGeometryRequest(ref b);
    
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
    public sealed class GetGeometryResponse : IResponse, IDeserializable<GetGeometryResponse>
    {
        [DataMember (Name = "mesh_geometry_stamped")] public MeshMsgs.MeshGeometryStamped MeshGeometryStamped;
    
        /// Constructor for empty message.
        public GetGeometryResponse()
        {
            MeshGeometryStamped = new MeshMsgs.MeshGeometryStamped();
        }
        
        /// Explicit constructor.
        public GetGeometryResponse(MeshMsgs.MeshGeometryStamped MeshGeometryStamped)
        {
            this.MeshGeometryStamped = MeshGeometryStamped;
        }
        
        /// Constructor with buffer.
        internal GetGeometryResponse(ref Buffer b)
        {
            MeshGeometryStamped = new MeshMsgs.MeshGeometryStamped(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetGeometryResponse(ref b);
        
        GetGeometryResponse IDeserializable<GetGeometryResponse>.RosDeserialize(ref Buffer b) => new GetGeometryResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            MeshGeometryStamped.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MeshGeometryStamped is null) throw new System.NullReferenceException(nameof(MeshGeometryStamped));
            MeshGeometryStamped.RosValidate();
        }
    
        public int RosMessageLength => 0 + MeshGeometryStamped.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
