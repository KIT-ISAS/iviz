using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/GetTime")]
    public sealed class GetTime : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetTimeRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetTimeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetTime()
        {
            Request = GetTimeRequest.Singleton;
            Response = new GetTimeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/GetTime";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "556a4fb76023a469987922359d08a844";
    }

    [DataContract]
    public sealed class GetTimeRequest : IRequest<GetTime, GetTimeResponse>, IDeserializable<GetTimeRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public GetTimeRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetTimeRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        GetTimeRequest IDeserializable<GetTimeRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly GetTimeRequest Singleton = new();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class GetTimeResponse : IResponse, IDeserializable<GetTimeResponse>
    {
        [DataMember (Name = "time")] public time Time { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetTimeResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetTimeResponse(time Time)
        {
            this.Time = Time;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetTimeResponse(ref Buffer b)
        {
            Time = b.Deserialize<time>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetTimeResponse(ref b);
        }
        
        GetTimeResponse IDeserializable<GetTimeResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Time);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    }
}
