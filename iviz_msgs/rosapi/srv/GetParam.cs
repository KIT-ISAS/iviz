using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class GetParam : IService
    {
        /// <summary> Request message. </summary>
        public GetParamRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public GetParamResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetParam()
        {
            Request = new GetParamRequest();
            Response = new GetParamResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetParam(GetParamRequest request)
        {
            Request = request;
            Response = new GetParamResponse();
        }
        
        public IService Create() => new GetParam();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/GetParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "e36fd90759dbac1c5159140a7fa8c644";
    }

    public sealed class GetParamRequest : IRequest
    {
        public string name;
        public string @default;
    
        /// <summary> Constructor for empty message. </summary>
        public GetParamRequest()
        {
            name = "";
            @default = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out name, ref ptr, end);
            BuiltIns.Deserialize(out @default, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(name, ref ptr, end);
            BuiltIns.Serialize(@default, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Encoding.UTF8.GetByteCount(name);
                size += Encoding.UTF8.GetByteCount(@default);
                return size;
            }
        }
    }

    public sealed class GetParamResponse : IResponse
    {
        public string value;
    
        /// <summary> Constructor for empty message. </summary>
        public GetParamResponse()
        {
            value = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out value, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(value, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Encoding.UTF8.GetByteCount(value);
                return size;
            }
        }
    }
}
