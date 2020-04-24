namespace Iviz.Msgs.rosbridge_library
{
    public class TestNestedService : IService
    {
        public sealed class Request : IRequest
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
        
            public int GetLength() => 56;
        }

        public sealed class Response : IResponse
        {
            //response definition
            public std_msgs.Float64 data;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
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
        
            public int GetLength() => 8;
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosbridge_library/TestNestedService";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "063d2b71e58b5225a457d4ee09dab6f6";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public TestNestedService()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestNestedService(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new TestNestedService();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
