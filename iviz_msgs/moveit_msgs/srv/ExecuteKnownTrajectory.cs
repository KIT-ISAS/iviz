using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class ExecuteKnownTrajectory : IService
    {
        /// Request message.
        [DataMember] public ExecuteKnownTrajectoryRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ExecuteKnownTrajectoryResponse Response { get; set; }
        
        /// Empty constructor.
        public ExecuteKnownTrajectory()
        {
            Request = new ExecuteKnownTrajectoryRequest();
            Response = new ExecuteKnownTrajectoryResponse();
        }
        
        /// Setter constructor.
        public ExecuteKnownTrajectory(ExecuteKnownTrajectoryRequest request)
        {
            Request = request;
            Response = new ExecuteKnownTrajectoryResponse();
        }
        
        IService IService.Create() => new ExecuteKnownTrajectory();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ExecuteKnownTrajectoryRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ExecuteKnownTrajectoryResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/ExecuteKnownTrajectory";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "e30b86cbd13304832e894dc994795e33";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ExecuteKnownTrajectoryRequest : IRequest<ExecuteKnownTrajectory, ExecuteKnownTrajectoryResponse>, IDeserializable<ExecuteKnownTrajectoryRequest>
    {
        // This service is deprecated and will go away at some point. For new development use the ExecuteTrajectory action.
        // Effective since: Indigo 0.7.4, Jade and Kinetic 0.8.3
        // The trajectory to execute 
        [DataMember (Name = "trajectory")] public RobotTrajectory Trajectory;
        // Set this to true if the service should block until the execution is complete
        [DataMember (Name = "wait_for_execution")] public bool WaitForExecution;
    
        /// Constructor for empty message.
        public ExecuteKnownTrajectoryRequest()
        {
            Trajectory = new RobotTrajectory();
        }
        
        /// Explicit constructor.
        public ExecuteKnownTrajectoryRequest(RobotTrajectory Trajectory, bool WaitForExecution)
        {
            this.Trajectory = Trajectory;
            this.WaitForExecution = WaitForExecution;
        }
        
        /// Constructor with buffer.
        internal ExecuteKnownTrajectoryRequest(ref Buffer b)
        {
            Trajectory = new RobotTrajectory(ref b);
            WaitForExecution = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ExecuteKnownTrajectoryRequest(ref b);
        
        ExecuteKnownTrajectoryRequest IDeserializable<ExecuteKnownTrajectoryRequest>.RosDeserialize(ref Buffer b) => new ExecuteKnownTrajectoryRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Trajectory.RosSerialize(ref b);
            b.Serialize(WaitForExecution);
        }
        
        public void RosValidate()
        {
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            Trajectory.RosValidate();
        }
    
        public int RosMessageLength => 1 + Trajectory.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ExecuteKnownTrajectoryResponse : IResponse, IDeserializable<ExecuteKnownTrajectoryResponse>
    {
        // Error code - encodes the overall reason for failure
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
    
        /// Constructor for empty message.
        public ExecuteKnownTrajectoryResponse()
        {
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// Explicit constructor.
        public ExecuteKnownTrajectoryResponse(MoveItErrorCodes ErrorCode)
        {
            this.ErrorCode = ErrorCode;
        }
        
        /// Constructor with buffer.
        internal ExecuteKnownTrajectoryResponse(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ExecuteKnownTrajectoryResponse(ref b);
        
        ExecuteKnownTrajectoryResponse IDeserializable<ExecuteKnownTrajectoryResponse>.RosDeserialize(ref Buffer b) => new ExecuteKnownTrajectoryResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
