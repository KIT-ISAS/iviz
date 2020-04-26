using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class Topics : IService
    {
        /// <summary> Request message. </summary>
        public TopicsRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public TopicsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Topics()
        {
            Request = new TopicsRequest();
            Response = new TopicsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Topics(TopicsRequest request)
        {
            Request = request;
            Response = new TopicsResponse();
        }
        
        public IService Create() => new Topics();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/Topics";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "d966d98fc333fa1f3135af765eac1ba8";
    }

    public sealed class TopicsRequest : IRequest
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

    public sealed class TopicsResponse : IResponse
    {
        public string[] topics;
        public string[] types;
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsResponse()
        {
            topics = System.Array.Empty<string>();
            types = System.Array.Empty<string>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out topics, ref ptr, end, 0);
            BuiltIns.Deserialize(out types, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(topics, ref ptr, end, 0);
            BuiltIns.Serialize(types, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += 4 * topics.Length;
                for (int i = 0; i < topics.Length; i++)
                {
                    size += Encoding.UTF8.GetByteCount(topics[i]);
                }
                size += 4 * types.Length;
                for (int i = 0; i < types.Length; i++)
                {
                    size += Encoding.UTF8.GetByteCount(types[i]);
                }
                return size;
            }
        }
    }
}
