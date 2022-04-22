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
        public FrameGraphRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public FrameGraphRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static FrameGraphRequest? singleton;
        public static FrameGraphRequest Singleton => singleton ??= new FrameGraphRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
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
            DotGraph = "";
        }
        
        /// Explicit constructor.
        public FrameGraphResponse(string DotGraph)
        {
            this.DotGraph = DotGraph;
        }
        
        /// Constructor with buffer.
        public FrameGraphResponse(ref ReadBuffer b)
        {
            DotGraph = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new FrameGraphResponse(ref b);
        
        public FrameGraphResponse RosDeserialize(ref ReadBuffer b) => new FrameGraphResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(DotGraph);
        }
        
        public void RosValidate()
        {
            if (DotGraph is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(DotGraph);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
