namespace Iviz.Msgs.rosapi
{
    public class ServiceHost : IService
    {
        public sealed class Request : IRequest
        {
            public string service;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                service = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out service, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(service, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += service.Length;
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            public string host;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                host = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out host, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(host, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += host.Length;
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/ServiceHost";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "a1b60006f8ee69637c856c94dd192f5a";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public ServiceHost()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServiceHost(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new ServiceHost();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
