using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class TopicsAndRawTypes : IService
    {
        /// <summary> Request message. </summary>
        public TopicsAndRawTypesRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public TopicsAndRawTypesResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TopicsAndRawTypes()
        {
            Request = new TopicsAndRawTypesRequest();
            Response = new TopicsAndRawTypesResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TopicsAndRawTypes(TopicsAndRawTypesRequest request)
        {
            Request = request;
            Response = new TopicsAndRawTypesResponse();
        }
        
        public IService Create() => new TopicsAndRawTypes();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/TopicsAndRawTypes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "e1432466c8f64316723276ba07c59d12";
    }

    public sealed class TopicsAndRawTypesRequest : IRequest
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

    public sealed class TopicsAndRawTypesResponse : IResponse
    {
        public string[] topics;
        public string[] types;
        public string[] typedefs_full_text;
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsAndRawTypesResponse()
        {
            topics = System.Array.Empty<string>();
            types = System.Array.Empty<string>();
            typedefs_full_text = System.Array.Empty<string>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out topics, ref ptr, end, 0);
            BuiltIns.Deserialize(out types, ref ptr, end, 0);
            BuiltIns.Deserialize(out typedefs_full_text, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(topics, ref ptr, end, 0);
            BuiltIns.Serialize(types, ref ptr, end, 0);
            BuiltIns.Serialize(typedefs_full_text, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 12;
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
                size += 4 * typedefs_full_text.Length;
                for (int i = 0; i < typedefs_full_text.Length; i++)
                {
                    size += Encoding.UTF8.GetByteCount(typedefs_full_text[i]);
                }
                return size;
            }
        }
    }
}
