using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class GetTexture : IService
    {
        /// Request message.
        [DataMember] public GetTextureRequest Request;
        
        /// Response message.
        [DataMember] public GetTextureResponse Response;
        
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
    public sealed class GetTextureRequest : IRequest<GetTexture, GetTextureResponse>, IDeserializable<GetTextureRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "texture_index")] public uint TextureIndex;
    
        public GetTextureRequest()
        {
            Uuid = "";
        }
        
        public GetTextureRequest(string Uuid, uint TextureIndex)
        {
            this.Uuid = Uuid;
            this.TextureIndex = TextureIndex;
        }
        
        public GetTextureRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Uuid);
            b.Deserialize(out TextureIndex);
        }
        
        public GetTextureRequest(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Uuid);
            b.Deserialize(out TextureIndex);
        }
        
        public GetTextureRequest RosDeserialize(ref ReadBuffer b) => new GetTextureRequest(ref b);
        
        public GetTextureRequest RosDeserialize(ref ReadBuffer2 b) => new GetTextureRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
            b.Serialize(TextureIndex);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Uuid);
            b.Serialize(TextureIndex);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + WriteBuffer.GetStringSize(Uuid);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.AddLength(c, Uuid);
            c = WriteBuffer2.Align4(c);
            c += 4; /* TextureIndex */
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetTextureResponse : IResponse, IDeserializable<GetTextureResponse>
    {
        [DataMember (Name = "texture")] public MeshMsgs.MeshTexture Texture;
    
        public GetTextureResponse()
        {
            Texture = new MeshMsgs.MeshTexture();
        }
        
        public GetTextureResponse(MeshMsgs.MeshTexture Texture)
        {
            this.Texture = Texture;
        }
        
        public GetTextureResponse(ref ReadBuffer b)
        {
            Texture = new MeshMsgs.MeshTexture(ref b);
        }
        
        public GetTextureResponse(ref ReadBuffer2 b)
        {
            Texture = new MeshMsgs.MeshTexture(ref b);
        }
        
        public GetTextureResponse RosDeserialize(ref ReadBuffer b) => new GetTextureResponse(ref b);
        
        public GetTextureResponse RosDeserialize(ref ReadBuffer2 b) => new GetTextureResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Texture.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Texture.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Texture is null) BuiltIns.ThrowNullReference();
            Texture.RosValidate();
        }
    
        public int RosMessageLength => 0 + Texture.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Texture.AddRos2MessageLength(c);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
