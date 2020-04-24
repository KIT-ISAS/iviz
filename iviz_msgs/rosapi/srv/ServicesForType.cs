namespace Iviz.Msgs.rosapi
{
    public class ServicesForType : IService
    {
        public sealed class Request : IRequest
        {
            public string type;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                type = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out type, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(type, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += type.Length;
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            public string[] services;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                services = System.Array.Empty<string>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out services, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(services, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 8;
                for (int i = 0; i < services.Length; i++)
                {
                    size += services[i].Length;
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/ServicesForType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "93e9fe8ae5a9136008e260fe510bd2b0";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public ServicesForType()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServicesForType(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new ServicesForType();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
