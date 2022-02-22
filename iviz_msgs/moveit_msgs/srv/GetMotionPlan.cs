using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetMotionPlan : IService
    {
        /// Request message.
        [DataMember] public GetMotionPlanRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetMotionPlanResponse Response { get; set; }
        
        /// Empty constructor.
        public GetMotionPlan()
        {
            Request = new GetMotionPlanRequest();
            Response = new GetMotionPlanResponse();
        }
        
        /// Setter constructor.
        public GetMotionPlan(GetMotionPlanRequest request)
        {
            Request = request;
            Response = new GetMotionPlanResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetMotionPlanRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetMotionPlanResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/GetMotionPlan";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "657e571ceabcb225c850c02c2249a1e1";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMotionPlanRequest : IRequest<GetMotionPlan, GetMotionPlanResponse>, IDeserializable<GetMotionPlanRequest>
    {
        // This service contains the definition for a request to the motion
        // planner and the output it provides
        [DataMember (Name = "motion_plan_request")] public MotionPlanRequest MotionPlanRequest;
    
        /// Constructor for empty message.
        public GetMotionPlanRequest()
        {
            MotionPlanRequest = new MotionPlanRequest();
        }
        
        /// Explicit constructor.
        public GetMotionPlanRequest(MotionPlanRequest MotionPlanRequest)
        {
            this.MotionPlanRequest = MotionPlanRequest;
        }
        
        /// Constructor with buffer.
        public GetMotionPlanRequest(ref ReadBuffer b)
        {
            MotionPlanRequest = new MotionPlanRequest(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetMotionPlanRequest(ref b);
        
        public GetMotionPlanRequest RosDeserialize(ref ReadBuffer b) => new GetMotionPlanRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            MotionPlanRequest.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MotionPlanRequest is null) BuiltIns.ThrowNullReference(nameof(MotionPlanRequest));
            MotionPlanRequest.RosValidate();
        }
    
        public int RosMessageLength => 0 + MotionPlanRequest.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMotionPlanResponse : IResponse, IDeserializable<GetMotionPlanResponse>
    {
        [DataMember (Name = "motion_plan_response")] public MotionPlanResponse MotionPlanResponse;
    
        /// Constructor for empty message.
        public GetMotionPlanResponse()
        {
            MotionPlanResponse = new MotionPlanResponse();
        }
        
        /// Explicit constructor.
        public GetMotionPlanResponse(MotionPlanResponse MotionPlanResponse)
        {
            this.MotionPlanResponse = MotionPlanResponse;
        }
        
        /// Constructor with buffer.
        public GetMotionPlanResponse(ref ReadBuffer b)
        {
            MotionPlanResponse = new MotionPlanResponse(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetMotionPlanResponse(ref b);
        
        public GetMotionPlanResponse RosDeserialize(ref ReadBuffer b) => new GetMotionPlanResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            MotionPlanResponse.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MotionPlanResponse is null) BuiltIns.ThrowNullReference(nameof(MotionPlanResponse));
            MotionPlanResponse.RosValidate();
        }
    
        public int RosMessageLength => 0 + MotionPlanResponse.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
