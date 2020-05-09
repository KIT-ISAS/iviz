using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class GetTime : IService
    {
        /// <summary> Request message. </summary>
        public GetTimeRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public GetTimeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetTime()
        {
            Request = new GetTimeRequest();
            Response = new GetTimeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetTime(GetTimeRequest request)
        {
            Request = request;
            Response = new GetTimeResponse();
        }
        
        public IService Create() => new GetTime();
        
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
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/GetTime";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "556a4fb76023a469987922359d08a844";
    }

    public sealed class GetTimeRequest : Internal.EmptyRequest
    {
    }

    public sealed class GetTimeResponse : IResponse
    {
        public time time { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetTimeResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetTimeResponse(time time)
        {
            this.time = time;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetTimeResponse(Buffer b)
        {
            this.time = BuiltIns.DeserializeStruct<time>(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetTimeResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.time, b);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 8;
    }
}
