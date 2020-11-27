using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/GetUUID")]
    public sealed class GetUUID : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetUUIDRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetUUIDResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetUUID()
        {
            Request = new GetUUIDRequest();
            Response = new GetUUIDResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetUUID(GetUUIDRequest request)
        {
            Request = request;
            Response = new GetUUIDResponse();
        }
        
        IService IService.Create() => new GetUUID();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetUUIDRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetUUIDResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "mesh_msgs/GetUUID";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "18ad0215778d252d8f14959901273e8d";
    }

    [DataContract]
    public sealed class GetUUIDRequest : Internal.EmptyRequest
    {
    }

    [DataContract]
    public sealed class GetUUIDResponse : IResponse, IDeserializable<GetUUIDResponse>
    {
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetUUIDResponse()
        {
            Uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetUUIDResponse(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetUUIDResponse(ref Buffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetUUIDResponse(ref b);
        }
        
        GetUUIDResponse IDeserializable<GetUUIDResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetUUIDResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
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
}
