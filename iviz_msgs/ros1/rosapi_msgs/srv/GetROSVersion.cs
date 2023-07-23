using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class GetROSVersion : IService<GetROSVersionRequest, GetROSVersionResponse>
    {
        /// Request message.
        [DataMember] public GetROSVersionRequest Request;
        
        /// Response message.
        [DataMember] public GetROSVersionResponse Response;
        
        /// Empty constructor.
        public GetROSVersion()
        {
            Request = GetROSVersionRequest.Singleton;
            Response = new GetROSVersionResponse();
        }
        
        /// Setter constructor.
        public GetROSVersion(GetROSVersionRequest request)
        {
            Request = request;
            Response = new GetROSVersionResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetROSVersionRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetROSVersionResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/GetROSVersion";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "e18b8c901d19ae7b29f35f226cb5b4c4";
        
        public IService Generate() => new GetROSVersion();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetROSVersionRequest : IRequest<GetROSVersion, GetROSVersionResponse>, IDeserializable<GetROSVersionRequest>
    {
    
        public GetROSVersionRequest()
        {
        }
        
        public GetROSVersionRequest(ref ReadBuffer b)
        {
        }
        
        public GetROSVersionRequest(ref ReadBuffer2 b)
        {
        }
        
        public GetROSVersionRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetROSVersionRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetROSVersionRequest? singleton;
        public static GetROSVersionRequest Singleton => singleton ??= new GetROSVersionRequest();
    
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
    public sealed class GetROSVersionResponse : IResponse, IDeserializable<GetROSVersionResponse>
    {
        [DataMember (Name = "version")] public byte Version;
        [DataMember (Name = "distro")] public string Distro;
    
        public GetROSVersionResponse()
        {
            Distro = "";
        }
        
        public GetROSVersionResponse(byte Version, string Distro)
        {
            this.Version = Version;
            this.Distro = Distro;
        }
        
        public GetROSVersionResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Version);
            Distro = b.DeserializeString();
        }
        
        public GetROSVersionResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Version);
            b.Align4();
            Distro = b.DeserializeString();
        }
        
        public GetROSVersionResponse RosDeserialize(ref ReadBuffer b) => new GetROSVersionResponse(ref b);
        
        public GetROSVersionResponse RosDeserialize(ref ReadBuffer2 b) => new GetROSVersionResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Version);
            b.Serialize(Distro);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Version);
            b.Align4();
            b.Serialize(Distro);
        }
        
        public void RosValidate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 5;
                size += WriteBuffer.GetStringSize(Distro);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Version
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Distro);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
