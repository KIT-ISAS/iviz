using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetPlanningScene : IService
    {
        /// Request message.
        [DataMember] public GetPlanningSceneRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetPlanningSceneResponse Response { get; set; }
        
        /// Empty constructor.
        public GetPlanningScene()
        {
            Request = new GetPlanningSceneRequest();
            Response = new GetPlanningSceneResponse();
        }
        
        /// Setter constructor.
        public GetPlanningScene(GetPlanningSceneRequest request)
        {
            Request = request;
            Response = new GetPlanningSceneResponse();
        }
        
        IService IService.Create() => new GetPlanningScene();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetPlanningSceneRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetPlanningSceneResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/GetPlanningScene";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "2745cf315b4eb5fb00e5befa8714d64d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetPlanningSceneRequest : IRequest<GetPlanningScene, GetPlanningSceneResponse>, IDeserializable<GetPlanningSceneRequest>
    {
        // Get parts of the planning scene that are of interest
        // All scene components are returned if none are specified
        [DataMember (Name = "components")] public PlanningSceneComponents Components;
    
        /// Constructor for empty message.
        public GetPlanningSceneRequest()
        {
            Components = new PlanningSceneComponents();
        }
        
        /// Explicit constructor.
        public GetPlanningSceneRequest(PlanningSceneComponents Components)
        {
            this.Components = Components;
        }
        
        /// Constructor with buffer.
        public GetPlanningSceneRequest(ref ReadBuffer b)
        {
            Components = new PlanningSceneComponents(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetPlanningSceneRequest(ref b);
        
        public GetPlanningSceneRequest RosDeserialize(ref ReadBuffer b) => new GetPlanningSceneRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Components.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Components is null) throw new System.NullReferenceException(nameof(Components));
            Components.RosValidate();
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetPlanningSceneResponse : IResponse, IDeserializable<GetPlanningSceneResponse>
    {
        [DataMember (Name = "scene")] public PlanningScene Scene;
    
        /// Constructor for empty message.
        public GetPlanningSceneResponse()
        {
            Scene = new PlanningScene();
        }
        
        /// Explicit constructor.
        public GetPlanningSceneResponse(PlanningScene Scene)
        {
            this.Scene = Scene;
        }
        
        /// Constructor with buffer.
        public GetPlanningSceneResponse(ref ReadBuffer b)
        {
            Scene = new PlanningScene(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetPlanningSceneResponse(ref b);
        
        public GetPlanningSceneResponse RosDeserialize(ref ReadBuffer b) => new GetPlanningSceneResponse(ref b);
    
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
}
