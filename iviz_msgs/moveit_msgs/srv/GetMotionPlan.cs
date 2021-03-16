using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/GetMotionPlan")]
    public sealed class GetMotionPlan : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetMotionPlanRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetMotionPlanResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetMotionPlan()
        {
            Request = new GetMotionPlanRequest();
            Response = new GetMotionPlanResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetMotionPlan(GetMotionPlanRequest request)
        {
            Request = request;
            Response = new GetMotionPlanResponse();
        }
        
        IService IService.Create() => new GetMotionPlan();
        
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/GetMotionPlan";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "657e571ceabcb225c850c02c2249a1e1";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMotionPlanRequest : IRequest<GetMotionPlan, GetMotionPlanResponse>, IDeserializable<GetMotionPlanRequest>
    {
        // This service contains the definition for a request to the motion
        // planner and the output it provides
        [DataMember (Name = "motion_plan_request")] public MotionPlanRequest MotionPlanRequest { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMotionPlanRequest()
        {
            MotionPlanRequest = new MotionPlanRequest();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMotionPlanRequest(MotionPlanRequest MotionPlanRequest)
        {
            this.MotionPlanRequest = MotionPlanRequest;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMotionPlanRequest(ref Buffer b)
        {
            MotionPlanRequest = new MotionPlanRequest(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMotionPlanRequest(ref b);
        }
        
        GetMotionPlanRequest IDeserializable<GetMotionPlanRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetMotionPlanRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            MotionPlanRequest.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (MotionPlanRequest is null) throw new System.NullReferenceException(nameof(MotionPlanRequest));
            MotionPlanRequest.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += MotionPlanRequest.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMotionPlanResponse : IResponse, IDeserializable<GetMotionPlanResponse>
    {
        [DataMember (Name = "motion_plan_response")] public MotionPlanResponse MotionPlanResponse { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMotionPlanResponse()
        {
            MotionPlanResponse = new MotionPlanResponse();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMotionPlanResponse(MotionPlanResponse MotionPlanResponse)
        {
            this.MotionPlanResponse = MotionPlanResponse;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMotionPlanResponse(ref Buffer b)
        {
            MotionPlanResponse = new MotionPlanResponse(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMotionPlanResponse(ref b);
        }
        
        GetMotionPlanResponse IDeserializable<GetMotionPlanResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetMotionPlanResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            MotionPlanResponse.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (MotionPlanResponse is null) throw new System.NullReferenceException(nameof(MotionPlanResponse));
            MotionPlanResponse.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += MotionPlanResponse.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
