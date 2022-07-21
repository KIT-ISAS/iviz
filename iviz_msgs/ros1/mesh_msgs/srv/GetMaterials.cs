using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "mesh_msgs/GetMaterials";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "f9e04e76772e6c10688525f021cfc500";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMaterialsRequest : IRequest<GetMaterials, GetMaterialsResponse>, IDeserializable<GetMaterialsRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        public GetMaterialsRequest()
        {
            Uuid = "";
        }
        
        public GetMaterialsRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        public GetMaterialsRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Uuid);
        }
        
        public GetMaterialsRequest(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Uuid);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetMaterialsRequest(ref b);
        
        public GetMaterialsRequest RosDeserialize(ref ReadBuffer b) => new GetMaterialsRequest(ref b);
        
        public GetMaterialsRequest RosDeserialize(ref ReadBuffer2 b) => new GetMaterialsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Uuid);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Uuid);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMaterialsResponse : IResponse, IDeserializable<GetMaterialsResponse>
    {
        [DataMember (Name = "mesh_materials_stamped")] public MeshMsgs.MeshMaterialsStamped MeshMaterialsStamped;
    
        public GetMaterialsResponse()
        {
            MeshMaterialsStamped = new MeshMsgs.MeshMaterialsStamped();
        }
        
        public GetMaterialsResponse(MeshMsgs.MeshMaterialsStamped MeshMaterialsStamped)
        {
            this.MeshMaterialsStamped = MeshMaterialsStamped;
        }
        
        public GetMaterialsResponse(ref ReadBuffer b)
        {
            MeshMaterialsStamped = new MeshMsgs.MeshMaterialsStamped(ref b);
        }
        
        public GetMaterialsResponse(ref ReadBuffer2 b)
        {
            MeshMaterialsStamped = new MeshMsgs.MeshMaterialsStamped(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetMaterialsResponse(ref b);
        
        public GetMaterialsResponse RosDeserialize(ref ReadBuffer b) => new GetMaterialsResponse(ref b);
        
        public GetMaterialsResponse RosDeserialize(ref ReadBuffer2 b) => new GetMaterialsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            MeshMaterialsStamped.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            MeshMaterialsStamped.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MeshMaterialsStamped is null) BuiltIns.ThrowNullReference();
            MeshMaterialsStamped.RosValidate();
        }
    
        public int RosMessageLength => 0 + MeshMaterialsStamped.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            MeshMaterialsStamped.AddRos2MessageLength(ref c);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
