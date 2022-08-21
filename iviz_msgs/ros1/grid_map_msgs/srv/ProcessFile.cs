using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract]
    public sealed class ProcessFile : IService
    {
        /// Request message.
        [DataMember] public ProcessFileRequest Request;
        
        /// Response message.
        [DataMember] public ProcessFileResponse Response;
        
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
        
        public const string ServiceType = "grid_map_msgs/ProcessFile";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "03f389710f49a6dd2a8b447bb2850cd6";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ProcessFileRequest : IRequest<ProcessFile, ProcessFileResponse>, IDeserializable<ProcessFileRequest>
    {
        // Absolute file path.
        [DataMember (Name = "file_path")] public string FilePath;
        // For ROS bags: topic name that should be processed (optional).
        [DataMember (Name = "topic_name")] public string TopicName;
    
        public ProcessFileRequest()
        {
            FilePath = "";
            TopicName = "";
        }
        
        public ProcessFileRequest(string FilePath, string TopicName)
        {
            this.FilePath = FilePath;
            this.TopicName = TopicName;
        }
        
        public ProcessFileRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out FilePath);
            b.DeserializeString(out TopicName);
        }
        
        public ProcessFileRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out FilePath);
            b.Align4();
            b.DeserializeString(out TopicName);
        }
        
        public ProcessFileRequest RosDeserialize(ref ReadBuffer b) => new ProcessFileRequest(ref b);
        
        public ProcessFileRequest RosDeserialize(ref ReadBuffer2 b) => new ProcessFileRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(FilePath);
            b.Serialize(TopicName);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(FilePath);
            b.Serialize(TopicName);
        }
        
        public void RosValidate()
        {
            if (FilePath is null) BuiltIns.ThrowNullReference();
            if (TopicName is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + WriteBuffer.GetStringSize(FilePath) + WriteBuffer.GetStringSize(TopicName);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, FilePath);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, TopicName);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ProcessFileResponse : IResponse, IDeserializable<ProcessFileResponse>
    {
        // True if file processing was successful.
        [DataMember (Name = "success")] public bool Success;
    
        public ProcessFileResponse()
        {
        }
        
        public ProcessFileResponse(bool Success)
        {
            this.Success = Success;
        }
        
        public ProcessFileResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
        }
        
        public ProcessFileResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
        }
        
        public ProcessFileResponse RosDeserialize(ref ReadBuffer b) => new ProcessFileResponse(ref b);
        
        public ProcessFileResponse RosDeserialize(ref ReadBuffer2 b) => new ProcessFileResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 1;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Success
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}