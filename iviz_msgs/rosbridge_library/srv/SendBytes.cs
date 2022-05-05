using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract]
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
        
        public const string ServiceType = "rosbridge_library/SendBytes";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "d875457256decc7436099d9d612ebf8a";
        
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
        public SendBytesRequest(ref ReadBuffer b)
        {
            b.Deserialize(out Count);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SendBytesRequest(ref b);
        
        public SendBytesRequest RosDeserialize(ref ReadBuffer b) => new SendBytesRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Count);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 8;
        
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
            Data = "";
        }
        
        /// Explicit constructor.
        public SendBytesResponse(string Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public SendBytesResponse(ref ReadBuffer b)
        {
            b.DeserializeString(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SendBytesResponse(ref b);
        
        public SendBytesResponse RosDeserialize(ref ReadBuffer b) => new SendBytesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Data);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
