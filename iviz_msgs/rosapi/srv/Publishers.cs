using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class Publishers : IService
    {
        /// <summary> Request message. </summary>
        public PublishersRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public PublishersResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Publishers()
        {
            Request = new PublishersRequest();
            Response = new PublishersResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Publishers(PublishersRequest request)
        {
            Request = request;
            Response = new PublishersResponse();
        }
        
        public IService Create() => new Publishers();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/Publishers";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "cb37f09944e7ba1fc08ee38f7a94291d";
    }

    public sealed class PublishersRequest : IRequest
    {
        public string topic;
    
        /// <summary> Constructor for empty message. </summary>
        public PublishersRequest()
        {
            topic = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out topic, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(topic, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Encoding.UTF8.GetByteCount(topic);
                return size;
            }
        }
    }

    public sealed class PublishersResponse : IResponse
    {
        public string[] publishers;
    
        /// <summary> Constructor for empty message. </summary>
        public PublishersResponse()
        {
            publishers = System.Array.Empty<string>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out publishers, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(publishers, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * publishers.Length;
                for (int i = 0; i < publishers.Length; i++)
                {
                    size += Encoding.UTF8.GetByteCount(publishers[i]);
                }
                return size;
            }
        }
    }
}
