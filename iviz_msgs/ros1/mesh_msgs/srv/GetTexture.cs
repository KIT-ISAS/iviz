using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class GetTexture : IService
    {
        /// Request message.
        [DataMember] public GetTextureRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetTextureResponse Response { get; set; }
        
        /// Empty constructor.
        public GetTexture()
        {
            Request = new GetTextureRequest();
            Response = new GetTextureResponse();
        }
        
        /// Setter constructor.
        public GetTexture(GetTextureRequest request)
        {
            Request = request;
            Response = new GetTextureResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetTextureRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetTextureResponse)value;
        }
        
        public const string ServiceType = "mesh_msgs/GetTexture";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "48823554c65f6c317f12f79207ce78ac";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetTextureRequest : IRequest<GetTexture, GetTextureResponse>, IDeserializableRos1<GetTextureRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "texture_index")] public uint TextureIndex;
    
        /// Constructor for empty message.
        public GetTextureRequest()
        {
            Uuid = "";
        }
        
        /// Explicit constructor.
        public GetTextureRequest(string Uuid, uint TextureIndex)
        {
            this.Uuid = Uuid;
            this.TextureIndex = TextureIndex;
        }
        
        /// Constructor with buffer.
        public GetTextureRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Uuid);
            b.Deserialize(out TextureIndex);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetTextureRequest(ref b);
        
        public GetTextureRequest RosDeserialize(ref ReadBuffer b) => new GetTextureRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
            b.Serialize(TextureIndex);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + WriteBuffer.GetStringSize(Uuid);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetTextureResponse : IResponse, IDeserializableRos1<GetTextureResponse>
    {
        [DataMember (Name = "texture")] public MeshMsgs.MeshTexture Texture;
    
        /// Constructor for empty message.
        public GetTextureResponse()
        {
            Texture = new MeshMsgs.MeshTexture();
        }
        
        /// Explicit constructor.
        public GetTextureResponse(MeshMsgs.MeshTexture Texture)
        {
            this.Texture = Texture;
        }
        
        /// Constructor with buffer.
        public GetTextureResponse(ref ReadBuffer b)
        {
            Texture = new MeshMsgs.MeshTexture(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetTextureResponse(ref b);
        
        public GetTextureResponse RosDeserialize(ref ReadBuffer b) => new GetTextureResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Texture.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Texture is null) BuiltIns.ThrowNullReference();
            Texture.RosValidate();
        }
    
        public int RosMessageLength => 0 + Texture.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
