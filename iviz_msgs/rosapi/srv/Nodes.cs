namespace Iviz.Msgs.rosapi
{
    public class Nodes : IService
    {
        public sealed class Request : IRequest
        {
            
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        
            public int GetLength() => 0;
        }

        public sealed class Response : IResponse
        {
            public string[] nodes;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                nodes = System.Array.Empty<string>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out nodes, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(nodes, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 8;
                for (int i = 0; i < nodes.Length; i++)
                {
                    size += nodes[i].Length;
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/Nodes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "3d07bfda1268b4f76b16b7ba8a82665d";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public Nodes()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public Nodes(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new Nodes();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
