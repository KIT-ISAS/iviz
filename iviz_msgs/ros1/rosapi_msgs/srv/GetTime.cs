using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class GetTime : IService<GetTimeRequest, GetTimeResponse>
    {
        /// Request message.
        [DataMember] public GetTimeRequest Request;
        
        /// Response message.
        [DataMember] public GetTimeResponse Response;
        
        /// Empty constructor.
        public GetTime()
        {
            Request = GetTimeRequest.Singleton;
            Response = new GetTimeResponse();
        }
        
        /// Setter constructor.
        public GetTime(GetTimeRequest request)
        {
            Request = request;
            Response = new GetTimeResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetTimeRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetTimeResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/GetTime";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "556a4fb76023a469987922359d08a844";
        
        public IService Generate() => new GetTime();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetTimeRequest : IRequest<GetTime, GetTimeResponse>, IDeserializable<GetTimeRequest>
    {
    
        public GetTimeRequest()
        {
        }
        
        public GetTimeRequest(ref ReadBuffer b)
        {
        }
        
        public GetTimeRequest(ref ReadBuffer2 b)
        {
        }
        
        public GetTimeRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetTimeRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetTimeRequest? singleton;
        public static GetTimeRequest Singleton => singleton ??= new GetTimeRequest();
    
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
    public sealed class GetTimeResponse : IResponse, IDeserializable<GetTimeResponse>
    {
        [DataMember (Name = "time")] public time Time;
    
        public GetTimeResponse()
        {
        }
        
        public GetTimeResponse(time Time)
        {
            this.Time = Time;
        }
        
        public GetTimeResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Time);
        }
        
        public GetTimeResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Time);
        }
        
        public GetTimeResponse RosDeserialize(ref ReadBuffer b) => new GetTimeResponse(ref b);
        
        public GetTimeResponse RosDeserialize(ref ReadBuffer2 b) => new GetTimeResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Time);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Time);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 8;
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 8; // Time
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
