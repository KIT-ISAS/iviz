using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class GetUUID : IService<GetUUIDRequest, GetUUIDResponse>
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
        
        public IService Generate() => new GetUUID();
        
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
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
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
            Uuid = b.DeserializeString();
        }
        
        public GetUUIDResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Uuid = b.DeserializeString();
        }
        
        public GetUUIDResponse RosDeserialize(ref ReadBuffer b) => new GetUUIDResponse(ref b);
        
        public GetUUIDResponse RosDeserialize(ref ReadBuffer2 b) => new GetUUIDResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Uuid);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Uuid);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
