using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/GetModelFile")]
    public sealed class GetModelFile : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetModelFileRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetModelFileResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetModelFile()
        {
            Request = new GetModelFileRequest();
            Response = new GetModelFileResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetModelFile(GetModelFileRequest request)
        {
            Request = request;
            Response = new GetModelFileResponse();
        }
        
        IService IService.Create() => new GetModelFile();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetModelFileRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetModelFileResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/GetModelFile";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "08088c7144705ee9cf37b287c931476d";
    }

    public sealed class GetModelFileRequest : IRequest
    {
        // Retrieves a file
        [DataMember (Name = "uri")] public string Uri { get; set; } // Uri of the file. Example: package://some_package/file.dae
    
        /// <summary> Constructor for empty message. </summary>
        public GetModelFileRequest()
        {
            Uri = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetModelFileRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetModelFileRequest(Buffer b)
        {
            Uri = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetModelFileRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            if (Uri is null) throw new System.NullReferenceException(nameof(Uri));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Uri);
                return size;
            }
        }
    }

    public sealed class GetModelFileResponse : IResponse
    {
        [DataMember (Name = "success")] public bool Success { get; set; } // Whether the retrieval succeeded
        [DataMember (Name = "bytes")] public byte[] Bytes { get; set; } // The content of the file as byte array
        [DataMember (Name = "message")] public string Message { get; set; } // An error message if success is false
    
        /// <summary> Constructor for empty message. </summary>
        public GetModelFileResponse()
        {
            Bytes = System.Array.Empty<byte>();
            Message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetModelFileResponse(bool Success, byte[] Bytes, string Message)
        {
            this.Success = Success;
            this.Bytes = Bytes;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetModelFileResponse(Buffer b)
        {
            Success = b.Deserialize<bool>();
            Bytes = b.DeserializeStructArray<byte>();
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetModelFileResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Success);
            b.SerializeStructArray(Bytes, 0);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Bytes is null) throw new System.NullReferenceException(nameof(Bytes));
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 9;
                size += 1 * Bytes.Length;
                size += BuiltIns.UTF8.GetByteCount(Message);
                return size;
            }
        }
    }
}
