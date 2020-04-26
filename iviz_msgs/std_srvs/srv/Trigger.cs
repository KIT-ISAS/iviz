using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.std_srvs
{
    public sealed class Trigger : IService
    {
        /// <summary> Request message. </summary>
        public TriggerRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public TriggerResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Trigger()
        {
            Request = new TriggerRequest();
            Response = new TriggerResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Trigger(TriggerRequest request)
        {
            Request = request;
            Response = new TriggerResponse();
        }
        
        public IService Create() => new Trigger();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "std_srvs/Trigger";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "937c9679a518e3a18d831e57125ea522";
    }

    public sealed class TriggerRequest : Internal.EmptyRequest
    {
    }

    public sealed class TriggerResponse : IResponse
    {
        public bool success; // indicate successful run of triggered service
        public string message; // informational, e.g. for error messages
    
        /// <summary> Constructor for empty message. </summary>
        public TriggerResponse()
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
