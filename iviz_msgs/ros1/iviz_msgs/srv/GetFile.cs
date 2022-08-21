using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class GetFile : IService
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
            b.DeserializeString(out Uri);
        }
        
        public GetFileRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Uri);
        }
        
        public GetFileRequest RosDeserialize(ref ReadBuffer b) => new GetFileRequest(ref b);
        
        public GetFileRequest RosDeserialize(ref ReadBuffer2 b) => new GetFileRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uri);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            if (Uri is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Uri);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Uri);
            return c;
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
            Bytes = System.Array.Empty<byte>();
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
            unsafe
            {
                int n = b.DeserializeArrayLength();
                Bytes = n == 0
                    ? System.Array.Empty<byte>()
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Bytes[0]), n * 1);
                }
            }
            b.DeserializeString(out Message);
        }
        
        public GetFileResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            unsafe
            {
                int n = b.DeserializeArrayLength();
                Bytes = n == 0
                    ? System.Array.Empty<byte>()
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Bytes[0]), n * 1);
                }
            }
            b.Align4();
            b.DeserializeString(out Message);
        }
        
        public GetFileResponse RosDeserialize(ref ReadBuffer b) => new GetFileResponse(ref b);
        
        public GetFileResponse RosDeserialize(ref ReadBuffer2 b) => new GetFileResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.SerializeStructArray(Bytes);
            b.Serialize(Message);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
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
    
        public int RosMessageLength => 9 + Bytes.Length + WriteBuffer.GetStringSize(Message);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Success
            c = WriteBuffer2.Align4(c);
            c += 4; // Bytes length
            c += 1 * Bytes.Length;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Message);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
