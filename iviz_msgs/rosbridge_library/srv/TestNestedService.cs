using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestNestedService : IService
    {
        /// <summary> Request message. </summary>
        public TestNestedServiceRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public TestNestedServiceResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestNestedService()
        {
            Request = new TestNestedServiceRequest();
            Response = new TestNestedServiceResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestNestedService(TestNestedServiceRequest request)
        {
            Request = request;
            Response = new TestNestedServiceResponse();
        }
        
        public IService Create() => new TestNestedService();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosbridge_library/TestNestedService";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "063d2b71e58b5225a457d4ee09dab6f6";
    }

    public sealed class TestNestedServiceRequest : IRequest
    {
        //request definition
        public geometry_msgs.Pose pose;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            pose.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            pose.Serialize(ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 56;
    }

    public sealed class TestNestedServiceResponse : IResponse
    {
        //response definition
        public std_msgs.Float64 data;
    
        /// <summary> Constructor for empty message. </summary>
        public TestNestedServiceResponse()
        {
            data = new std_msgs.Float64();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            data.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            data.Serialize(ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 8;
    }
}
