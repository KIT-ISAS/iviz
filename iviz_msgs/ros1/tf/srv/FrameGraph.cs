using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf
{
    [DataContract]
    public sealed class FrameGraph : IService
    {
        /// Request message.
        [DataMember] public FrameGraphRequest Request;
        
        /// Response message.
        [DataMember] public FrameGraphResponse Response;
        
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
    public sealed class FrameGraphRequest : IRequest<FrameGraph, FrameGraphResponse>, IDeserializable<FrameGraphRequest>
    {
    
        public FrameGraphRequest()
        {
        }
        
        public FrameGraphRequest(ref ReadBuffer b)
        {
        }
        
        public FrameGraphRequest(ref ReadBuffer2 b)
        {
        }
        
        public FrameGraphRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public FrameGraphRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static FrameGraphRequest? singleton;
        public static FrameGraphRequest Singleton => singleton ??= new FrameGraphRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class FrameGraphResponse : IResponse, IDeserializable<FrameGraphResponse>
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
        
        public FrameGraphResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out DotGraph);
        }
        
        public FrameGraphResponse RosDeserialize(ref ReadBuffer b) => new FrameGraphResponse(ref b);
        
        public FrameGraphResponse RosDeserialize(ref ReadBuffer2 b) => new FrameGraphResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(DotGraph);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(DotGraph);
        }
        
        public void RosValidate()
        {
            if (DotGraph is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(DotGraph);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, DotGraph);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
