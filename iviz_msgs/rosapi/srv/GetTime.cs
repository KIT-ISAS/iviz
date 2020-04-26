using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class GetTime : IService
    {
        /// <summary> Request message. </summary>
        public GetTimeRequest Request { get; }
        
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
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/GetTime";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "556a4fb76023a469987922359d08a844";
    }

    public sealed class GetTimeRequest : Internal.EmptyRequest
    {
    }

    public sealed class GetTimeResponse : IResponse
    {
        public time time;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out time, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(time, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 8;
    }
}
