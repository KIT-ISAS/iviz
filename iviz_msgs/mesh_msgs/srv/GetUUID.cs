using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetUUID : IService
    {
        /// Request message.
        [DataMember] public GetUUIDRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetUUIDResponse Response { get; set; }
        
        /// Empty constructor.
        public GetUUID()
        {
            Request = GetUUIDRequest.Singleton;
            Response = new GetUUIDResponse();
        }
        
        /// Setter constructor.
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
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "mesh_msgs/GetUUID";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "18ad0215778d252d8f14959901273e8d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetUUIDRequest : IRequest<GetUUID, GetUUIDResponse>, IDeserializable<GetUUIDRequest>
    {
    
        /// Constructor for empty message.
        public GetUUIDRequest()
        {
        }
        
        /// Constructor with buffer.
        internal GetUUIDRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetUUIDRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly GetUUIDRequest Singleton = new GetUUIDRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetUUIDResponse : IResponse, IDeserializable<GetUUIDResponse>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        /// Constructor for empty message.
        public GetUUIDResponse()
        {
            Uuid = string.Empty;
        }
        
        /// Explicit constructor.
        public GetUUIDResponse(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// Constructor with buffer.
        internal GetUUIDResponse(ref ReadBuffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetUUIDResponse(ref b);
        
        public GetUUIDResponse RosDeserialize(ref ReadBuffer b) => new GetUUIDResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
}
