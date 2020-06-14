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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "mesh_msgs/GetVertexColors";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "f9925939094ed9c8a413184db9bca5b3";
    }

    public sealed class GetVertexColorsRequest : IRequest
    {
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexColorsRequest()
        {
            Uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetVertexColorsRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetVertexColorsRequest(Buffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetVertexColorsRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException();
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

    public sealed class GetVertexColorsResponse : IResponse
    {
        [DataMember (Name = "mesh_vertex_colors_stamped")] public MeshMsgs.MeshVertexColorsStamped MeshVertexColorsStamped { get; set; }
    
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
        internal GetVertexColorsResponse(Buffer b)
        {
            MeshVertexColorsStamped = new MeshMsgs.MeshVertexColorsStamped(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetVertexColorsResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            MeshVertexColorsStamped.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (MeshVertexColorsStamped is null) throw new System.NullReferenceException();
            MeshVertexColorsStamped.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += MeshVertexColorsStamped.RosMessageLength;
                return size;
            }
        }
    }
}
