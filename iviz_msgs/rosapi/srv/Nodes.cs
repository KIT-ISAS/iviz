using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class Nodes : IService
    {
        /// Request message.
        [DataMember] public NodesRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public NodesResponse Response { get; set; }
        
        /// Empty constructor.
        public Nodes()
        {
            Request = NodesRequest.Singleton;
            Response = new NodesResponse();
        }
        
        /// Setter constructor.
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/Nodes";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "3d07bfda1268b4f76b16b7ba8a82665d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class NodesRequest : IRequest<Nodes, NodesResponse>, IDeserializable<NodesRequest>
    {
    
        /// Constructor for empty message.
        public NodesRequest()
        {
        }
        
        /// Constructor with buffer.
        internal NodesRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        NodesRequest IDeserializable<NodesRequest>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly NodesRequest Singleton = new NodesRequest();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class NodesResponse : IResponse, IDeserializable<NodesResponse>
    {
        [DataMember (Name = "nodes")] public string[] Nodes_;
    
        /// Constructor for empty message.
        public NodesResponse()
        {
            Nodes_ = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public NodesResponse(string[] Nodes_)
        {
            this.Nodes_ = Nodes_;
        }
        
        /// Constructor with buffer.
        internal NodesResponse(ref Buffer b)
        {
            Nodes_ = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new NodesResponse(ref b);
        
        NodesResponse IDeserializable<NodesResponse>.RosDeserialize(ref Buffer b) => new NodesResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Nodes_);
        }
        
        public void RosValidate()
        {
            if (Nodes_ is null) throw new System.NullReferenceException(nameof(Nodes_));
            for (int i = 0; i < Nodes_.Length; i++)
            {
                if (Nodes_[i] is null) throw new System.NullReferenceException($"{nameof(Nodes_)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Nodes_);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
