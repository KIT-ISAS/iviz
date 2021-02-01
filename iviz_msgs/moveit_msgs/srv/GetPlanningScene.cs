using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/GetPlanningScene")]
    public sealed class GetPlanningScene : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetPlanningSceneRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetPlanningSceneResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetPlanningScene()
        {
            Request = new GetPlanningSceneRequest();
            Response = new GetPlanningSceneResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/GetPlanningScene";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "2745cf315b4eb5fb00e5befa8714d64d";
    }

    [DataContract]
    public sealed class GetPlanningSceneRequest : IRequest<GetPlanningScene, GetPlanningSceneResponse>, IDeserializable<GetPlanningSceneRequest>
    {
        // Get parts of the planning scene that are of interest
        // All scene components are returned if none are specified
        [DataMember (Name = "components")] public PlanningSceneComponents Components { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetPlanningSceneRequest()
        {
            Components = new PlanningSceneComponents();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetPlanningSceneRequest(PlanningSceneComponents Components)
        {
            this.Components = Components;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetPlanningSceneRequest(ref Buffer b)
        {
            Components = new PlanningSceneComponents(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetPlanningSceneRequest(ref b);
        }
        
        GetPlanningSceneRequest IDeserializable<GetPlanningSceneRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetPlanningSceneRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
    }

    [DataContract]
    public sealed class GetPlanningSceneResponse : IResponse, IDeserializable<GetPlanningSceneResponse>
    {
        [DataMember (Name = "scene")] public PlanningScene Scene { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetPlanningSceneResponse()
        {
            Scene = new PlanningScene();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetPlanningSceneResponse(PlanningScene Scene)
        {
            this.Scene = Scene;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetPlanningSceneResponse(ref Buffer b)
        {
            Scene = new PlanningScene(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetPlanningSceneResponse(ref b);
        }
        
        GetPlanningSceneResponse IDeserializable<GetPlanningSceneResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetPlanningSceneResponse(ref b);
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
}
