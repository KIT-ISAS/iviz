using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class SendBytes : IService
    {
        /// <summary> Request message. </summary>
        public SendBytesRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public SendBytesResponse Response { get; set; }
        
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
        
        public IService Create() => new SendBytes();
        
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
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosbridge_library/SendBytes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "d875457256decc7436099d9d612ebf8a";
    }

    public sealed class SendBytesRequest : IRequest
    {
        public long count { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SendBytesRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public SendBytesRequest(long count)
        {
            this.count = count;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SendBytesRequest(Buffer b)
        {
            this.count = b.Deserialize<long>();
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new SendBytesRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.count);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 8;
    }

    public sealed class SendBytesResponse : IResponse
    {
        public string data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SendBytesResponse()
        {
            data = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SendBytesResponse(string data)
        {
            this.data = data ?? throw new System.ArgumentNullException(nameof(data));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SendBytesResponse(Buffer b)
        {
            this.data = b.DeserializeString();
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new SendBytesResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.data);
        }
        
        public void Validate()
        {
            if (data is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(data);
                return size;
            }
        }
    }
}
