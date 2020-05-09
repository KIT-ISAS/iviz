using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class NodeDetails : IService
    {
        /// <summary> Request message. </summary>
        public NodeDetailsRequest Request { get; set; }
        
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
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (NodeDetailsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (NodeDetailsResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/NodeDetails";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "e1d0ced5ab8d5edb5fc09c98eb1d46f6";
    }

    public sealed class NodeDetailsRequest : IRequest
    {
        public string node { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public NodeDetailsRequest()
        {
            node = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public NodeDetailsRequest(string node)
        {
            this.node = node ?? throw new System.ArgumentNullException(nameof(node));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal NodeDetailsRequest(Buffer b)
        {
            this.node = BuiltIns.DeserializeString(b);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new NodeDetailsRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.node, b);
        }
        
        public void Validate()
        {
            if (node is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(node);
                return size;
            }
        }
    }

    public sealed class NodeDetailsResponse : IResponse
    {
        public string[] subscribing { get; set; }
        public string[] publishing { get; set; }
        public string[] services { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public NodeDetailsResponse()
        {
            subscribing = System.Array.Empty<string>();
            publishing = System.Array.Empty<string>();
            services = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public NodeDetailsResponse(string[] subscribing, string[] publishing, string[] services)
        {
            this.subscribing = subscribing ?? throw new System.ArgumentNullException(nameof(subscribing));
            this.publishing = publishing ?? throw new System.ArgumentNullException(nameof(publishing));
            this.services = services ?? throw new System.ArgumentNullException(nameof(services));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal NodeDetailsResponse(Buffer b)
        {
            this.subscribing = BuiltIns.DeserializeStringArray(b, 0);
            this.publishing = BuiltIns.DeserializeStringArray(b, 0);
            this.services = BuiltIns.DeserializeStringArray(b, 0);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new NodeDetailsResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.subscribing, b, 0);
            BuiltIns.Serialize(this.publishing, b, 0);
            BuiltIns.Serialize(this.services, b, 0);
        }
        
        public void Validate()
        {
            if (subscribing is null) throw new System.NullReferenceException();
            if (publishing is null) throw new System.NullReferenceException();
            if (services is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += 4 * subscribing.Length;
                for (int i = 0; i < subscribing.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(subscribing[i]);
                }
                size += 4 * publishing.Length;
                for (int i = 0; i < publishing.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(publishing[i]);
                }
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
