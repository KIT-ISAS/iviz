namespace Iviz.Msgs.rosbridge_library
{
    public class SendBytes : IService
    {
        public sealed class Request : IRequest
        {
            public long count;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out count, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(count, ref ptr, end);
            }
        
            public int GetLength() => 8;
        }

        public sealed class Response : IResponse
        {
            public string data;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                data = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out data, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(data, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += data.Length;
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosbridge_library/SendBytes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "d875457256decc7436099d9d612ebf8a";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public SendBytes()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public SendBytes(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new SendBytes();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
