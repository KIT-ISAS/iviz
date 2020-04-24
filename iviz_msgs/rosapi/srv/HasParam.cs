namespace Iviz.Msgs.rosapi
{
    public class HasParam : IService
    {
        public sealed class Request : IRequest
        {
            public string name;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                name = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out name, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(name, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += name.Length;
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            public bool exists;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out exists, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(exists, ref ptr, end);
            }
        
            public int GetLength() => 1;
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/HasParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "ed3df286bd6dff9b961770f577454ea9";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public HasParam()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public HasParam(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new HasParam();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
