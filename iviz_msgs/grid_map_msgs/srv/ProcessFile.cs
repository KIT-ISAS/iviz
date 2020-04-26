using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class ProcessFile : IService
    {
        /// <summary> Request message. </summary>
        public ProcessFileRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public ProcessFileResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ProcessFile()
        {
            Request = new ProcessFileRequest();
            Response = new ProcessFileResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ProcessFile(ProcessFileRequest request)
        {
            Request = request;
            Response = new ProcessFileResponse();
        }
        
        public IService Create() => new ProcessFile();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "grid_map_msgs/ProcessFile";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "03f389710f49a6dd2a8b447bb2850cd6";
    }

    public sealed class ProcessFileRequest : IRequest
    {
        // Absolute file path.
        public string file_path;
        
        // For ROS bags: topic name that should be processed (optional).
        public string topic_name;
        
    
        /// <summary> Constructor for empty message. </summary>
        public ProcessFileRequest()
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
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Encoding.UTF8.GetByteCount(file_path);
                size += Encoding.UTF8.GetByteCount(topic_name);
                return size;
            }
        }
    }

    public sealed class ProcessFileResponse : IResponse
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
    
        [IgnoreDataMember]
        public int RosMessageLength => 1;
    }
}
