using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetFile : IService
    {
        /// Request message.
        [DataMember] public GetFileRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetFileResponse Response { get; set; }
        
        /// Empty constructor.
        public GetFile()
        {
            Request = new GetFileRequest();
            Response = new GetFileResponse();
        }
        
        /// Setter constructor.
        public GetFile(GetFileRequest request)
        {
            Request = request;
            Response = new GetFileResponse();
        }
        
        IService IService.Create() => new GetFile();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetFileRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetFileResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/GetFile";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "08088c7144705ee9cf37b287c931476d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetFileRequest : IRequest<GetFile, GetFileResponse>, IDeserializable<GetFileRequest>
    {
        // Retrieves a file
        [DataMember (Name = "uri")] public string Uri; // Uri of the file. Example: package://some_package/file.dae
    
        /// Constructor for empty message.
        public GetFileRequest()
        {
            Uri = string.Empty;
        }
        
        /// Explicit constructor.
        public GetFileRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        /// Constructor with buffer.
        internal GetFileRequest(ref Buffer b)
        {
            Uri = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetFileRequest(ref b);
        
        GetFileRequest IDeserializable<GetFileRequest>.RosDeserialize(ref Buffer b) => new GetFileRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            if (Uri is null) throw new System.NullReferenceException(nameof(Uri));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Uri);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetFileResponse : IResponse, IDeserializable<GetFileResponse>
    {
        [DataMember (Name = "success")] public bool Success; // Whether the retrieval succeeded
        [DataMember (Name = "bytes")] public byte[] Bytes; // The content of the file as byte array
        [DataMember (Name = "message")] public string Message; // An error message if success is false
    
        /// Constructor for empty message.
        public GetFileResponse()
        {
            Bytes = System.Array.Empty<byte>();
            Message = string.Empty;
        }
        
        /// Explicit constructor.
        public GetFileResponse(bool Success, byte[] Bytes, string Message)
        {
            this.Success = Success;
            this.Bytes = Bytes;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        internal GetFileResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Bytes = b.DeserializeStructArray<byte>();
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetFileResponse(ref b);
        
        GetFileResponse IDeserializable<GetFileResponse>.RosDeserialize(ref Buffer b) => new GetFileResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.SerializeStructArray(Bytes, 0);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Bytes is null) throw new System.NullReferenceException(nameof(Bytes));
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength => 9 + Bytes.Length + BuiltIns.GetStringSize(Message);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
