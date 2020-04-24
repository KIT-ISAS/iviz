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
        
            public int GetLength()
            {
                int size = 8;
                size += file_path.Length;
                size += topic_name.Length;
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            
            // True if file processing was successful.
            public bool success;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out success, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(success, ref ptr, end);
            }
        
            public int GetLength() => 1;
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "grid_map_msgs/ProcessFile";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "03f389710f49a6dd2a8b447bb2850cd6";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public ProcessFile()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public ProcessFile(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new ProcessFile();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
