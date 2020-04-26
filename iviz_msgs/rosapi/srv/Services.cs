using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class Services : IService
    {
        /// <summary> Request message. </summary>
        public ServicesRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public ServicesResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Services()
        {
            Request = new ServicesRequest();
            Response = new ServicesResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Services(ServicesRequest request)
        {
            Request = request;
            Response = new ServicesResponse();
        }
        
        public IService Create() => new Services();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/Services";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "e44a7e7bcb900acadbcc28b132378f0c";
    }

    public sealed class ServicesRequest : IRequest
    {
        
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 0;
    }

    public sealed class ServicesResponse : IResponse
    {
        public string[] services;
    
        /// <summary> Constructor for empty message. </summary>
        public ServicesResponse()
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
                    size += Encoding.UTF8.GetByteCount(services[i]);
                }
                return size;
            }
        }
    }
}
