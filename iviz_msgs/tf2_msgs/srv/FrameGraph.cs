using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract (Name = "tf2_msgs/FrameGraph")]
    public sealed class FrameGraph : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public FrameGraphRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public FrameGraphResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public FrameGraph()
        {
            Request = FrameGraphRequest.Singleton;
            Response = new FrameGraphResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "tf2_msgs/FrameGraph";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "437ea58e9463815a0d511c7326b686b0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class FrameGraphRequest : IRequest<FrameGraph, FrameGraphResponse>, IDeserializable<FrameGraphRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public FrameGraphRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal FrameGraphRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        FrameGraphRequest IDeserializable<FrameGraphRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly FrameGraphRequest Singleton = new FrameGraphRequest();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void Dispose()
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
        [DataMember (Name = "frame_yaml")] public string FrameYaml;
    
        /// <summary> Constructor for empty message. </summary>
        public FrameGraphResponse()
        {
            FrameYaml = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public FrameGraphResponse(string FrameYaml)
        {
            this.FrameYaml = FrameYaml;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal FrameGraphResponse(ref Buffer b)
        {
            FrameYaml = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FrameGraphResponse(ref b);
        }
        
        FrameGraphResponse IDeserializable<FrameGraphResponse>.RosDeserialize(ref Buffer b)
        {
            return new FrameGraphResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(FrameYaml);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (FrameYaml is null) throw new System.NullReferenceException(nameof(FrameYaml));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(FrameYaml);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
