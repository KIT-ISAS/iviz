namespace Iviz.Msgs.rosapi
{
    public class TopicsAndRawTypes : IService
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
            public string[] topics;
            public string[] types;
            public string[] typedefs_full_text;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                topics = System.Array.Empty<string>();
                types = System.Array.Empty<string>();
                typedefs_full_text = System.Array.Empty<string>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out topics, ref ptr, end, 0);
                BuiltIns.Deserialize(out types, ref ptr, end, 0);
                BuiltIns.Deserialize(out typedefs_full_text, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(topics, ref ptr, end, 0);
                BuiltIns.Serialize(types, ref ptr, end, 0);
                BuiltIns.Serialize(typedefs_full_text, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 24;
                for (int i = 0; i < topics.Length; i++)
                {
                    size += topics[i].Length;
                }
                for (int i = 0; i < types.Length; i++)
                {
                    size += types[i].Length;
                }
                for (int i = 0; i < typedefs_full_text.Length; i++)
                {
                    size += typedefs_full_text[i].Length;
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/TopicsAndRawTypes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "e1432466c8f64316723276ba07c59d12";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public TopicsAndRawTypes()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public TopicsAndRawTypes(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new TopicsAndRawTypes();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
