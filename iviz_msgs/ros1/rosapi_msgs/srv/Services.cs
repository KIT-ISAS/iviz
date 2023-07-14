using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class Services : IService<ServicesRequest, ServicesResponse>
    {
        /// Request message.
        [DataMember] public ServicesRequest Request;
        
        /// Response message.
        [DataMember] public ServicesResponse Response;
        
        /// Empty constructor.
        public Services()
        {
            Request = ServicesRequest.Singleton;
            Response = new ServicesResponse();
        }
        
        /// Setter constructor.
        public Services(ServicesRequest request)
        {
            Request = request;
            Response = new ServicesResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServicesRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServicesResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/Services";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "e44a7e7bcb900acadbcc28b132378f0c";
        
        public IService Generate() => new Services();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServicesRequest : IRequest<Services, ServicesResponse>, IDeserializable<ServicesRequest>
    {
    
        public ServicesRequest()
        {
        }
        
        public ServicesRequest(ref ReadBuffer b)
        {
        }
        
        public ServicesRequest(ref ReadBuffer2 b)
        {
        }
        
        public ServicesRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public ServicesRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static ServicesRequest? singleton;
        public static ServicesRequest Singleton => singleton ??= new ServicesRequest();
    
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
    public sealed class ServicesResponse : IResponse, IDeserializable<ServicesResponse>
    {
        [DataMember (Name = "services")] public string[] Services_;
    
        public ServicesResponse()
        {
            Services_ = EmptyArray<string>.Value;
        }
        
        public ServicesResponse(string[] Services_)
        {
            this.Services_ = Services_;
        }
        
        public ServicesResponse(ref ReadBuffer b)
        {
            Services_ = b.DeserializeStringArray();
        }
        
        public ServicesResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Services_ = b.DeserializeStringArray();
        }
        
        public ServicesResponse RosDeserialize(ref ReadBuffer b) => new ServicesResponse(ref b);
        
        public ServicesResponse RosDeserialize(ref ReadBuffer2 b) => new ServicesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Services_.Length);
            b.SerializeArray(Services_);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Services_.Length);
            b.SerializeArray(Services_);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Services_, nameof(Services_));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Services_);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Services_);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
