namespace Iviz.Msgs.rosapi
{
    public class GetParam : IService
    {
        public sealed class Request : IRequest
        {
            public string name;
            public string @default;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                name = "";
                @default = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out name, ref ptr, end);
                BuiltIns.Deserialize(out @default, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(name, ref ptr, end);
                BuiltIns.Serialize(@default, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 8;
                size += name.Length;
                size += @default.Length;
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            public string value;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                value = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out value, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(value, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += value.Length;
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/GetParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "e36fd90759dbac1c5159140a7fa8c644";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public GetParam()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetParam(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new GetParam();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
