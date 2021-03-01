using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/GetStateValidity")]
    public sealed class GetStateValidity : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetStateValidityRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetStateValidityResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetStateValidity()
        {
            Request = new GetStateValidityRequest();
            Response = new GetStateValidityResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetStateValidity(GetStateValidityRequest request)
        {
            Request = request;
            Response = new GetStateValidityResponse();
        }
        
        IService IService.Create() => new GetStateValidity();
        
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/GetStateValidity";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "0c7c937b6a056e7ae5fded13d8e9b242";
    }

    [DataContract]
    public sealed class GetStateValidityRequest : IRequest<GetStateValidity, GetStateValidityResponse>, IDeserializable<GetStateValidityRequest>
    {
        [DataMember (Name = "robot_state")] public RobotState RobotState { get; set; }
        [DataMember (Name = "group_name")] public string GroupName { get; set; }
        [DataMember (Name = "constraints")] public Constraints Constraints { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetStateValidityRequest()
        {
            RobotState = new RobotState();
            GroupName = string.Empty;
            Constraints = new Constraints();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetStateValidityRequest(RobotState RobotState, string GroupName, Constraints Constraints)
        {
            this.RobotState = RobotState;
            this.GroupName = GroupName;
            this.Constraints = Constraints;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetStateValidityRequest(ref Buffer b)
        {
            RobotState = new RobotState(ref b);
            GroupName = b.DeserializeString();
            Constraints = new Constraints(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetStateValidityRequest(ref b);
        }
        
        GetStateValidityRequest IDeserializable<GetStateValidityRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetStateValidityRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            RobotState.RosSerialize(ref b);
            b.Serialize(GroupName);
            Constraints.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (RobotState is null) throw new System.NullReferenceException(nameof(RobotState));
            RobotState.RosValidate();
            if (GroupName is null) throw new System.NullReferenceException(nameof(GroupName));
            if (Constraints is null) throw new System.NullReferenceException(nameof(Constraints));
            Constraints.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += RobotState.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(GroupName);
                size += Constraints.RosMessageLength;
                return size;
            }
        }
    }

    [DataContract]
    public sealed class GetStateValidityResponse : IResponse, IDeserializable<GetStateValidityResponse>
    {
        [DataMember (Name = "valid")] public bool Valid { get; set; }
        [DataMember (Name = "contacts")] public ContactInformation[] Contacts { get; set; }
        [DataMember (Name = "cost_sources")] public CostSource[] CostSources { get; set; }
        [DataMember (Name = "constraint_result")] public ConstraintEvalResult[] ConstraintResult { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetStateValidityResponse()
        {
            Contacts = System.Array.Empty<ContactInformation>();
            CostSources = System.Array.Empty<CostSource>();
            ConstraintResult = System.Array.Empty<ConstraintEvalResult>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetStateValidityResponse(bool Valid, ContactInformation[] Contacts, CostSource[] CostSources, ConstraintEvalResult[] ConstraintResult)
        {
            this.Valid = Valid;
            this.Contacts = Contacts;
            this.CostSources = CostSources;
            this.ConstraintResult = ConstraintResult;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetStateValidityResponse(ref Buffer b)
        {
            Valid = b.Deserialize<bool>();
            Contacts = b.DeserializeArray<ContactInformation>();
            for (int i = 0; i < Contacts.Length; i++)
            {
                Contacts[i] = new ContactInformation(ref b);
            }
            CostSources = b.DeserializeArray<CostSource>();
            for (int i = 0; i < CostSources.Length; i++)
            {
                CostSources[i] = new CostSource(ref b);
            }
            ConstraintResult = b.DeserializeArray<ConstraintEvalResult>();
            for (int i = 0; i < ConstraintResult.Length; i++)
            {
                ConstraintResult[i] = new ConstraintEvalResult(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetStateValidityResponse(ref b);
        }
        
        GetStateValidityResponse IDeserializable<GetStateValidityResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetStateValidityResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Valid);
            b.SerializeArray(Contacts, 0);
            b.SerializeArray(CostSources, 0);
            b.SerializeArray(ConstraintResult, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Contacts is null) throw new System.NullReferenceException(nameof(Contacts));
            for (int i = 0; i < Contacts.Length; i++)
            {
                if (Contacts[i] is null) throw new System.NullReferenceException($"{nameof(Contacts)}[{i}]");
                Contacts[i].RosValidate();
            }
            if (CostSources is null) throw new System.NullReferenceException(nameof(CostSources));
            for (int i = 0; i < CostSources.Length; i++)
            {
                if (CostSources[i] is null) throw new System.NullReferenceException($"{nameof(CostSources)}[{i}]");
                CostSources[i].RosValidate();
            }
            if (ConstraintResult is null) throw new System.NullReferenceException(nameof(ConstraintResult));
            for (int i = 0; i < ConstraintResult.Length; i++)
            {
                if (ConstraintResult[i] is null) throw new System.NullReferenceException($"{nameof(ConstraintResult)}[{i}]");
                ConstraintResult[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 13;
                foreach (var i in Contacts)
                {
                    size += i.RosMessageLength;
                }
                size += 56 * CostSources.Length;
                size += 9 * ConstraintResult.Length;
                return size;
            }
        }
    }
}
