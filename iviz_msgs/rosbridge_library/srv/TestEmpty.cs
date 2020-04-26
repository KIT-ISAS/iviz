using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestEmpty : IService
    {
        /// <summary> Request message. </summary>
        public TestEmptyRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public TestEmptyResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestEmpty()
        {
            Request = new TestEmptyRequest();
            Response = new TestEmptyResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestEmpty(TestEmptyRequest request)
        {
            Request = request;
            Response = new TestEmptyResponse();
        }
        
        public IService Create() => new TestEmpty();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosbridge_library/TestEmpty";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
    }

    public sealed class TestEmptyRequest : Internal.EmptyRequest
    {
    }

    public sealed class TestEmptyResponse : Internal.EmptyResponse
    {
    }
}
