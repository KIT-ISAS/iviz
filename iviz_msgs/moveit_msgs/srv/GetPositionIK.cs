using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "moveit_msgs/GetPositionIK";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "0661ea3324398c69f5a971d0ec55657e";
        
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
        public GetPositionIKRequest(ref ReadBuffer b)
        {
            IkRequest = new PositionIKRequest(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetPositionIKRequest(ref b);
        
        public GetPositionIKRequest RosDeserialize(ref ReadBuffer b) => new GetPositionIKRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            IkRequest.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (IkRequest is null) BuiltIns.ThrowNullReference();
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
        public GetPositionIKResponse(ref ReadBuffer b)
        {
            Solution = new RobotState(ref b);
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetPositionIKResponse(ref b);
        
        public GetPositionIKResponse RosDeserialize(ref ReadBuffer b) => new GetPositionIKResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Solution.RosSerialize(ref b);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Solution is null) BuiltIns.ThrowNullReference();
            Solution.RosValidate();
            if (ErrorCode is null) BuiltIns.ThrowNullReference();
            ErrorCode.RosValidate();
        }
    
        public int RosMessageLength => 4 + Solution.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
