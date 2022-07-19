using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "mesh_msgs/GetGeometry";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "e21c42f8a3978429fcbcd1c03ddeb4e3";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetGeometryRequest : IRequest<GetGeometry, GetGeometryResponse>, IDeserializableRos1<GetGeometryRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        /// Constructor for empty message.
        public GetGeometryRequest()
        {
            Uuid = "";
        }
        
        /// Explicit constructor.
        public GetGeometryRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// Constructor with buffer.
        public GetGeometryRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Uuid);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetGeometryRequest(ref b);
        
        public GetGeometryRequest RosDeserialize(ref ReadBuffer b) => new GetGeometryRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Uuid);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetGeometryResponse : IResponse, IDeserializableRos1<GetGeometryResponse>
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
        public GetGeometryResponse(ref ReadBuffer b)
        {
            MeshGeometryStamped = new MeshMsgs.MeshGeometryStamped(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetGeometryResponse(ref b);
        
        public GetGeometryResponse RosDeserialize(ref ReadBuffer b) => new GetGeometryResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            MeshGeometryStamped.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MeshGeometryStamped is null) BuiltIns.ThrowNullReference();
            MeshGeometryStamped.RosValidate();
        }
    
        public int RosMessageLength => 0 + MeshGeometryStamped.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
