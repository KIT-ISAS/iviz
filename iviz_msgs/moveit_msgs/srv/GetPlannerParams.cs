using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class GetPlannerParams : IService
    {
        /// Request message.
        [DataMember] public GetPlannerParamsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetPlannerParamsResponse Response { get; set; }
        
        /// Empty constructor.
        public GetPlannerParams()
        {
            Request = new GetPlannerParamsRequest();
            Response = new GetPlannerParamsResponse();
        }
        
        /// Setter constructor.
        public GetPlannerParams(GetPlannerParamsRequest request)
        {
            Request = request;
            Response = new GetPlannerParamsResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetPlannerParamsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetPlannerParamsResponse)value;
        }
        
        public const string ServiceType = "moveit_msgs/GetPlannerParams";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "b3ec1aca2b1471e3eea051c548c69810";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetPlannerParamsRequest : IRequest<GetPlannerParams, GetPlannerParamsResponse>, IDeserializable<GetPlannerParamsRequest>
    {
        // Name of planning config
        [DataMember (Name = "planner_config")] public string PlannerConfig;
        // Optional name of planning group (return global defaults if empty)
        [DataMember (Name = "group")] public string Group;
    
        /// Constructor for empty message.
        public GetPlannerParamsRequest()
        {
            PlannerConfig = "";
            Group = "";
        }
        
        /// Explicit constructor.
        public GetPlannerParamsRequest(string PlannerConfig, string Group)
        {
            this.PlannerConfig = PlannerConfig;
            this.Group = Group;
        }
        
        /// Constructor with buffer.
        public GetPlannerParamsRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out PlannerConfig);
            b.DeserializeString(out Group);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetPlannerParamsRequest(ref b);
        
        public GetPlannerParamsRequest RosDeserialize(ref ReadBuffer b) => new GetPlannerParamsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(PlannerConfig);
            b.Serialize(Group);
        }
        
        public void RosValidate()
        {
            if (PlannerConfig is null) BuiltIns.ThrowNullReference();
            if (Group is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(PlannerConfig) + BuiltIns.GetStringSize(Group);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetPlannerParamsResponse : IResponse, IDeserializable<GetPlannerParamsResponse>
    {
        // parameters as key-value pairs
        [DataMember (Name = "params")] public PlannerParams Params;
    
        /// Constructor for empty message.
        public GetPlannerParamsResponse()
        {
            Params = new PlannerParams();
        }
        
        /// Explicit constructor.
        public GetPlannerParamsResponse(PlannerParams Params)
        {
            this.Params = Params;
        }
        
        /// Constructor with buffer.
        public GetPlannerParamsResponse(ref ReadBuffer b)
        {
            Params = new PlannerParams(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetPlannerParamsResponse(ref b);
        
        public GetPlannerParamsResponse RosDeserialize(ref ReadBuffer b) => new GetPlannerParamsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Params.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Params is null) BuiltIns.ThrowNullReference();
            Params.RosValidate();
        }
    
        public int RosMessageLength => 0 + Params.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
