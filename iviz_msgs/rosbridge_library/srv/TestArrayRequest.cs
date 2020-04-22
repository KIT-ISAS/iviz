namespace Iviz.Msgs.rosbridge_library
{
    public class TestArrayRequest : IService
    {
        public sealed class Request : IRequest
        {
            public int[] @int;
        
            public int GetLength()
            {
                int size = 4;
                size += 4 * @int.Length;
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                @int = System.Array.Empty<0>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out @int, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(@int, ref ptr, end, 0);
            }
        
            public Response Call(IServiceCaller caller)
            {
                TestArrayRequest s = new TestArrayRequest(this);
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
        public const string ServiceType = "rosbridge_library/TestArrayRequest";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "3d7cfb7e4aa0844868966efa8a264398";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACsvMKzE2io5VyMwr4eXS1dXl5QIAVi7OQxIAAAA=";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public TestArrayRequest()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestArrayRequest(Request request)
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
