using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetTime : IService
    {
        /// Request message.
        [DataMember] public GetTimeRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetTimeResponse Response { get; set; }
        
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
        
        IService IService.Create() => new GetTime();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/GetTime";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "556a4fb76023a469987922359d08a844";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetTimeRequest : IRequest<GetTime, GetTimeResponse>, IDeserializable<GetTimeRequest>
    {
    
        /// Constructor for empty message.
        public GetTimeRequest()
        {
        }
        
        /// Constructor with buffer.
        internal GetTimeRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        GetTimeRequest IDeserializable<GetTimeRequest>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly GetTimeRequest Singleton = new GetTimeRequest();
    
        public void RosSerialize(ref Buffer b)
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
    public sealed class GetTimeResponse : IResponse, IDeserializable<GetTimeResponse>
    {
        [DataMember (Name = "time")] public time Time;
    
        /// Constructor for empty message.
        public GetTimeResponse()
        {
        }
        
        /// Explicit constructor.
        public GetTimeResponse(time Time)
        {
            this.Time = Time;
        }
        
        /// Constructor with buffer.
        internal GetTimeResponse(ref Buffer b)
        {
            Time = b.Deserialize<time>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetTimeResponse(ref b);
        
        GetTimeResponse IDeserializable<GetTimeResponse>.RosDeserialize(ref Buffer b) => new GetTimeResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Time);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
