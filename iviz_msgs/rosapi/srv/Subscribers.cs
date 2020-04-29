using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class Subscribers : IService
    {
        /// <summary> Request message. </summary>
        public SubscribersRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public SubscribersResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Subscribers()
        {
            Request = new SubscribersRequest();
            Response = new SubscribersResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Subscribers(SubscribersRequest request)
        {
            Request = request;
            Response = new SubscribersResponse();
        }
        
        public IService Create() => new Subscribers();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/Subscribers";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "cb387b68f5b29bc1456398ee8476b973";
    }

    public sealed class SubscribersRequest : IRequest
    {
        public string topic;
    
        /// <summary> Constructor for empty message. </summary>
        public SubscribersRequest()
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
                size += BuiltIns.UTF8.GetByteCount(topic);
                return size;
            }
        }
    }

    public sealed class SubscribersResponse : IResponse
    {
        public string[] subscribers;
    
        /// <summary> Constructor for empty message. </summary>
        public SubscribersResponse()
        {
            subscribers = System.Array.Empty<string>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out subscribers, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(subscribers, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * subscribers.Length;
                for (int i = 0; i < subscribers.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(subscribers[i]);
                }
                return size;
            }
        }
    }
}
