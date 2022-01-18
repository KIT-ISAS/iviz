using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
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
        [Preserve] public const string RosServiceType = "tf2_msgs/FrameGraph";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "437ea58e9463815a0d511c7326b686b0";
        
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
        
        public static readonly FrameGraphRequest Singleton = new FrameGraphRequest();
    
        public void RosSerialize(ref WriteBuffer b)
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
        [DataMember (Name = "frame_yaml")] public string FrameYaml;
    
        /// Constructor for empty message.
        public FrameGraphResponse()
        {
            FrameYaml = string.Empty;
        }
        
        /// Explicit constructor.
        public FrameGraphResponse(string FrameYaml)
        {
            this.FrameYaml = FrameYaml;
        }
        
        /// Constructor with buffer.
        public FrameGraphResponse(ref ReadBuffer b)
        {
            FrameYaml = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new FrameGraphResponse(ref b);
        
        public FrameGraphResponse RosDeserialize(ref ReadBuffer b) => new FrameGraphResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(FrameYaml);
        }
        
        public void RosValidate()
        {
            if (FrameYaml is null) throw new System.NullReferenceException(nameof(FrameYaml));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(FrameYaml);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
