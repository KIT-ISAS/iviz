using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class GetUUID : IService
    {
        /// Request message.
        [DataMember] public GetUUIDRequest Request;
        
        /// Response message.
        [DataMember] public GetUUIDResponse Response;
        
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
    public sealed class GetUUIDRequest : IRequest<GetUUID, GetUUIDResponse>, IDeserializable<GetUUIDRequest>
    {
    
        public GetUUIDRequest()
        {
        }
        
        public GetUUIDRequest(ref ReadBuffer b)
        {
        }
        
        public GetUUIDRequest(ref ReadBuffer2 b)
        {
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetUUIDRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetUUIDRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetUUIDRequest? singleton;
        public static GetUUIDRequest Singleton => singleton ??= new GetUUIDRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public void AddRos2MessageLength(ref int c) { }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetUUIDResponse : IResponse, IDeserializable<GetUUIDResponse>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        public GetUUIDResponse()
        {
            Uuid = "";
        }
        
        public GetUUIDResponse(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        public GetUUIDResponse(ref ReadBuffer b)
        {
            b.DeserializeString(out Uuid);
        }
        
        public GetUUIDResponse(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Uuid);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetUUIDResponse(ref b);
        
        public GetUUIDResponse RosDeserialize(ref ReadBuffer b) => new GetUUIDResponse(ref b);
        
        public GetUUIDResponse RosDeserialize(ref ReadBuffer2 b) => new GetUUIDResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Uuid);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Uuid);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
