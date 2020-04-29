using System.Runtime.Serialization;

namespace Iviz.Msgs.nav_msgs
{
    public sealed class GetPlan : IService
    {
        /// <summary> Request message. </summary>
        public GetPlanRequest Request { get; }
        
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
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
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
        public geometry_msgs.PoseStamped start;
        
        // The final pose of the goal position
        public geometry_msgs.PoseStamped goal;
        
        // If the goal is obstructed, how many meters the planner can 
        // relax the constraint in x and y before failing. 
        public float tolerance;
    
        /// <summary> Constructor for empty message. </summary>
        public GetPlanRequest()
        {
            start = new geometry_msgs.PoseStamped();
            goal = new geometry_msgs.PoseStamped();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            start.Deserialize(ref ptr, end);
            goal.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out tolerance, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            start.Serialize(ref ptr, end);
            goal.Serialize(ref ptr, end);
            BuiltIns.Serialize(tolerance, ref ptr, end);
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
        public nav_msgs.Path plan;
    
        /// <summary> Constructor for empty message. </summary>
        public GetPlanResponse()
        {
            plan = new nav_msgs.Path();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            plan.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            plan.Serialize(ref ptr, end);
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
