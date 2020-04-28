using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class HasParam : IService
    {
        /// <summary> Request message. </summary>
        public HasParamRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public HasParamResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public HasParam()
        {
            Request = new HasParamRequest();
            Response = new HasParamResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public HasParam(HasParamRequest request)
        {
            Request = request;
            Response = new HasParamResponse();
        }
        
        public IService Create() => new HasParam();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/HasParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "ed3df286bd6dff9b961770f577454ea9";
    }

    public sealed class HasParamRequest : IRequest
    {
        public string name;
    
        /// <summary> Constructor for empty message. </summary>
        public HasParamRequest()
        {
            name = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out name, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(name, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(name);
                return size;
            }
        }
    }

    public sealed class HasParamResponse : IResponse
    {
        public bool exists;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out exists, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(exists, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 1;
    }
}
