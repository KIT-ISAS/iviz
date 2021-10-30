using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/GetTexture")]
    public sealed class GetTexture : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetTextureRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetTextureResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetTexture()
        {
            Request = new GetTextureRequest();
            Response = new GetTextureResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetTexture(GetTextureRequest request)
        {
            Request = request;
            Response = new GetTextureResponse();
        }
        
        IService IService.Create() => new GetTexture();
        
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "mesh_msgs/GetTexture";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "48823554c65f6c317f12f79207ce78ac";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetTextureRequest : IRequest<GetTexture, GetTextureResponse>, IDeserializable<GetTextureRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "texture_index")] public uint TextureIndex;
    
        /// <summary> Constructor for empty message. </summary>
        public GetTextureRequest()
        {
            Uuid = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetTextureRequest(string Uuid, uint TextureIndex)
        {
            this.Uuid = Uuid;
            this.TextureIndex = TextureIndex;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetTextureRequest(ref Buffer b)
        {
            Uuid = b.DeserializeString();
            TextureIndex = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetTextureRequest(ref b);
        }
        
        GetTextureRequest IDeserializable<GetTextureRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetTextureRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Uuid);
            b.Serialize(TextureIndex);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Uuid);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetTextureResponse : IResponse, IDeserializable<GetTextureResponse>
    {
        [DataMember (Name = "texture")] public MeshMsgs.MeshTexture Texture;
    
        /// <summary> Constructor for empty message. </summary>
        public GetTextureResponse()
        {
            Texture = new MeshMsgs.MeshTexture();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetTextureResponse(MeshMsgs.MeshTexture Texture)
        {
            this.Texture = Texture;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetTextureResponse(ref Buffer b)
        {
            Texture = new MeshMsgs.MeshTexture(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetTextureResponse(ref b);
        }
        
        GetTextureResponse IDeserializable<GetTextureResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetTextureResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Texture.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Texture is null) throw new System.NullReferenceException(nameof(Texture));
            Texture.RosValidate();
        }
    
        public int RosMessageLength => 0 + Texture.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
