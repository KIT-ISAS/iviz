namespace Iviz.Msgs.rosbridge_library
{
    public class TestArrayRequest : IService
    {
        public sealed class Request : IRequest
        {
            public int[] @int;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                @int = System.Array.Empty<int>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out @int, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(@int, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += 4 * @int.Length;
                return size;
            }
        }

        public sealed class Response : IResponse
        {
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        
            public int GetLength() => 0;
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosbridge_library/TestArrayRequest";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "3d7cfb7e4aa0844868966efa8a264398";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public TestArrayRequest()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestArrayRequest(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new TestArrayRequest();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
