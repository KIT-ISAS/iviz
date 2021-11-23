using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = RosServiceType)]
    public sealed class SendBytes : IService
    {
        /// Request message.
        [DataMember] public SendBytesRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SendBytesResponse Response { get; set; }
        
        /// Empty constructor.
        public SendBytes()
        {
            Request = new SendBytesRequest();
            Response = new SendBytesResponse();
        }
        
        /// Setter constructor.
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosbridge_library/SendBytes";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "d875457256decc7436099d9d612ebf8a";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SendBytesRequest : IRequest<SendBytes, SendBytesResponse>, IDeserializable<SendBytesRequest>
    {
        [DataMember (Name = "count")] public long Count;
    
        /// Constructor for empty message.
        public SendBytesRequest()
        {
        }
        
        /// Explicit constructor.
        public SendBytesRequest(long Count)
        {
            this.Count = Count;
        }
        
        /// Constructor with buffer.
        internal SendBytesRequest(ref Buffer b)
        {
            Count = b.Deserialize<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new SendBytesRequest(ref b);
        
        SendBytesRequest IDeserializable<SendBytesRequest>.RosDeserialize(ref Buffer b) => new SendBytesRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Count);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SendBytesResponse : IResponse, IDeserializable<SendBytesResponse>
    {
        [DataMember (Name = "data")] public string Data;
    
        /// Constructor for empty message.
        public SendBytesResponse()
        {
            Data = string.Empty;
        }
        
        /// Explicit constructor.
        public SendBytesResponse(string Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal SendBytesResponse(ref Buffer b)
        {
            Data = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new SendBytesResponse(ref b);
        
        SendBytesResponse IDeserializable<SendBytesResponse>.RosDeserialize(ref Buffer b) => new SendBytesResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Data);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
