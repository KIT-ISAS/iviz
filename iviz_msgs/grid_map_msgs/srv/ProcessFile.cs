using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract (Name = "grid_map_msgs/ProcessFile")]
    public sealed class ProcessFile : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ProcessFileRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ProcessFileResponse Response { get; set; }
        
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
        
        IService IService.Create() => new ProcessFile();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ProcessFileRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ProcessFileResponse)value;
        }
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "grid_map_msgs/ProcessFile";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "03f389710f49a6dd2a8b447bb2850cd6";
    }

    public sealed class ProcessFileRequest : IRequest, IDeserializable<ProcessFileRequest>
    {
        // Absolute file path.
        [DataMember (Name = "file_path")] public string FilePath { get; set; }
        // For ROS bags: topic name that should be processed (optional).
        [DataMember (Name = "topic_name")] public string TopicName { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ProcessFileRequest()
        {
            FilePath = "";
            TopicName = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ProcessFileRequest(string FilePath, string TopicName)
        {
            this.FilePath = FilePath;
            this.TopicName = TopicName;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ProcessFileRequest(ref Buffer b)
        {
            FilePath = b.DeserializeString();
            TopicName = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ProcessFileRequest(ref b);
        }
        
        ProcessFileRequest IDeserializable<ProcessFileRequest>.RosDeserialize(ref Buffer b)
        {
            return new ProcessFileRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(FilePath);
            b.Serialize(TopicName);
        }
        
        public void RosValidate()
        {
            if (FilePath is null) throw new System.NullReferenceException(nameof(FilePath));
            if (TopicName is null) throw new System.NullReferenceException(nameof(TopicName));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(FilePath);
                size += BuiltIns.UTF8.GetByteCount(TopicName);
                return size;
            }
        }
    }

    public sealed class ProcessFileResponse : IResponse, IDeserializable<ProcessFileResponse>
    {
        // True if file processing was successful.
        [DataMember (Name = "success")] public bool Success { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ProcessFileResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public ProcessFileResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ProcessFileResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ProcessFileResponse(ref b);
        }
        
        ProcessFileResponse IDeserializable<ProcessFileResponse>.RosDeserialize(ref Buffer b)
        {
            return new ProcessFileResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 1;
    }
}
