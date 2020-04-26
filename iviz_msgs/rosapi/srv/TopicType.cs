using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class TopicType : IService
    {
        /// <summary> Request message. </summary>
        public TopicTypeRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public TopicTypeResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TopicType()
        {
            Request = new TopicTypeRequest();
            Response = new TopicTypeResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TopicType(TopicTypeRequest request)
        {
            Request = request;
            Response = new TopicTypeResponse();
        }
        
        public IService Create() => new TopicType();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/TopicType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "0d30b3f53a0fd5036523a7141e524ddf";
    }

    public sealed class TopicTypeRequest : IRequest
    {
        public string topic;
    
        /// <summary> Constructor for empty message. </summary>
        public TopicTypeRequest()
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

    public sealed class TopicTypeResponse : IResponse
    {
        public string type;
    
        /// <summary> Constructor for empty message. </summary>
        public TopicTypeResponse()
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
}
