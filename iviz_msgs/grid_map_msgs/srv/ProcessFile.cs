namespace Iviz.Msgs.grid_map_msgs
{
    public class ProcessFile : IService
    {
        public sealed class Request : IRequest
        {
            // Absolute file path.
            public string file_path;
            
            // For ROS bags: topic name that should be processed (optional).
            public string topic_name;
            
        
            public int GetLength()
            {
                int size = 8;
                size += file_path.Length;
                size += topic_name.Length;
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                file_path = "";
                topic_name = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out file_path, ref ptr, end);
                BuiltIns.Deserialize(out topic_name, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(file_path, ref ptr, end);
                BuiltIns.Serialize(topic_name, ref ptr, end);
            }
        }

        public sealed class Response : IResponse
        {
            
            // True if file processing was successful.
            public bool success;
        
            public int GetLength() => 1;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out success, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(success, ref ptr, end);
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string MessageType = "grid_map_msgs/ProcessFile";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "03f389710f49a6dd2a8b447bb2850cd6";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACj2NOw7CMBAF+5W4w5PSQJEcgI6GFgnoI9uxY0smG3nX4vrg8Cl39Ga2w8kK56oeIWWP" +
            "1WgcSLSkZd7I2AhRhzMXXC83WDPLEcprcljMw0OjUUjkmifYd6Gw8yJ+wp5XTbyYfPgXN21sGlHf9617" +
            "L9Ujhe/7j9ymTyOQ6toZah7IMucfoB29AHvJfdW5AAAA";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public ProcessFile()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public ProcessFile(Request request)
        {
            this.request = request;
        }
        
        public IResponse CreateResponse() => new Response();
        
        public IRequest GetRequest() => request;
        
        public void SetResponse(IResponse response)
        {
            this.response = (Response)response;
        }
    }

}
