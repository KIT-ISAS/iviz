using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
    public sealed class Nodes : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public NodesRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public NodesResponse Response { get; set; }
        
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
        
        IService IService.Create() => new Nodes();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/Nodes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "3d07bfda1268b4f76b16b7ba8a82665d";
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
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new NodesRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 0;
    }

    public sealed class NodesResponse : IResponse
    {
        [DataMember] public string[] nodes { get; set; }
    
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
            this.nodes = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new NodesResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.nodes, 0);
        }
        
        public void Validate()
        {
            if (nodes is null) throw new System.NullReferenceException();
        }
    
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
