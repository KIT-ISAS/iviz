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
            Request = NodesRequest.Singleton;
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/Nodes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "3d07bfda1268b4f76b16b7ba8a82665d";
    }

    [DataContract]
    public sealed class NodesRequest : IRequest<Nodes, NodesResponse>, IDeserializable<NodesRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public NodesRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public NodesRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        NodesRequest IDeserializable<NodesRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly NodesRequest Singleton = new();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class NodesResponse : IResponse, IDeserializable<NodesResponse>
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
        public NodesResponse(ref Buffer b)
        {
            Nodes_ = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new NodesResponse(ref b);
        }
        
        NodesResponse IDeserializable<NodesResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
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
                foreach (string s in Nodes_)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    }
}
