using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class ProcessFile : IService
    {
        /// Request message.
        [DataMember] public ProcessFileRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ProcessFileResponse Response { get; set; }
        
        /// Empty constructor.
        public ProcessFile()
        {
            Request = new ProcessFileRequest();
            Response = new ProcessFileResponse();
        }
        
        /// Setter constructor.
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "grid_map_msgs/ProcessFile";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "03f389710f49a6dd2a8b447bb2850cd6";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ProcessFileRequest : IRequest<ProcessFile, ProcessFileResponse>, IDeserializable<ProcessFileRequest>
    {
        // Absolute file path.
        [DataMember (Name = "file_path")] public string FilePath;
        // For ROS bags: topic name that should be processed (optional).
        [DataMember (Name = "topic_name")] public string TopicName;
    
        /// Constructor for empty message.
        public ProcessFileRequest()
        {
            FilePath = "";
            TopicName = "";
        }
        
        /// Explicit constructor.
        public ProcessFileRequest(string FilePath, string TopicName)
        {
            this.FilePath = FilePath;
            this.TopicName = TopicName;
        }
        
        /// Constructor with buffer.
        public ProcessFileRequest(ref ReadBuffer b)
        {
            FilePath = b.DeserializeString();
            TopicName = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ProcessFileRequest(ref b);
        
        public ProcessFileRequest RosDeserialize(ref ReadBuffer b) => new ProcessFileRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(FilePath);
            b.Serialize(TopicName);
        }
        
        public void RosValidate()
        {
            if (FilePath is null) throw new System.NullReferenceException(nameof(FilePath));
            if (TopicName is null) throw new System.NullReferenceException(nameof(TopicName));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(FilePath) + BuiltIns.GetStringSize(TopicName);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ProcessFileResponse : IResponse, IDeserializable<ProcessFileResponse>
    {
        // True if file processing was successful.
        [DataMember (Name = "success")] public bool Success;
    
        /// Constructor for empty message.
        public ProcessFileResponse()
        {
        }
        
        /// Explicit constructor.
        public ProcessFileResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// Constructor with buffer.
        public ProcessFileResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ProcessFileResponse(ref b);
        
        public ProcessFileResponse RosDeserialize(ref ReadBuffer b) => new ProcessFileResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
