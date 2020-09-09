using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/GetGeometry")]
    public sealed class GetGeometry : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetGeometryRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetGeometryResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetGeometry()
        {
            Request = new GetGeometryRequest();
            Response = new GetGeometryResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "mesh_msgs/GetGeometry";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "e21c42f8a3978429fcbcd1c03ddeb4e3";
    }

    public sealed class GetGeometryRequest : IRequest
    {
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetGeometryRequest()
        {
            Uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetGeometryRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetGeometryRequest(Buffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetGeometryRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
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

    public sealed class GetGeometryResponse : IResponse
    {
        [DataMember (Name = "mesh_geometry_stamped")] public MeshMsgs.MeshGeometryStamped MeshGeometryStamped { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetGeometryResponse()
        {
            MeshGeometryStamped = new MeshMsgs.MeshGeometryStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetGeometryResponse(MeshMsgs.MeshGeometryStamped MeshGeometryStamped)
        {
            this.MeshGeometryStamped = MeshGeometryStamped;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetGeometryResponse(Buffer b)
        {
            MeshGeometryStamped = new MeshMsgs.MeshGeometryStamped(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetGeometryResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            MeshGeometryStamped.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (MeshGeometryStamped is null) throw new System.NullReferenceException(nameof(MeshGeometryStamped));
            MeshGeometryStamped.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += MeshGeometryStamped.RosMessageLength;
                return size;
            }
        }
    }
}
