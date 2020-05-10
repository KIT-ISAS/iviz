using System.Runtime.Serialization;

namespace Iviz.Msgs.nav_msgs
{
    public sealed class GetPlan : IService
    {
        /// <summary> Request message. </summary>
        public GetPlanRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public GetPlanResponse Response { get; set; }
        
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
        
        public IService Create() => new GetPlan();
        
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
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "nav_msgs/GetPlan";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "421c8ea4d21c6c9db7054b4bbdf1e024";
    }

    public sealed class GetPlanRequest : IRequest
    {
        // Get a plan from the current position to the goal Pose 
        
        // The start pose for the plan
        public geometry_msgs.PoseStamped start { get; set; }
        
        // The final pose of the goal position
        public geometry_msgs.PoseStamped goal { get; set; }
        
        // If the goal is obstructed, how many meters the planner can 
        // relax the constraint in x and y before failing. 
        public float tolerance { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetPlanRequest()
        {
            start = new geometry_msgs.PoseStamped();
            goal = new geometry_msgs.PoseStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetPlanRequest(geometry_msgs.PoseStamped start, geometry_msgs.PoseStamped goal, float tolerance)
        {
            this.start = start ?? throw new System.ArgumentNullException(nameof(start));
            this.goal = goal ?? throw new System.ArgumentNullException(nameof(goal));
            this.tolerance = tolerance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetPlanRequest(Buffer b)
        {
            this.start = new geometry_msgs.PoseStamped(b);
            this.goal = new geometry_msgs.PoseStamped(b);
            this.tolerance = b.Deserialize<float>();
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetPlanRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.start.Serialize(b);
            this.goal.Serialize(b);
            b.Serialize(this.tolerance);
        }
        
        public void Validate()
        {
            if (start is null) throw new System.NullReferenceException();
            if (goal is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += start.RosMessageLength;
                size += goal.RosMessageLength;
                return size;
            }
        }
    }

    public sealed class GetPlanResponse : IResponse
    {
        public nav_msgs.Path plan { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetPlanResponse()
        {
            plan = new nav_msgs.Path();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetPlanResponse(nav_msgs.Path plan)
        {
            this.plan = plan ?? throw new System.ArgumentNullException(nameof(plan));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetPlanResponse(Buffer b)
        {
            this.plan = new nav_msgs.Path(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetPlanResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.plan.Serialize(b);
        }
        
        public void Validate()
        {
            if (plan is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += plan.RosMessageLength;
                return size;
            }
        }
    }
}
