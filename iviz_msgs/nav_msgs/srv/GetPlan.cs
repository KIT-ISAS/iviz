using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract (Name = "nav_msgs/GetPlan")]
    public sealed class GetPlan : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetPlanRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetPlanResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetPlan()
        {
            Request = new GetPlanRequest();
            Response = new GetPlanResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetPlan(GetPlanRequest request)
        {
            Request = request;
            Response = new GetPlanResponse();
        }
        
        IService IService.Create() => new GetPlan();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetPlanRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetPlanResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "nav_msgs/GetPlan";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "421c8ea4d21c6c9db7054b4bbdf1e024";
    }

    public sealed class GetPlanRequest : IRequest
    {
        // Get a plan from the current position to the goal Pose 
        // The start pose for the plan
        [DataMember (Name = "start")] public GeometryMsgs.PoseStamped Start { get; set; }
        // The final pose of the goal position
        [DataMember (Name = "goal")] public GeometryMsgs.PoseStamped Goal { get; set; }
        // If the goal is obstructed, how many meters the planner can 
        // relax the constraint in x and y before failing. 
        [DataMember (Name = "tolerance")] public float Tolerance { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetPlanRequest()
        {
            Start = new GeometryMsgs.PoseStamped();
            Goal = new GeometryMsgs.PoseStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetPlanRequest(GeometryMsgs.PoseStamped Start, GeometryMsgs.PoseStamped Goal, float Tolerance)
        {
            this.Start = Start;
            this.Goal = Goal;
            this.Tolerance = Tolerance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetPlanRequest(Buffer b)
        {
            Start = new GeometryMsgs.PoseStamped(b);
            Goal = new GeometryMsgs.PoseStamped(b);
            Tolerance = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetPlanRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Start.RosSerialize(b);
            Goal.RosSerialize(b);
            b.Serialize(Tolerance);
        }
        
        public void RosValidate()
        {
            if (Start is null) throw new System.NullReferenceException(nameof(Start));
            Start.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Start.RosMessageLength;
                size += Goal.RosMessageLength;
                return size;
            }
        }
    }

    public sealed class GetPlanResponse : IResponse
    {
        [DataMember (Name = "plan")] public NavMsgs.Path Plan { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetPlanResponse()
        {
            Plan = new NavMsgs.Path();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetPlanResponse(NavMsgs.Path Plan)
        {
            this.Plan = Plan;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetPlanResponse(Buffer b)
        {
            Plan = new NavMsgs.Path(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetPlanResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Plan.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (Plan is null) throw new System.NullReferenceException(nameof(Plan));
            Plan.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Plan.RosMessageLength;
                return size;
            }
        }
    }
}
