using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class NodeDetails : IService
    {
        /// <summary> Request message. </summary>
        public NodeDetailsRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public NodeDetailsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public NodeDetails()
        {
            Request = new NodeDetailsRequest();
            Response = new NodeDetailsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public NodeDetails(NodeDetailsRequest request)
        {
            Request = request;
            Response = new NodeDetailsResponse();
        }
        
        public IService Create() => new NodeDetails();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/NodeDetails";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "e1d0ced5ab8d5edb5fc09c98eb1d46f6";
    }

    public sealed class NodeDetailsRequest : IRequest
    {
        public string node;
    
        /// <summary> Constructor for empty message. </summary>
        public NodeDetailsRequest()
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
                size += Encoding.UTF8.GetByteCount(node);
                return size;
            }
        }
    }

    public sealed class NodeDetailsResponse : IResponse
    {
        public string[] subscribing;
        public string[] publishing;
        public string[] services;
    
        /// <summary> Constructor for empty message. </summary>
        public NodeDetailsResponse()
        {
            subscribing = System.Array.Empty<string>();
            publishing = System.Array.Empty<string>();
            services = System.Array.Empty<string>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out subscribing, ref ptr, end, 0);
            BuiltIns.Deserialize(out publishing, ref ptr, end, 0);
            BuiltIns.Deserialize(out services, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(subscribing, ref ptr, end, 0);
            BuiltIns.Serialize(publishing, ref ptr, end, 0);
            BuiltIns.Serialize(services, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += 4 * subscribing.Length;
                for (int i = 0; i < subscribing.Length; i++)
                {
                    size += Encoding.UTF8.GetByteCount(subscribing[i]);
                }
                size += 4 * publishing.Length;
                for (int i = 0; i < publishing.Length; i++)
                {
                    size += Encoding.UTF8.GetByteCount(publishing[i]);
                }
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
