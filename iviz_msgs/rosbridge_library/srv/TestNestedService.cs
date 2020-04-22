namespace Iviz.Msgs.rosbridge_library
{
    public class TestNestedService : IService
    {
        public sealed class Request : IRequest
        {
            //request definition
            public geometry_msgs.Pose pose;
        
            public int GetLength() => 56;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                pose.Deserialize(ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                pose.Serialize(ref ptr, end);
            }
        
            public Response Call(IServiceCaller caller)
            {
                TestNestedService s = new TestNestedService(this);
                caller.Call(s);
                return s.response;
            }
        }

        public sealed class Response : IResponse
        {
            //response definition
            public std_msgs.Float64 data;
        
            public int GetLength() => 8;
        
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
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "rosbridge_library/TestNestedService";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "063d2b71e58b5225a457d4ee09dab6f6";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACr2SsU4DMQyGd0t5B0tdubIgBiQGFpgqtYIdRT2njcTF19gVlKfHuba5FgYWaIbIduz4" +
            "/5xMMm22JIothZiiRk4OVsQdad69drKS6zkLYW+bg6ZpHEwySc/Jgqc1ou0+/fGNvd7eYOvVO3Bw/8fL" +
            "wez56Q5/ajRl+ICZetNHSX3RhRwG6RgThkyE0vslXeGSuxJuD+cDA/pkfo7H2inCnGPSmgCLrVfKabh3" +
            "zLsco4kpkC/rKEZg7WMS1DWNCIbjzSuqz4ghHF7lo1q7an1eimBR51cx6nOJjf90quf6i7cZpx84d1P4" +
            "Bepovf8f3vdP72rT/e//AuElY/VeAwAA";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public TestNestedService()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestNestedService(Request request)
        {
            this.request = request;
        }
        
        public IResponse CreateResponse() => new Response();
        
        public IRequest GetRequest() => request;
        
        public void SetResponse(IResponse response)
        {
            this.response = (Response)response;
        }
    }

}
