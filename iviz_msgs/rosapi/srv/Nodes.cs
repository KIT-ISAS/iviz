using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class Nodes : IService
    {
        /// <summary> Request message. </summary>
        public NodesRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public NodesResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Nodes()
        {
            Request = new NodesRequest();
            Response = new NodesResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Nodes(NodesRequest request)
        {
            Request = request;
            Response = new NodesResponse();
        }
        
        public IService Create() => new Nodes();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (NodesRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (NodesResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/Nodes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "3d07bfda1268b4f76b16b7ba8a82665d";
    }

    public sealed class NodesRequest : IRequest
    {
        
    
        /// <summary> Constructor for empty message. </summary>
        public NodesRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal NodesRequest(Buffer b)
        {
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new NodesRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 0;
    }

    public sealed class NodesResponse : IResponse
    {
        public string[] nodes { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public NodesResponse()
        {
            nodes = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public NodesResponse(string[] nodes)
        {
            this.nodes = nodes ?? throw new System.ArgumentNullException(nameof(nodes));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal NodesResponse(Buffer b)
        {
            this.nodes = b.DeserializeStringArray(0);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new NodesResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.nodes, 0);
        }
        
        public void Validate()
        {
            if (nodes is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * nodes.Length;
                for (int i = 0; i < nodes.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(nodes[i]);
                }
                return size;
            }
        }
    }
}
