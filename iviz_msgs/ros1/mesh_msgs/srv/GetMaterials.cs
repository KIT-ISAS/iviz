using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class GetMaterials : IService<GetMaterialsRequest, GetMaterialsResponse>
    {
        /// Request message.
        [DataMember] public GetMaterialsRequest Request;
        
        /// Response message.
        [DataMember] public GetMaterialsResponse Response;
        
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
        
        public IService Generate() => new GetMaterials();
        
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
            Uuid = b.DeserializeString();
        }
        
        public GetMaterialsRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Uuid = b.DeserializeString();
        }
        
        public GetMaterialsRequest RosDeserialize(ref ReadBuffer b) => new GetMaterialsRequest(ref b);
        
        public GetMaterialsRequest RosDeserialize(ref ReadBuffer2 b) => new GetMaterialsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Uuid, nameof(Uuid));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Uuid);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Uuid);
            return size;
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
            BuiltIns.ThrowIfNull(MeshMaterialsStamped, nameof(MeshMaterialsStamped));
            MeshMaterialsStamped.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += MeshMaterialsStamped.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = MeshMaterialsStamped.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
