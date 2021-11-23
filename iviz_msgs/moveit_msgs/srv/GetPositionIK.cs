using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetPositionIK : IService
    {
        /// Request message.
        [DataMember] public GetPositionIKRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetPositionIKResponse Response { get; set; }
        
        /// Empty constructor.
        public GetPositionIK()
        {
            Request = new GetPositionIKRequest();
            Response = new GetPositionIKResponse();
        }
        
        /// Setter constructor.
        public GetPositionIK(GetPositionIKRequest request)
        {
            Request = request;
            Response = new GetPositionIKResponse();
        }
        
        IService IService.Create() => new GetPositionIK();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetPositionIKRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetPositionIKResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/GetPositionIK";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "0661ea3324398c69f5a971d0ec55657e";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetPositionIKRequest : IRequest<GetPositionIK, GetPositionIKResponse>, IDeserializable<GetPositionIKRequest>
    {
        // A service call to carry out an inverse kinematics computation
        // The inverse kinematics request
        [DataMember (Name = "ik_request")] public PositionIKRequest IkRequest;
    
        /// Constructor for empty message.
        public GetPositionIKRequest()
        {
            IkRequest = new PositionIKRequest();
        }
        
        /// Explicit constructor.
        public GetPositionIKRequest(PositionIKRequest IkRequest)
        {
            this.IkRequest = IkRequest;
        }
        
        /// Constructor with buffer.
        internal GetPositionIKRequest(ref Buffer b)
        {
            IkRequest = new PositionIKRequest(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetPositionIKRequest(ref b);
        
        GetPositionIKRequest IDeserializable<GetPositionIKRequest>.RosDeserialize(ref Buffer b) => new GetPositionIKRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            IkRequest.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (IkRequest is null) throw new System.NullReferenceException(nameof(IkRequest));
            IkRequest.RosValidate();
        }
    
        public int RosMessageLength => 0 + IkRequest.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetPositionIKResponse : IResponse, IDeserializable<GetPositionIKResponse>
    {
        // The returned solution 
        // (in the same order as the list of joints specified in the IKRequest message)
        [DataMember (Name = "solution")] public RobotState Solution;
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
    
        /// Constructor for empty message.
        public GetPositionIKResponse()
        {
            Solution = new RobotState();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// Explicit constructor.
        public GetPositionIKResponse(RobotState Solution, MoveItErrorCodes ErrorCode)
        {
            this.Solution = Solution;
            this.ErrorCode = ErrorCode;
        }
        
        /// Constructor with buffer.
        internal GetPositionIKResponse(ref Buffer b)
        {
            Solution = new RobotState(ref b);
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetPositionIKResponse(ref b);
        
        GetPositionIKResponse IDeserializable<GetPositionIKResponse>.RosDeserialize(ref Buffer b) => new GetPositionIKResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Solution.RosSerialize(ref b);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Solution is null) throw new System.NullReferenceException(nameof(Solution));
            Solution.RosValidate();
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        public int RosMessageLength => 4 + Solution.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
