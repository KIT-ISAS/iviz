using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class GetFile : IService<GetFileRequest, GetFileResponse>
    {
        /// Request message.
        [DataMember] public GetFileRequest Request;
        
        /// Response message.
        [DataMember] public GetFileResponse Response;
        
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
        
        public const string ServiceType = "iviz_msgs/GetFile";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "08088c7144705ee9cf37b287c931476d";
        
        public IService Generate() => new GetFile();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetFileRequest : IRequest<GetFile, GetFileResponse>, IDeserializable<GetFileRequest>
    {
        // Retrieves a file
        /// <summary> Uri of the file. Example: package://some_package/file.dae </summary>
        [DataMember (Name = "uri")] public string Uri;
    
        public GetFileRequest()
        {
            Uri = "";
        }
        
        public GetFileRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        public GetFileRequest(ref ReadBuffer b)
        {
            Uri = b.DeserializeString();
        }
        
        public GetFileRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Uri = b.DeserializeString();
        }
        
        public GetFileRequest RosDeserialize(ref ReadBuffer b) => new GetFileRequest(ref b);
        
        public GetFileRequest RosDeserialize(ref ReadBuffer2 b) => new GetFileRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uri);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Uri);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Uri);
            return size;
        }
    
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
    
        public GetFileResponse()
        {
            Bytes = EmptyArray<byte>.Value;
            Message = "";
        }
        
        public GetFileResponse(bool Success, byte[] Bytes, string Message)
        {
            this.Success = Success;
            this.Bytes = Bytes;
            this.Message = Message;
        }
        
        public GetFileResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            {
                int n = b.DeserializeArrayLength();
                byte[] array;
                if (n == 0) array = EmptyArray<byte>.Value;
                else
                {
                    array = new byte[n];
                    b.DeserializeStructArray(array);
                }
                Bytes = array;
            }
            Message = b.DeserializeString();
        }
        
        public GetFileResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                byte[] array;
                if (n == 0) array = EmptyArray<byte>.Value;
                else
                {
                    array = new byte[n];
                    b.DeserializeStructArray(array);
                }
                Bytes = array;
            }
            b.Align4();
            Message = b.DeserializeString();
        }
        
        public GetFileResponse RosDeserialize(ref ReadBuffer b) => new GetFileResponse(ref b);
        
        public GetFileResponse RosDeserialize(ref ReadBuffer2 b) => new GetFileResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Bytes.Length);
            b.SerializeStructArray(Bytes);
            b.Serialize(Message);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Align4();
            b.Serialize(Bytes.Length);
            b.SerializeStructArray(Bytes);
            b.Align4();
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Bytes, nameof(Bytes));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 9;
                size += Bytes.Length;
                size += WriteBuffer.GetStringSize(Message);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Success
            size = WriteBuffer2.Align4(size);
            size += 4; // Bytes.Length
            size += 1 * Bytes.Length;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Message);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
