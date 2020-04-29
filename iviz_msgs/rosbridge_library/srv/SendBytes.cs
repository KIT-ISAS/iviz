using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class SendBytes : IService
    {
        /// <summary> Request message. </summary>
        public SendBytesRequest Request { get; }
        
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
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
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
        public long count;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out count, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(count, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 8;
    }

    public sealed class SendBytesResponse : IResponse
    {
        public string data;
    
        /// <summary> Constructor for empty message. </summary>
        public SendBytesResponse()
        {
            data = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
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
