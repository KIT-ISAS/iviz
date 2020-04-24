namespace Iviz.Msgs.rosapi
{
    public class SetParam : IService
    {
        public sealed class Request : IRequest
        {
            public string name;
            public string value;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                name = "";
                value = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out name, ref ptr, end);
                BuiltIns.Deserialize(out value, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(name, ref ptr, end);
                BuiltIns.Serialize(value, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 8;
                size += name.Length;
                size += value.Length;
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
        public const string _ServiceType = "rosapi/SetParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "bc6ccc4a57f61779c8eaae61e9f422e0";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public SetParam()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetParam(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new SetParam();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
