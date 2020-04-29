using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class ServiceType : IService
    {
        /// <summary> Request message. </summary>
        public ServiceTypeRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public ServiceTypeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ServiceType()
        {
            Request = new ServiceTypeRequest();
            Response = new ServiceTypeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServiceType(ServiceTypeRequest request)
        {
            Request = request;
            Response = new ServiceTypeResponse();
        }
        
        public IService Create() => new ServiceType();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/ServiceType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "0e24a2dcdf70e483afc092a35a1f15f7";
    }

    public sealed class ServiceTypeRequest : IRequest
    {
        public string service;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceTypeRequest()
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
                size += BuiltIns.UTF8.GetByteCount(service);
                return size;
            }
        }
    }

    public sealed class ServiceTypeResponse : IResponse
    {
        public string type;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceTypeResponse()
        {
            type = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out type, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(type, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(type);
                return size;
            }
        }
    }
}
