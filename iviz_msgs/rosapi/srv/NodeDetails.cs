using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class NodeDetails : IService
    {
        /// Request message.
        [DataMember] public NodeDetailsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public NodeDetailsResponse Response { get; set; }
        
        /// Empty constructor.
        public NodeDetails()
        {
            Request = new NodeDetailsRequest();
            Response = new NodeDetailsResponse();
        }
        
        /// Setter constructor.
        public NodeDetails(NodeDetailsRequest request)
        {
            Request = request;
            Response = new NodeDetailsResponse();
        }
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/NodeDetails";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "e1d0ced5ab8d5edb5fc09c98eb1d46f6";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class NodeDetailsRequest : IRequest<NodeDetails, NodeDetailsResponse>, IDeserializable<NodeDetailsRequest>
    {
        [DataMember (Name = "node")] public string Node;
    
        /// Constructor for empty message.
        public NodeDetailsRequest()
        {
            Node = "";
        }
        
        /// Explicit constructor.
        public NodeDetailsRequest(string Node)
        {
            this.Node = Node;
        }
        
        /// Constructor with buffer.
        public NodeDetailsRequest(ref ReadBuffer b)
        {
            Node = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new NodeDetailsRequest(ref b);
        
        public NodeDetailsRequest RosDeserialize(ref ReadBuffer b) => new NodeDetailsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Node);
        }
        
        public void RosValidate()
        {
            if (Node is null) BuiltIns.ThrowNullReference(nameof(Node));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Node);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class NodeDetailsResponse : IResponse, IDeserializable<NodeDetailsResponse>
    {
        [DataMember (Name = "subscribing")] public string[] Subscribing;
        [DataMember (Name = "publishing")] public string[] Publishing;
        [DataMember (Name = "services")] public string[] Services;
    
        /// Constructor for empty message.
        public NodeDetailsResponse()
        {
            Subscribing = System.Array.Empty<string>();
            Publishing = System.Array.Empty<string>();
            Services = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public NodeDetailsResponse(string[] Subscribing, string[] Publishing, string[] Services)
        {
            this.Subscribing = Subscribing;
            this.Publishing = Publishing;
            this.Services = Services;
        }
        
        /// Constructor with buffer.
        public NodeDetailsResponse(ref ReadBuffer b)
        {
            Subscribing = b.DeserializeStringArray();
            Publishing = b.DeserializeStringArray();
            Services = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new NodeDetailsResponse(ref b);
        
        public NodeDetailsResponse RosDeserialize(ref ReadBuffer b) => new NodeDetailsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Subscribing);
            b.SerializeArray(Publishing);
            b.SerializeArray(Services);
        }
        
        public void RosValidate()
        {
            if (Subscribing is null) BuiltIns.ThrowNullReference(nameof(Subscribing));
            for (int i = 0; i < Subscribing.Length; i++)
            {
                if (Subscribing[i] is null) BuiltIns.ThrowNullReference($"{nameof(Subscribing)}[{i}]");
            }
            if (Publishing is null) BuiltIns.ThrowNullReference(nameof(Publishing));
            for (int i = 0; i < Publishing.Length; i++)
            {
                if (Publishing[i] is null) BuiltIns.ThrowNullReference($"{nameof(Publishing)}[{i}]");
            }
            if (Services is null) BuiltIns.ThrowNullReference(nameof(Services));
            for (int i = 0; i < Services.Length; i++)
            {
                if (Services[i] is null) BuiltIns.ThrowNullReference($"{nameof(Services)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.GetArraySize(Subscribing);
                size += BuiltIns.GetArraySize(Publishing);
                size += BuiltIns.GetArraySize(Services);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
