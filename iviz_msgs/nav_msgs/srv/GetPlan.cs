namespace Iviz.Msgs.nav_msgs
{
    public class GetPlan : IService
    {
        public sealed class Request : IRequest
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
            public Request()
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
        
            public int GetLength()
            {
                int size = 4;
                size += start.GetLength();
                size += goal.GetLength();
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            public nav_msgs.Path plan;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
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
        
            public int GetLength()
            {
                int size = 0;
                size += plan.GetLength();
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "nav_msgs/GetPlan";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "421c8ea4d21c6c9db7054b4bbdf1e024";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public GetPlan()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetPlan(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new GetPlan();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
