using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class ServiceNode : IService
    {
        /// <summary> Request message. </summary>
        public ServiceNodeRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public ServiceNodeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ServiceNode()
        {
            Request = new ServiceNodeRequest();
            Response = new ServiceNodeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServiceNode(ServiceNodeRequest request)
        {
            Request = request;
            Response = new ServiceNodeResponse();
        }
        
        public IService Create() => new ServiceNode();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/ServiceNode";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "bd2a0a45fd7a73a86c8d6051d5a6db8a";
    }

    public sealed class ServiceNodeRequest : IRequest
    {
        public string service;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceNodeRequest()
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

    public sealed class ServiceNodeResponse : IResponse
    {
        public string node;
    
        /// <summary> Constructor for empty message. </summary>
        public ServiceNodeResponse()
        {
            node = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out node, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(node, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(node);
                return size;
            }
        }
    }
}
