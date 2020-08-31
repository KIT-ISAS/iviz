using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/Nodes")]
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
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new NodesRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 0;
    }

    public sealed class NodesResponse : IResponse
    {
        [DataMember (Name = "nodes")] public string[] Nodes_ { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public NodesResponse()
        {
            Nodes_ = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public NodesResponse(string[] Nodes_)
        {
            this.Nodes_ = Nodes_;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal NodesResponse(Buffer b)
        {
            Nodes_ = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new NodesResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(Nodes_, 0);
        }
        
        public void RosValidate()
        {
            if (Nodes_ is null) throw new System.NullReferenceException(nameof(Nodes_));
            for (int i = 0; i < Nodes_.Length; i++)
            {
                if (Nodes_[i] is null) throw new System.NullReferenceException($"{nameof(Nodes_)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * Nodes_.Length;
                for (int i = 0; i < Nodes_.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(Nodes_[i]);
                }
                return size;
            }
        }
    }
}
