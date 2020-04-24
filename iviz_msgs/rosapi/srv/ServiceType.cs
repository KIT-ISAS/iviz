namespace Iviz.Msgs.rosapi
{
    public class ServiceType : IService
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
            public string type;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
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
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/ServiceType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "0e24a2dcdf70e483afc092a35a1f15f7";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public ServiceType()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServiceType(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new ServiceType();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
