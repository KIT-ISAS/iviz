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
            Request = new FrameGraphRequest();
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "tf2_msgs/FrameGraph";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "437ea58e9463815a0d511c7326b686b0";
    }

    public sealed class FrameGraphRequest : Internal.EmptyRequest
    {
    }

    public sealed class FrameGraphResponse : IResponse, IDeserializable<FrameGraphResponse>
    {
        [DataMember (Name = "frame_yaml")] public string FrameYaml { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public FrameGraphResponse()
        {
            FrameYaml = "";
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
        
        public void RosValidate()
        {
            if (FrameYaml is null) throw new System.NullReferenceException(nameof(FrameYaml));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(FrameYaml);
                return size;
            }
        }
    }
}
