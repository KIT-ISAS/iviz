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
        /// <summary> Uri of the file. Example: package://some_package/file.dae </summary>
        [DataMember (Name = "uri")] public string Uri;
    
        /// Constructor for empty message.
        public GetFileRequest()
        {
            Uri = "";
        }
        
        /// Explicit constructor.
        public GetFileRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        /// Constructor with buffer.
        public GetFileRequest(ref ReadBuffer b)
        {
            Uri = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetFileRequest(ref b);
        
        public GetFileRequest RosDeserialize(ref ReadBuffer b) => new GetFileRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            if (Uri is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Uri);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetFileResponse : IResponse, IDeserializable<GetFileResponse>
    {
        /// <summary> Whether the retrieval succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> The content of the file as byte array </summary>
        [DataMember (Name = "bytes")] public byte[] Bytes;
        /// <summary> An error message if success is false </summary>
        [DataMember (Name = "message")] public string Message;
    
        /// Constructor for empty message.
        public GetFileResponse()
        {
            Bytes = System.Array.Empty<byte>();
            Message = "";
        }
        
        /// Explicit constructor.
        public GetFileResponse(bool Success, byte[] Bytes, string Message)
        {
            this.Success = Success;
            this.Bytes = Bytes;
            this.Message = Message;
        }
        
        /// Constructor with buffer.
        public GetFileResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Bytes = b.DeserializeStructArray<byte>();
            Message = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetFileResponse(ref b);
        
        public GetFileResponse RosDeserialize(ref ReadBuffer b) => new GetFileResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.SerializeStructArray(Bytes);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Bytes is null) BuiltIns.ThrowNullReference();
            if (Message is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 9 + Bytes.Length + BuiltIns.GetStringSize(Message);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
