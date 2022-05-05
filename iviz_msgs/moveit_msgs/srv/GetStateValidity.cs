using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class GetStateValidity : IService
    {
        /// Request message.
        [DataMember] public GetStateValidityRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetStateValidityResponse Response { get; set; }
        
        /// Empty constructor.
        public GetStateValidity()
        {
            Request = new GetStateValidityRequest();
            Response = new GetStateValidityResponse();
        }
        
        /// Setter constructor.
        public GetStateValidity(GetStateValidityRequest request)
        {
            Request = request;
            Response = new GetStateValidityResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetStateValidityRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetStateValidityResponse)value;
        }
        
        public const string ServiceType = "moveit_msgs/GetStateValidity";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "0c7c937b6a056e7ae5fded13d8e9b242";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetStateValidityRequest : IRequest<GetStateValidity, GetStateValidityResponse>, IDeserializable<GetStateValidityRequest>
    {
        [DataMember (Name = "robot_state")] public RobotState RobotState;
        [DataMember (Name = "group_name")] public string GroupName;
        [DataMember (Name = "constraints")] public Constraints Constraints;
    
        /// Constructor for empty message.
        public GetStateValidityRequest()
        {
            RobotState = new RobotState();
            GroupName = "";
            Constraints = new Constraints();
        }
        
        /// Explicit constructor.
        public GetStateValidityRequest(RobotState RobotState, string GroupName, Constraints Constraints)
        {
            this.RobotState = RobotState;
            this.GroupName = GroupName;
            this.Constraints = Constraints;
        }
        
        /// Constructor with buffer.
        public GetStateValidityRequest(ref ReadBuffer b)
        {
            RobotState = new RobotState(ref b);
            b.DeserializeString(out GroupName);
            Constraints = new Constraints(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetStateValidityRequest(ref b);
        
        public GetStateValidityRequest RosDeserialize(ref ReadBuffer b) => new GetStateValidityRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            RobotState.RosSerialize(ref b);
            b.Serialize(GroupName);
            Constraints.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (RobotState is null) BuiltIns.ThrowNullReference();
            RobotState.RosValidate();
            if (GroupName is null) BuiltIns.ThrowNullReference();
            if (Constraints is null) BuiltIns.ThrowNullReference();
            Constraints.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += RobotState.RosMessageLength;
                size += BuiltIns.GetStringSize(GroupName);
                size += Constraints.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetStateValidityResponse : IResponse, IDeserializable<GetStateValidityResponse>
    {
        [DataMember (Name = "valid")] public bool Valid;
        [DataMember (Name = "contacts")] public ContactInformation[] Contacts;
        [DataMember (Name = "cost_sources")] public CostSource[] CostSources;
        [DataMember (Name = "constraint_result")] public ConstraintEvalResult[] ConstraintResult;
    
        /// Constructor for empty message.
        public GetStateValidityResponse()
        {
            Contacts = System.Array.Empty<ContactInformation>();
            CostSources = System.Array.Empty<CostSource>();
            ConstraintResult = System.Array.Empty<ConstraintEvalResult>();
        }
        
        /// Explicit constructor.
        public GetStateValidityResponse(bool Valid, ContactInformation[] Contacts, CostSource[] CostSources, ConstraintEvalResult[] ConstraintResult)
        {
            this.Valid = Valid;
            this.Contacts = Contacts;
            this.CostSources = CostSources;
            this.ConstraintResult = ConstraintResult;
        }
        
        /// Constructor with buffer.
        public GetStateValidityResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Valid);
            b.DeserializeArray(out Contacts);
            for (int i = 0; i < Contacts.Length; i++)
            {
                Contacts[i] = new ContactInformation(ref b);
            }
            b.DeserializeArray(out CostSources);
            for (int i = 0; i < CostSources.Length; i++)
            {
                CostSources[i] = new CostSource(ref b);
            }
            b.DeserializeArray(out ConstraintResult);
            for (int i = 0; i < ConstraintResult.Length; i++)
            {
                ConstraintResult[i] = new ConstraintEvalResult(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetStateValidityResponse(ref b);
        
        public GetStateValidityResponse RosDeserialize(ref ReadBuffer b) => new GetStateValidityResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Valid);
            b.SerializeArray(Contacts);
            b.SerializeArray(CostSources);
            b.SerializeArray(ConstraintResult);
        }
        
        public void RosValidate()
        {
            if (Contacts is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Contacts.Length; i++)
            {
                if (Contacts[i] is null) BuiltIns.ThrowNullReference(nameof(Contacts), i);
                Contacts[i].RosValidate();
            }
            if (CostSources is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < CostSources.Length; i++)
            {
                if (CostSources[i] is null) BuiltIns.ThrowNullReference(nameof(CostSources), i);
                CostSources[i].RosValidate();
            }
            if (ConstraintResult is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < ConstraintResult.Length; i++)
            {
                if (ConstraintResult[i] is null) BuiltIns.ThrowNullReference(nameof(ConstraintResult), i);
                ConstraintResult[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 13;
                size += BuiltIns.GetArraySize(Contacts);
                size += 56 * CostSources.Length;
                size += 9 * ConstraintResult.Length;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
