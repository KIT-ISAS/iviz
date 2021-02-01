using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/ApplyPlanningScene")]
    public sealed class ApplyPlanningScene : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ApplyPlanningSceneRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ApplyPlanningSceneResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ApplyPlanningScene()
        {
            Request = new ApplyPlanningSceneRequest();
            Response = new ApplyPlanningSceneResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ApplyPlanningScene(ApplyPlanningSceneRequest request)
        {
            Request = request;
            Response = new ApplyPlanningSceneResponse();
        }
        
        IService IService.Create() => new ApplyPlanningScene();
        
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/ApplyPlanningScene";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "60a182de67a2bc514fbbc64e682bcaaa";
    }

    [DataContract]
    public sealed class ApplyPlanningSceneRequest : IRequest<ApplyPlanningScene, ApplyPlanningSceneResponse>, IDeserializable<ApplyPlanningSceneRequest>
    {
        [DataMember (Name = "scene")] public PlanningScene Scene { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ApplyPlanningSceneRequest()
        {
            Scene = new PlanningScene();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ApplyPlanningSceneRequest(PlanningScene Scene)
        {
            this.Scene = Scene;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ApplyPlanningSceneRequest(ref Buffer b)
        {
            Scene = new PlanningScene(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ApplyPlanningSceneRequest(ref b);
        }
        
        ApplyPlanningSceneRequest IDeserializable<ApplyPlanningSceneRequest>.RosDeserialize(ref Buffer b)
        {
            return new ApplyPlanningSceneRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Scene.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Scene is null) throw new System.NullReferenceException(nameof(Scene));
            Scene.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Scene.RosMessageLength;
                return size;
            }
        }
    }

    [DataContract]
    public sealed class ApplyPlanningSceneResponse : IResponse, IDeserializable<ApplyPlanningSceneResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ApplyPlanningSceneResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public ApplyPlanningSceneResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ApplyPlanningSceneResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ApplyPlanningSceneResponse(ref b);
        }
        
        ApplyPlanningSceneResponse IDeserializable<ApplyPlanningSceneResponse>.RosDeserialize(ref Buffer b)
        {
            return new ApplyPlanningSceneResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    }
}
