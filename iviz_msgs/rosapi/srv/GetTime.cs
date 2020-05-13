using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
    public sealed class GetTime : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetTimeRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetTimeResponse Response { get; set; }
        
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/GetTime";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "556a4fb76023a469987922359d08a844";
    }

    public sealed class GetTimeRequest : Internal.EmptyRequest
    {
    }

    public sealed class GetTimeResponse : IResponse
    {
        [DataMember] public time time { get; set; }
    
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
            this.time = b.Deserialize<time>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new GetTimeResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.time);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 8;
    }
}
