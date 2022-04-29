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
            PlannerConfig = "";
            Group = "";
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
        public SetPlannerParamsRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out PlannerConfig);
            b.DeserializeString(out Group);
            Params = new PlannerParams(ref b);
            b.Deserialize(out Replace);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SetPlannerParamsRequest(ref b);
        
        public SetPlannerParamsRequest RosDeserialize(ref ReadBuffer b) => new SetPlannerParamsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(PlannerConfig);
            b.Serialize(Group);
            Params.RosSerialize(ref b);
            b.Serialize(Replace);
        }
        
        public void RosValidate()
        {
            if (PlannerConfig is null) BuiltIns.ThrowNullReference();
            if (Group is null) BuiltIns.ThrowNullReference();
            if (Params is null) BuiltIns.ThrowNullReference();
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
        public SetPlannerParamsResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public SetPlannerParamsResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static SetPlannerParamsResponse? singleton;
        public static SetPlannerParamsResponse Singleton => singleton ??= new SetPlannerParamsResponse();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
