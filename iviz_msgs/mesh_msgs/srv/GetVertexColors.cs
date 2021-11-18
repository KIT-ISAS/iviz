using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/GetVertexColors")]
    public sealed class GetVertexColors : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetVertexColorsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetVertexColorsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetVertexColors()
        {
            Request = new GetVertexColorsRequest();
            Response = new GetVertexColorsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetVertexColors(GetVertexColorsRequest request)
        {
            Request = request;
            Response = new GetVertexColorsResponse();
        }
        
        IService IService.Create() => new GetVertexColors();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetVertexColorsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetVertexColorsResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "mesh_msgs/GetVertexColors";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "f9925939094ed9c8a413184db9bca5b3";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetVertexColorsRequest : IRequest<GetVertexColors, GetVertexColorsResponse>, IDeserializable<GetVertexColorsRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexColorsRequest()
        {
            Uuid = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetVertexColorsRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetVertexColorsRequest(ref Buffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetVertexColorsRequest(ref b);
        }
        
        GetVertexColorsRequest IDeserializable<GetVertexColorsRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetVertexColorsRequest(ref b);
        }
    
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
    public sealed class GetVertexColorsResponse : IResponse, IDeserializable<GetVertexColorsResponse>
    {
        [DataMember (Name = "mesh_vertex_colors_stamped")] public MeshMsgs.MeshVertexColorsStamped MeshVertexColorsStamped;
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexColorsResponse()
        {
            MeshVertexColorsStamped = new MeshMsgs.MeshVertexColorsStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetVertexColorsResponse(MeshMsgs.MeshVertexColorsStamped MeshVertexColorsStamped)
        {
            this.MeshVertexColorsStamped = MeshVertexColorsStamped;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetVertexColorsResponse(ref Buffer b)
        {
            MeshVertexColorsStamped = new MeshMsgs.MeshVertexColorsStamped(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetVertexColorsResponse(ref b);
        }
        
        GetVertexColorsResponse IDeserializable<GetVertexColorsResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetVertexColorsResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            MeshVertexColorsStamped.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MeshVertexColorsStamped is null) throw new System.NullReferenceException(nameof(MeshVertexColorsStamped));
            MeshVertexColorsStamped.RosValidate();
        }
    
        public int RosMessageLength => 0 + MeshVertexColorsStamped.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
