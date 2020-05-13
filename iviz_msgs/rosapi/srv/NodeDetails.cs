using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
    public sealed class NodeDetails : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public NodeDetailsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public NodeDetailsResponse Response { get; set; }
        
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
        
        IService IService.Create() => new NodeDetails();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/NodeDetails";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "e1d0ced5ab8d5edb5fc09c98eb1d46f6";
    }

    public sealed class NodeDetailsRequest : IRequest
    {
        [DataMember] public string node { get; set; }
    
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
            this.node = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new NodeDetailsRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.node);
        }
        
        public void Validate()
        {
            if (node is null) throw new System.NullReferenceException();
        }
    
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
        [DataMember] public string[] subscribing { get; set; }
        [DataMember] public string[] publishing { get; set; }
        [DataMember] public string[] services { get; set; }
    
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
            this.subscribing = b.DeserializeStringArray();
            this.publishing = b.DeserializeStringArray();
            this.services = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new NodeDetailsResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.subscribing, 0);
            b.SerializeArray(this.publishing, 0);
            b.SerializeArray(this.services, 0);
        }
        
        public void Validate()
        {
            if (subscribing is null) throw new System.NullReferenceException();
            if (publishing is null) throw new System.NullReferenceException();
            if (services is null) throw new System.NullReferenceException();
        }
    
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
