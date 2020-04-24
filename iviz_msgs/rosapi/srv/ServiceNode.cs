namespace Iviz.Msgs.rosapi
{
    public class ServiceNode : IService
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
            public string node;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                node = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out node, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(node, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += node.Length;
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/ServiceNode";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "bd2a0a45fd7a73a86c8d6051d5a6db8a";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public ServiceNode()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServiceNode(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new ServiceNode();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
