using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/GetPositionIK")]
    public sealed class GetPositionIK : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetPositionIKRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetPositionIKResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetPositionIK()
        {
            Request = new GetPositionIKRequest();
            Response = new GetPositionIKResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/GetPositionIK";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "0661ea3324398c69f5a971d0ec55657e";
    }

    [DataContract]
    public sealed class GetPositionIKRequest : IRequest<GetPositionIK, GetPositionIKResponse>, IDeserializable<GetPositionIKRequest>
    {
        // A service call to carry out an inverse kinematics computation
        // The inverse kinematics request
        [DataMember (Name = "ik_request")] public PositionIKRequest IkRequest { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetPositionIKRequest()
        {
            IkRequest = new PositionIKRequest();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetPositionIKRequest(PositionIKRequest IkRequest)
        {
            this.IkRequest = IkRequest;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetPositionIKRequest(ref Buffer b)
        {
            IkRequest = new PositionIKRequest(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetPositionIKRequest(ref b);
        }
        
        GetPositionIKRequest IDeserializable<GetPositionIKRequest>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            IkRequest.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (IkRequest is null) throw new System.NullReferenceException(nameof(IkRequest));
            IkRequest.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += IkRequest.RosMessageLength;
                return size;
            }
        }
    }

    [DataContract]
    public sealed class GetPositionIKResponse : IResponse, IDeserializable<GetPositionIKResponse>
    {
        // The returned solution 
        // (in the same order as the list of joints specified in the IKRequest message)
        [DataMember (Name = "solution")] public RobotState Solution { get; set; }
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetPositionIKResponse()
        {
            Solution = new RobotState();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetPositionIKResponse(RobotState Solution, MoveItErrorCodes ErrorCode)
        {
            this.Solution = Solution;
            this.ErrorCode = ErrorCode;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetPositionIKResponse(ref Buffer b)
        {
            Solution = new RobotState(ref b);
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetPositionIKResponse(ref b);
        }
        
        GetPositionIKResponse IDeserializable<GetPositionIKResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Solution.RosMessageLength;
                return size;
            }
        }
    }
}
