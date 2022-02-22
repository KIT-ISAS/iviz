using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class ApplyPlanningScene : IService
    {
        /// Request message.
        [DataMember] public ApplyPlanningSceneRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ApplyPlanningSceneResponse Response { get; set; }
        
        /// Empty constructor.
        public ApplyPlanningScene()
        {
            Request = new ApplyPlanningSceneRequest();
            Response = new ApplyPlanningSceneResponse();
        }
        
        /// Setter constructor.
        public ApplyPlanningScene(ApplyPlanningSceneRequest request)
        {
            Request = request;
            Response = new ApplyPlanningSceneResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ApplyPlanningSceneRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ApplyPlanningSceneResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/ApplyPlanningScene";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "60a182de67a2bc514fbbc64e682bcaaa";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ApplyPlanningSceneRequest : IRequest<ApplyPlanningScene, ApplyPlanningSceneResponse>, IDeserializable<ApplyPlanningSceneRequest>
    {
        [DataMember (Name = "scene")] public PlanningScene Scene;
    
        /// Constructor for empty message.
        public ApplyPlanningSceneRequest()
        {
            Scene = new PlanningScene();
        }
        
        /// Explicit constructor.
        public ApplyPlanningSceneRequest(PlanningScene Scene)
        {
            this.Scene = Scene;
        }
        
        /// Constructor with buffer.
        public ApplyPlanningSceneRequest(ref ReadBuffer b)
        {
            Scene = new PlanningScene(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ApplyPlanningSceneRequest(ref b);
        
        public ApplyPlanningSceneRequest RosDeserialize(ref ReadBuffer b) => new ApplyPlanningSceneRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Scene.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Scene is null) throw new System.NullReferenceException(nameof(Scene));
            Scene.RosValidate();
        }
    
        public int RosMessageLength => 0 + Scene.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ApplyPlanningSceneResponse : IResponse, IDeserializable<ApplyPlanningSceneResponse>
    {
        [DataMember (Name = "success")] public bool Success;
    
        /// Constructor for empty message.
        public ApplyPlanningSceneResponse()
        {
        }
        
        /// Explicit constructor.
        public ApplyPlanningSceneResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// Constructor with buffer.
        public ApplyPlanningSceneResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ApplyPlanningSceneResponse(ref b);
        
        public ApplyPlanningSceneResponse RosDeserialize(ref ReadBuffer b) => new ApplyPlanningSceneResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
