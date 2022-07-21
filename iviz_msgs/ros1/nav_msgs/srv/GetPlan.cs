using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetPlan : IService
    {
        /// Request message.
        [DataMember] public GetPlanRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetPlanResponse Response { get; set; }
        
        /// Empty constructor.
        public GetPlan()
        {
            Request = new GetPlanRequest();
            Response = new GetPlanResponse();
        }
        
        /// Setter constructor.
        public GetPlan(GetPlanRequest request)
        {
            Request = request;
            Response = new GetPlanResponse();
        }
        
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
        
        public const string ServiceType = "nav_msgs/GetPlan";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "421c8ea4d21c6c9db7054b4bbdf1e024";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetPlanRequest : IRequest<GetPlan, GetPlanResponse>, IDeserializableRos1<GetPlanRequest>
    {
        // Get a plan from the current position to the goal Pose 
        // The start pose for the plan
        [DataMember (Name = "start")] public GeometryMsgs.PoseStamped Start;
        // The final pose of the goal position
        [DataMember (Name = "goal")] public GeometryMsgs.PoseStamped Goal;
        // If the goal is obstructed, how many meters the planner can 
        // relax the constraint in x and y before failing. 
        [DataMember (Name = "tolerance")] public float Tolerance;
    
        public GetPlanRequest()
        {
            Start = new GeometryMsgs.PoseStamped();
            Goal = new GeometryMsgs.PoseStamped();
        }
        
        public GetPlanRequest(GeometryMsgs.PoseStamped Start, GeometryMsgs.PoseStamped Goal, float Tolerance)
        {
            this.Start = Start;
            this.Goal = Goal;
            this.Tolerance = Tolerance;
        }
        
        public GetPlanRequest(ref ReadBuffer b)
        {
            Start = new GeometryMsgs.PoseStamped(ref b);
            Goal = new GeometryMsgs.PoseStamped(ref b);
            b.Deserialize(out Tolerance);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetPlanRequest(ref b);
        
        public GetPlanRequest RosDeserialize(ref ReadBuffer b) => new GetPlanRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Start.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
            b.Serialize(Tolerance);
        }
        
        public void RosValidate()
        {
            if (Start is null) BuiltIns.ThrowNullReference();
            Start.RosValidate();
            if (Goal is null) BuiltIns.ThrowNullReference();
            Goal.RosValidate();
        }
    
        public int RosMessageLength => 4 + Start.RosMessageLength + Goal.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetPlanResponse : IResponse, IDeserializableRos1<GetPlanResponse>
    {
        [DataMember (Name = "plan")] public NavMsgs.Path Plan;
    
        public GetPlanResponse()
        {
            Plan = new NavMsgs.Path();
        }
        
        public GetPlanResponse(NavMsgs.Path Plan)
        {
            this.Plan = Plan;
        }
        
        public GetPlanResponse(ref ReadBuffer b)
        {
            Plan = new NavMsgs.Path(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetPlanResponse(ref b);
        
        public GetPlanResponse RosDeserialize(ref ReadBuffer b) => new GetPlanResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Plan.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Plan is null) BuiltIns.ThrowNullReference();
            Plan.RosValidate();
        }
    
        public int RosMessageLength => 0 + Plan.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
