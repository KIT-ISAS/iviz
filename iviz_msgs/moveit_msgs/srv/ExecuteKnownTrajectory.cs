using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "moveit_msgs/ExecuteKnownTrajectory";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "e30b86cbd13304832e894dc994795e33";
        
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
        public ExecuteKnownTrajectoryRequest(ref ReadBuffer b)
        {
            Trajectory = new RobotTrajectory(ref b);
            b.Deserialize(out WaitForExecution);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ExecuteKnownTrajectoryRequest(ref b);
        
        public ExecuteKnownTrajectoryRequest RosDeserialize(ref ReadBuffer b) => new ExecuteKnownTrajectoryRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Trajectory.RosSerialize(ref b);
            b.Serialize(WaitForExecution);
        }
        
        public void RosValidate()
        {
            if (Trajectory is null) BuiltIns.ThrowNullReference();
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
        public ExecuteKnownTrajectoryResponse(ref ReadBuffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ExecuteKnownTrajectoryResponse(ref b);
        
        public ExecuteKnownTrajectoryResponse RosDeserialize(ref ReadBuffer b) => new ExecuteKnownTrajectoryResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) BuiltIns.ThrowNullReference();
            ErrorCode.RosValidate();
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
