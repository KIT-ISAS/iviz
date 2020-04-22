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
        
            public int GetLength()
            {
                int size = 4;
                size += start.GetLength();
                size += goal.GetLength();
                return size;
            }
        
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
        
            public Response Call(IServiceCaller caller)
            {
                GetPlan s = new GetPlan(this);
                caller.Call(s);
                return s.response;
            }
        }

        public sealed class Response : IResponse
        {
            public nav_msgs.Path plan;
        
            public int GetLength()
            {
                int size = 0;
                size += plan.GetLength();
                return size;
            }
        
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
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "nav_msgs/GetPlan";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "421c8ea4d21c6c9db7054b4bbdf1e024";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACr1VwWrbQBC9L/gfBnxIUmIX2tKDoYdAaZpDISW5lRLG0shaWO0quyvb6tf37SqS45a0" +
            "PTQxAkurnbfz5r0ZzelSIjG1hi1V3jUUa6Gi815spNYFHbWzFF1e3zg2dO2CkFJzusVKiOzzPqHK+bwp" +
            "QamNuEai7++asAmvU8hN5KaVcogYwyttgZjDXXU4Yjz3DzBpX0K5ehSmA7l1iL4ropTnVLsdNWx7AoT4" +
            "MCVnxVMBuoj2Yng/UHYWkazBWlvaE9uSeloLWCFN1kbbzZJUZRzHt29QECOebSFqsVgoy9uHDDnWQwFm" +
            "6sN//s3Ul5vLFT1Zkhn4XAzq7DTS8FIJVCwSN+dLVDqCiedGMrmoG4EWTas+C5coSZ3/VAZIijwfhRDL" +
            "Ifvh5JQ4ONiSfZm04pIjZzvVelOLXxjZiknGycrnt7FvJSyzi6A6ro1AVjampy5gEwxbuKbprC4S7Yns" +
            "GI9IyAzfw4y66Az736qU0HEFue9yFa8+rrJLpOiiRkI9EAovHOAMvCTVwTywBgLU/HbnFniUDQo7HQ6j" +
            "MewVSPatl5Dy5LDCGa8GcktgozqCU8pAp3ntDo/hjHAIUpDWFTWdIvPrPtapL+HcLXvNayMJuEAFgHqS" +
            "gk7OHiGntFdk2boRfkA8nPEvsHbCTZwWNTRLfUGh26CA2Nh6t9Ultq77oauMTnPE6LVn36sUNRyp5p+y" +
            "E2OSLyuCfw7BFRoClNnBCg2Z0LMad7p8yZ4amslL0gkMOE9BjKg8qlCoyguYtFzIeTJaWi4f3g8TM7WY" +
            "83qMxei4dmm4TKPtawei3mbcw76X44hkZmP/wBERo+9hRI4UQAcNkrM+YjwMwffvaD/d9dPdj5dicKjf" +
            "RGOSC1Y6qupx/unp/lB9DJpmqf5CarzbPR+9o48IOF3ARN5zP9ouqYPx8Zgk5e9NmpRM3q1dTHOvcsa4" +
            "3S9T/cmvxrfvAzh4/QTn44CeDAgAAA==";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public GetPlan()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetPlan(Request request)
        {
            this.request = request;
        }
        
        public IResponse CreateResponse() => new Response();
        
        public IRequest GetRequest() => request;
        
        public void SetResponse(IResponse response)
        {
            this.response = (Response)response;
        }
    }

}
