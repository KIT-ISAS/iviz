using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.std_srvs
{
    public sealed class SetBool : IService
    {
        /// <summary> Request message. </summary>
        public SetBoolRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public SetBoolResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SetBool()
        {
            Request = new SetBoolRequest();
            Response = new SetBoolResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetBool(SetBoolRequest request)
        {
            Request = request;
            Response = new SetBoolResponse();
        }
        
        public IService Create() => new SetBool();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "std_srvs/SetBool";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "09fb03525b03e7ea1fd3992bafd87e16";
    }

    public sealed class SetBoolRequest : IRequest
    {
        public bool data; // e.g. for hardware enabling / disabling
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 1;
    }

    public sealed class SetBoolResponse : IResponse
    {
        public bool success; // indicate successful run of triggered service
        public string message; // informational, e.g. for error messages
    
        /// <summary> Constructor for empty message. </summary>
        public SetBoolResponse()
        {
            message = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out success, ref ptr, end);
            BuiltIns.Deserialize(out message, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(success, ref ptr, end);
            BuiltIns.Serialize(message, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += Encoding.UTF8.GetByteCount(message);
                return size;
            }
        }
    }
}
