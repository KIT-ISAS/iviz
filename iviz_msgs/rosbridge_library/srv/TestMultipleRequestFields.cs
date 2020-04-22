namespace Iviz.Msgs.rosbridge_library
{
    public class TestMultipleRequestFields : IService
    {
        public sealed class Request : IRequest
        {
            public int @int;
            public float @float;
            public string @string;
            public bool @bool;
        
            public int GetLength()
            {
                int size = 13;
                size += @string.Length;
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                @string = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out @int, ref ptr, end);
                BuiltIns.Deserialize(out @float, ref ptr, end);
                BuiltIns.Deserialize(out @string, ref ptr, end);
                BuiltIns.Deserialize(out @bool, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(@int, ref ptr, end);
                BuiltIns.Serialize(@float, ref ptr, end);
                BuiltIns.Serialize(@string, ref ptr, end);
                BuiltIns.Serialize(@bool, ref ptr, end);
            }
        
            public Response Call(IServiceCaller caller)
            {
                TestMultipleRequestFields s = new TestMultipleRequestFields(this);
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
        public const string ServiceType = "rosbridge_library/TestMultipleRequestFields";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "6cce9fb727dd0f31d504d7d198a1f4ef";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACsvMKzE2UsjMK+HlSsvJTwRxwDQvV3FJUWZeugKE4uVKys/PUQARvFy6urq8XACZOOXH" +
            "OQAAAA==";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public TestMultipleRequestFields()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestMultipleRequestFields(Request request)
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
