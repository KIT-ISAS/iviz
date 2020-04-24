namespace Iviz.Msgs.rosapi
{
    public class ServiceProviders : IService
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
            public string[] providers;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                providers = System.Array.Empty<string>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out providers, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(providers, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 8;
                for (int i = 0; i < providers.Length; i++)
                {
                    size += providers[i].Length;
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/ServiceProviders";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "f30b41d5e347454ae5483ee95eef5cc6";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public ServiceProviders()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServiceProviders(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new ServiceProviders();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
