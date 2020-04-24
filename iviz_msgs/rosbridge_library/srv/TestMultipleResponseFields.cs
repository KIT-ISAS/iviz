namespace Iviz.Msgs.rosbridge_library
{
    public class TestMultipleResponseFields : IService
    {
        public sealed class Request : IRequest
        {
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        
            public int GetLength() => 0;
        }

        public sealed class Response : IResponse
        {
            public int @int;
            public float @float;
            public string @string;
            public bool @bool;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
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
        
            public int GetLength()
            {
                int size = 13;
                size += @string.Length;
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "rosbridge_library/TestMultipleResponseFields";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "6cce9fb727dd0f31d504d7d198a1f4ef";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public TestMultipleResponseFields()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestMultipleResponseFields(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new TestMultipleResponseFields();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
