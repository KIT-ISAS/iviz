using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf
{
    [DataContract]
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
        
        public const string ServiceType = "tf/FrameGraph";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "c4af9ac907e58e906eb0b6e3c58478c0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class FrameGraphRequest : IRequest<FrameGraph, FrameGraphResponse>, IDeserializableRos1<FrameGraphRequest>
    {
    
        public FrameGraphRequest()
        {
        }
        
        public FrameGraphRequest(ref ReadBuffer b)
        {
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public FrameGraphRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static FrameGraphRequest? singleton;
        public static FrameGraphRequest Singleton => singleton ??= new FrameGraphRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class FrameGraphResponse : IResponse, IDeserializableRos1<FrameGraphResponse>
    {
        [DataMember (Name = "dot_graph")] public string DotGraph;
    
        public FrameGraphResponse()
        {
            DotGraph = "";
        }
        
        public FrameGraphResponse(string DotGraph)
        {
            this.DotGraph = DotGraph;
        }
        
        public FrameGraphResponse(ref ReadBuffer b)
        {
            b.DeserializeString(out DotGraph);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new FrameGraphResponse(ref b);
        
        public FrameGraphResponse RosDeserialize(ref ReadBuffer b) => new FrameGraphResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(DotGraph);
        }
        
        public void RosValidate()
        {
            if (DotGraph is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(DotGraph);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
