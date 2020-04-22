namespace Iviz.Msgs.rosbridge_library
{
    public class TestEmpty : IService
    {
        public sealed class Request : IRequest
        {
        
            public int GetLength() => 0;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        
            public Response Call(IServiceCaller caller)
            {
                TestEmpty s = new TestEmpty(this);
                caller.Call(s);
                return s.response;
            }
        }

        public sealed class Response : IResponse
        {
        
            public int GetLength() => 0;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "rosbridge_library/TestEmpty";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "d41d8cd98f00b204e9800998ecf8427e";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACtPV1eXlAgCrKcXIBQAAAA==";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public TestEmpty()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestEmpty(Request request)
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
