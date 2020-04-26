using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestRequestAndResponse : IService
    {
        /// <summary> Request message. </summary>
        public TestRequestAndResponseRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public TestRequestAndResponseResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestRequestAndResponse()
        {
            Request = new TestRequestAndResponseRequest();
            Response = new TestRequestAndResponseResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestRequestAndResponse(TestRequestAndResponseRequest request)
        {
            Request = request;
            Response = new TestRequestAndResponseResponse();
        }
        
        public IService Create() => new TestRequestAndResponse();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosbridge_library/TestRequestAndResponse";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "491d316f183df11876531749005b31d1";
    }

    public sealed class TestRequestAndResponseRequest : IRequest
    {
        public int data;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 4;
    }

    public sealed class TestRequestAndResponseResponse : IResponse
    {
        public int data;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 4;
    }
}
