using System.Runtime.Serialization;

namespace Iviz.Msgs.diagnostic_msgs
{
    public sealed class SelfTest : IService
    {
        /// <summary> Request message. </summary>
        public SelfTestRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public SelfTestResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SelfTest()
        {
            Request = new SelfTestRequest();
            Response = new SelfTestResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SelfTest(SelfTestRequest request)
        {
            Request = request;
            Response = new SelfTestResponse();
        }
        
        public IService Create() => new SelfTest();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "diagnostic_msgs/SelfTest";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "ac21b1bab7ab17546986536c22eb34e9";
    }

    public sealed class SelfTestRequest : Internal.EmptyRequest
    {
    }

    public sealed class SelfTestResponse : IResponse
    {
        public string id;
        public byte passed;
        public DiagnosticStatus[] status;
    
        /// <summary> Constructor for empty message. </summary>
        public SelfTestResponse()
        {
            id = "";
            status = System.Array.Empty<DiagnosticStatus>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out id, ref ptr, end);
            BuiltIns.Deserialize(out passed, ref ptr, end);
            BuiltIns.DeserializeArray(out status, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(id, ref ptr, end);
            BuiltIns.Serialize(passed, ref ptr, end);
            BuiltIns.SerializeArray(status, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 9;
                size += BuiltIns.UTF8.GetByteCount(id);
                for (int i = 0; i < status.Length; i++)
                {
                    size += status[i].RosMessageLength;
                }
                return size;
            }
        }
    }
}
