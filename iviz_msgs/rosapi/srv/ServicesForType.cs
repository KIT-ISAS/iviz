using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class ServicesForType : IService
    {
        /// <summary> Request message. </summary>
        public ServicesForTypeRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public ServicesForTypeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ServicesForType()
        {
            Request = new ServicesForTypeRequest();
            Response = new ServicesForTypeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ServicesForType(ServicesForTypeRequest request)
        {
            Request = request;
            Response = new ServicesForTypeResponse();
        }
        
        public IService Create() => new ServicesForType();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/ServicesForType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "93e9fe8ae5a9136008e260fe510bd2b0";
    }

    public sealed class ServicesForTypeRequest : IRequest
    {
        public string type;
    
        /// <summary> Constructor for empty message. </summary>
        public ServicesForTypeRequest()
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

    public sealed class ServicesForTypeResponse : IResponse
    {
        public string[] services;
    
        /// <summary> Constructor for empty message. </summary>
        public ServicesForTypeResponse()
        {
            services = System.Array.Empty<string>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out services, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(services, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * services.Length;
                for (int i = 0; i < services.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(services[i]);
                }
                return size;
            }
        }
    }
}
