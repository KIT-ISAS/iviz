using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    [DataContract]
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "mesh_msgs/GetUUID";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "18ad0215778d252d8f14959901273e8d";
    }

    public sealed class GetUUIDRequest : Internal.EmptyRequest
    {
    }

    public sealed class GetUUIDResponse : IResponse
    {
        [DataMember] public string uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetUUIDResponse()
        {
            uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetUUIDResponse(string uuid)
        {
            this.uuid = uuid ?? throw new System.ArgumentNullException(nameof(uuid));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetUUIDResponse(Buffer b)
        {
            this.uuid = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new GetUUIDResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.uuid);
        }
        
        public void Validate()
        {
            if (uuid is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(uuid);
                return size;
            }
        }
    }
}
