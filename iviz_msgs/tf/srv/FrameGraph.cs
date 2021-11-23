using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf
{
    [DataContract (Name = RosServiceType)]
    public sealed class FrameGraph : IService
    {
        /// Request message.
        [DataMember] public FrameGraphRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public FrameGraphResponse Response { get; set; }
        
        /// Empty constructor.
        public FrameGraph()
        {
            Request = FrameGraphRequest.Singleton;
            Response = new FrameGraphResponse();
        }
        
        /// Setter constructor.
        public FrameGraph(FrameGraphRequest request)
        {
            Request = request;
            Response = new FrameGraphResponse();
        }
        
        IService IService.Create() => new FrameGraph();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (FrameGraphRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (FrameGraphResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "tf/FrameGraph";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "c4af9ac907e58e906eb0b6e3c58478c0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class FrameGraphRequest : IRequest<FrameGraph, FrameGraphResponse>, IDeserializable<FrameGraphRequest>
    {
    
        /// Constructor for empty message.
        public FrameGraphRequest()
        {
        }
        
        /// Constructor with buffer.
        internal FrameGraphRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        FrameGraphRequest IDeserializable<FrameGraphRequest>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly FrameGraphRequest Singleton = new FrameGraphRequest();
    
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
    public sealed class FrameGraphResponse : IResponse, IDeserializable<FrameGraphResponse>
    {
        [DataMember (Name = "dot_graph")] public string DotGraph;
    
        /// Constructor for empty message.
        public FrameGraphResponse()
        {
            DotGraph = string.Empty;
        }
        
        /// Explicit constructor.
        public FrameGraphResponse(string DotGraph)
        {
            this.DotGraph = DotGraph;
        }
        
        /// Constructor with buffer.
        internal FrameGraphResponse(ref Buffer b)
        {
            DotGraph = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new FrameGraphResponse(ref b);
        
        FrameGraphResponse IDeserializable<FrameGraphResponse>.RosDeserialize(ref Buffer b) => new FrameGraphResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(DotGraph);
        }
        
        public void RosValidate()
        {
            if (DotGraph is null) throw new System.NullReferenceException(nameof(DotGraph));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(DotGraph);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
