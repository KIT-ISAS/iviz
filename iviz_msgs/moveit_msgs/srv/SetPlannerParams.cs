using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/SetPlannerParams")]
    public sealed class SetPlannerParams : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SetPlannerParamsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SetPlannerParamsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SetPlannerParams()
        {
            Request = new SetPlannerParamsRequest();
            Response = SetPlannerParamsResponse.Singleton;
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/SetPlannerParams";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "86762d89189c5f52cda7680fdbceb1db";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetPlannerParamsRequest : IRequest<SetPlannerParams, SetPlannerParamsResponse>, IDeserializable<SetPlannerParamsRequest>
    {
        // Name of planning config
        [DataMember (Name = "planner_config")] public string PlannerConfig { get; set; }
        // Optional name of planning group (set global defaults if empty)
        [DataMember (Name = "group")] public string Group { get; set; }
        // parameters as key-value pairs
        [DataMember (Name = "params")] public PlannerParams Params { get; set; }
        // replace params or augment existing ones?
        [DataMember (Name = "replace")] public bool Replace { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SetPlannerParamsRequest()
        {
            PlannerConfig = string.Empty;
            Group = string.Empty;
            Params = new PlannerParams();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetPlannerParamsRequest(string PlannerConfig, string Group, PlannerParams Params, bool Replace)
        {
            this.PlannerConfig = PlannerConfig;
            this.Group = Group;
            this.Params = Params;
            this.Replace = Replace;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SetPlannerParamsRequest(ref Buffer b)
        {
            PlannerConfig = b.DeserializeString();
            Group = b.DeserializeString();
            Params = new PlannerParams(ref b);
            Replace = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SetPlannerParamsRequest(ref b);
        }
        
        SetPlannerParamsRequest IDeserializable<SetPlannerParamsRequest>.RosDeserialize(ref Buffer b)
        {
            return new SetPlannerParamsRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(PlannerConfig);
            b.Serialize(Group);
            Params.RosSerialize(ref b);
            b.Serialize(Replace);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(PlannerConfig);
                size += BuiltIns.UTF8.GetByteCount(Group);
                size += Params.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetPlannerParamsResponse : IResponse, IDeserializable<SetPlannerParamsResponse>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public SetPlannerParamsResponse()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SetPlannerParamsResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        SetPlannerParamsResponse IDeserializable<SetPlannerParamsResponse>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly SetPlannerParamsResponse Singleton = new SetPlannerParamsResponse();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void Dispose()
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
