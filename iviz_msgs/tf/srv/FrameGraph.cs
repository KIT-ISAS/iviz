namespace Iviz.Msgs.tf
{
    public class FrameGraph : IService
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
            public string dot_graph;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                dot_graph = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out dot_graph, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(dot_graph, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += dot_graph.Length;
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "tf/FrameGraph";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "c4af9ac907e58e906eb0b6e3c58478c0";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public FrameGraph()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public FrameGraph(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new FrameGraph();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
