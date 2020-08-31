using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/SendBytes")]
    public sealed class SendBytes : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SendBytesRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SendBytesResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SendBytes()
        {
            Request = new SendBytesRequest();
            Response = new SendBytesResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SendBytes(SendBytesRequest request)
        {
            Request = request;
            Response = new SendBytesResponse();
        }
        
        IService IService.Create() => new SendBytes();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SendBytesRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SendBytesResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/SendBytes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "d875457256decc7436099d9d612ebf8a";
    }

    public sealed class SendBytesRequest : IRequest
    {
        [DataMember (Name = "count")] public long Count { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SendBytesRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public SendBytesRequest(long Count)
        {
            this.Count = Count;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SendBytesRequest(Buffer b)
        {
            Count = b.Deserialize<long>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new SendBytesRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Count);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 8;
    }

    public sealed class SendBytesResponse : IResponse
    {
        [DataMember (Name = "data")] public string Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SendBytesResponse()
        {
            Data = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SendBytesResponse(string Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SendBytesResponse(Buffer b)
        {
            Data = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new SendBytesResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Data);
                return size;
            }
        }
    }
}
