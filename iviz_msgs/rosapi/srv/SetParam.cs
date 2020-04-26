using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class SetParam : IService
    {
        /// <summary> Request message. </summary>
        public SetParamRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public SetParamResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SetParam()
        {
            Request = new SetParamRequest();
            Response = new SetParamResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetParam(SetParamRequest request)
        {
            Request = request;
            Response = new SetParamResponse();
        }
        
        public IService Create() => new SetParam();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/SetParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "bc6ccc4a57f61779c8eaae61e9f422e0";
    }

    public sealed class SetParamRequest : IRequest
    {
        public string name;
        public string value;
    
        /// <summary> Constructor for empty message. </summary>
        public SetParamRequest()
        {
            name = "";
            value = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out name, ref ptr, end);
            BuiltIns.Deserialize(out value, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(name, ref ptr, end);
            BuiltIns.Serialize(value, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Encoding.UTF8.GetByteCount(name);
                size += Encoding.UTF8.GetByteCount(value);
                return size;
            }
        }
    }

    public sealed class SetParamResponse : Internal.EmptyResponse
    {
    }
}
