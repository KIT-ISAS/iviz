using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class Subscribers : IService
    {
        /// <summary> Request message. </summary>
        public SubscribersRequest Request { get; set; }
        
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
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SubscribersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SubscribersResponse)value;
        }
        
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
        public string topic { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SubscribersRequest()
        {
            topic = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SubscribersRequest(string topic)
        {
            this.topic = topic ?? throw new System.ArgumentNullException(nameof(topic));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SubscribersRequest(Buffer b)
        {
            this.topic = BuiltIns.DeserializeString(b);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new SubscribersRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.topic, b);
        }
        
        public void Validate()
        {
            if (topic is null) throw new System.NullReferenceException();
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
        public string[] subscribers { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SubscribersResponse()
        {
            subscribers = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SubscribersResponse(string[] subscribers)
        {
            this.subscribers = subscribers ?? throw new System.ArgumentNullException(nameof(subscribers));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SubscribersResponse(Buffer b)
        {
            this.subscribers = BuiltIns.DeserializeStringArray(b, 0);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new SubscribersResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.subscribers, b, 0);
        }
        
        public void Validate()
        {
            if (subscribers is null) throw new System.NullReferenceException();
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
