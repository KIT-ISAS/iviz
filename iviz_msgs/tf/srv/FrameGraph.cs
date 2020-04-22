namespace Iviz.Msgs.tf
{
    public class FrameGraph : IService
    {
        public sealed class Request : IRequest
        {
        
            /// <summary> Full ROS name of the parent service. </summary>
            public const string MessageType = FrameGraph.MessageType;
        
            /// <summary> MD5 hash of a compact representation of the parent service. </summary>
            public const string Md5Sum = FrameGraph.Md5Sum;
        
            /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
            public const string DependenciesBase64 = FrameGraph.DependenciesBase64;
        
            public IResponse CreateResponse() => new Response();
        
            public bool IsResponseType<T>()
            {
                return typeof(T).Equals(typeof(Response));
            }
        
            public int GetLength() => 0;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        }

        public sealed class Response : IResponse
        {
            public string dot_graph;
        
            public int GetLength()
            {
                int size = 4;
                size += dot_graph.Length;
                return size;
            }
        
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
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string MessageType = "tf/FrameGraph";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "c4af9ac907e58e906eb0b6e3c58478c0";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACtPV1eUqLinKzEtXSMkviU8vSizI4OLlAgCI5a/GFwAAAA==";
            
    }

}
