using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestResponseOnly : IService
    {
        /// <summary> Request message. </summary>
        public TestResponseOnlyRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public TestResponseOnlyResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestResponseOnly()
        {
            Request = new TestResponseOnlyRequest();
            Response = new TestResponseOnlyResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestResponseOnly(TestResponseOnlyRequest request)
        {
            Request = request;
            Response = new TestResponseOnlyResponse();
        }
        
        public IService Create() => new TestResponseOnly();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosbridge_library/TestResponseOnly";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    }

    public sealed class TestResponseOnlyRequest : Internal.EmptyRequest
    {
    }

    public sealed class TestResponseOnlyResponse : IResponse
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
