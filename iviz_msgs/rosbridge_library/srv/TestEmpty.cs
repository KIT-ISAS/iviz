using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestEmpty")]
    public sealed class TestEmpty : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TestEmptyRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TestEmptyResponse Response { get; set; }
        
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
        
        IService IService.Create() => new TestEmpty();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TestEmptyRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TestEmptyResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestEmpty";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
    }

    public sealed class TestEmptyRequest : Internal.EmptyRequest
    {
    }

    public sealed class TestEmptyResponse : Internal.EmptyResponse
    {
    }
}
