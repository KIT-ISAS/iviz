using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class ServiceHost : IService
    {
        /// <summary> Request message. </summary>
        public ServiceHostRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public ServiceHostResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ServiceHost()
        {
            Request = new ServiceHostRequest();
            Response = new ServiceHostResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServiceHost(ServiceHostRequest request)
        {
            Request = request;
            Response = new ServiceHostResponse();
        }
        
        public IService Create() => new ServiceHost();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/ServiceHost";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "a1b60006f8ee69637c856c94dd192f5a";
    }

    public sealed class ServiceHostRequest : IRequest
    {
        public string service;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceHostRequest()
        {
            service = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out service, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(service, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Encoding.UTF8.GetByteCount(service);
                return size;
            }
        }
    }

    public sealed class ServiceHostResponse : IResponse
    {
        public string host;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceHostResponse()
        {
            host = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out host, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(host, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Encoding.UTF8.GetByteCount(host);
                return size;
            }
        }
    }
}
