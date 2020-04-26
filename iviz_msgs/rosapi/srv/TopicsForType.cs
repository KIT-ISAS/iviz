using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class TopicsForType : IService
    {
        /// <summary> Request message. </summary>
        public TopicsForTypeRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public TopicsForTypeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TopicsForType()
        {
            Request = new TopicsForTypeRequest();
            Response = new TopicsForTypeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TopicsForType(TopicsForTypeRequest request)
        {
            Request = request;
            Response = new TopicsForTypeResponse();
        }
        
        public IService Create() => new TopicsForType();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/TopicsForType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "56f77ff6da756dd27c1ed16ec721072a";
    }

    public sealed class TopicsForTypeRequest : IRequest
    {
        public string type;
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsForTypeRequest()
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
                size += Encoding.UTF8.GetByteCount(type);
                return size;
            }
        }
    }

    public sealed class TopicsForTypeResponse : IResponse
    {
        public string[] topics;
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsForTypeResponse()
        {
            topics = System.Array.Empty<string>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out topics, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(topics, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * topics.Length;
                for (int i = 0; i < topics.Length; i++)
                {
                    size += Encoding.UTF8.GetByteCount(topics[i]);
                }
                return size;
            }
        }
    }
}
