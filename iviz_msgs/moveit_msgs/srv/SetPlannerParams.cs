using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class SetPlannerParams : IService
    {
        /// Request message.
        [DataMember] public SetPlannerParamsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SetPlannerParamsResponse Response { get; set; }
        
        /// Empty constructor.
        public SetPlannerParams()
        {
            Request = new SetPlannerParamsRequest();
            Response = SetPlannerParamsResponse.Singleton;
        }
        
        /// Setter constructor.
        public SetPlannerParams(SetPlannerParamsRequest request)
        {
            Request = request;
            Response = SetPlannerParamsResponse.Singleton;
        }
        
        IService IService.Create() => new SetPlannerParams();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetPlannerParamsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetPlannerParamsResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/SetPlannerParams";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "86762d89189c5f52cda7680fdbceb1db";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetPlannerParamsRequest : IRequest<SetPlannerParams, SetPlannerParamsResponse>, IDeserializable<SetPlannerParamsRequest>
    {
        // Name of planning config
        [DataMember (Name = "planner_config")] public string PlannerConfig;
        // Optional name of planning group (set global defaults if empty)
        [DataMember (Name = "group")] public string Group;
        // parameters as key-value pairs
        [DataMember (Name = "params")] public PlannerParams Params;
        // replace params or augment existing ones?
        [DataMember (Name = "replace")] public bool Replace;
    
        /// Constructor for empty message.
        public SetPlannerParamsRequest()
        {
            PlannerConfig = string.Empty;
            Group = string.Empty;
            Params = new PlannerParams();
        }
        
        /// Explicit constructor.
        public SetPlannerParamsRequest(string PlannerConfig, string Group, PlannerParams Params, bool Replace)
        {
            this.PlannerConfig = PlannerConfig;
            this.Group = Group;
            this.Params = Params;
            this.Replace = Replace;
        }
        
        /// Constructor with buffer.
        internal SetPlannerParamsRequest(ref Buffer b)
        {
            PlannerConfig = b.DeserializeString();
            Group = b.DeserializeString();
            Params = new PlannerParams(ref b);
            Replace = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new SetPlannerParamsRequest(ref b);
        
        SetPlannerParamsRequest IDeserializable<SetPlannerParamsRequest>.RosDeserialize(ref Buffer b) => new SetPlannerParamsRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(PlannerConfig);
            b.Serialize(Group);
            Params.RosSerialize(ref b);
            b.Serialize(Replace);
        }
        
        public void RosValidate()
        {
            if (PlannerConfig is null) throw new System.NullReferenceException(nameof(PlannerConfig));
            if (Group is null) throw new System.NullReferenceException(nameof(Group));
            if (Params is null) throw new System.NullReferenceException(nameof(Params));
            Params.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 9;
                size += BuiltIns.GetStringSize(PlannerConfig);
                size += BuiltIns.GetStringSize(Group);
                size += Params.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetPlannerParamsResponse : IResponse, IDeserializable<SetPlannerParamsResponse>
    {
    
        /// Constructor for empty message.
        public SetPlannerParamsResponse()
        {
        }
        
        /// Constructor with buffer.
        internal SetPlannerParamsResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        SetPlannerParamsResponse IDeserializable<SetPlannerParamsResponse>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly SetPlannerParamsResponse Singleton = new SetPlannerParamsResponse();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
