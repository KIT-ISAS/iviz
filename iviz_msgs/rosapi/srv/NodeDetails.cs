namespace Iviz.Msgs.rosapi
{
    public class NodeDetails : IService
    {
        public sealed class Request : IRequest
        {
            public string node;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
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

        public sealed class Response : IResponse
        {
            public string[] subscribing;
            public string[] publishing;
            public string[] services;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                subscribing = System.Array.Empty<string>();
                publishing = System.Array.Empty<string>();
                services = System.Array.Empty<string>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out subscribing, ref ptr, end, 0);
                BuiltIns.Deserialize(out publishing, ref ptr, end, 0);
                BuiltIns.Deserialize(out services, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(subscribing, ref ptr, end, 0);
                BuiltIns.Serialize(publishing, ref ptr, end, 0);
                BuiltIns.Serialize(services, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 24;
                for (int i = 0; i < subscribing.Length; i++)
                {
                    size += subscribing[i].Length;
                }
                for (int i = 0; i < publishing.Length; i++)
                {
                    size += publishing[i].Length;
                }
                for (int i = 0; i < services.Length; i++)
                {
                    size += services[i].Length;
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/NodeDetails";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "e1d0ced5ab8d5edb5fc09c98eb1d46f6";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public NodeDetails()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public NodeDetails(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new NodeDetails();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
