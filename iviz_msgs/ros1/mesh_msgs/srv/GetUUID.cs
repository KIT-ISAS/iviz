using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "mesh_msgs/GetUUID";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "18ad0215778d252d8f14959901273e8d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetUUIDRequest : IRequest<GetUUID, GetUUIDResponse>, IDeserializableRos1<GetUUIDRequest>
    {
    
        /// Constructor for empty message.
        public GetUUIDRequest()
        {
        }
        
        /// Constructor with buffer.
        public GetUUIDRequest(ref ReadBuffer b)
        {
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetUUIDRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static GetUUIDRequest? singleton;
        public static GetUUIDRequest Singleton => singleton ??= new GetUUIDRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetUUIDResponse : IResponse, IDeserializableRos1<GetUUIDResponse>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        /// Constructor for empty message.
        public GetUUIDResponse()
        {
            Uuid = "";
        }
        
        /// Explicit constructor.
        public GetUUIDResponse(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// Constructor with buffer.
        public GetUUIDResponse(ref ReadBuffer b)
        {
            b.DeserializeString(out Uuid);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetUUIDResponse(ref b);
        
        public GetUUIDResponse RosDeserialize(ref ReadBuffer b) => new GetUUIDResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Uuid);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
