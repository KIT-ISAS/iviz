using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/ExecuteKnownTrajectory")]
    public sealed class ExecuteKnownTrajectory : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ExecuteKnownTrajectoryRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ExecuteKnownTrajectoryResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ExecuteKnownTrajectory()
        {
            Request = new ExecuteKnownTrajectoryRequest();
            Response = new ExecuteKnownTrajectoryResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/ExecuteKnownTrajectory";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "e30b86cbd13304832e894dc994795e33";
    }

    [DataContract]
    public sealed class ExecuteKnownTrajectoryRequest : IRequest<ExecuteKnownTrajectory, ExecuteKnownTrajectoryResponse>, IDeserializable<ExecuteKnownTrajectoryRequest>
    {
        // This service is deprecated and will go away at some point. For new development use the ExecuteTrajectory action.
        // Effective since: Indigo 0.7.4, Jade and Kinetic 0.8.3
        // The trajectory to execute 
        [DataMember (Name = "trajectory")] public RobotTrajectory Trajectory { get; set; }
        // Set this to true if the service should block until the execution is complete
        [DataMember (Name = "wait_for_execution")] public bool WaitForExecution { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteKnownTrajectoryRequest()
        {
            Trajectory = new RobotTrajectory();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteKnownTrajectoryRequest(RobotTrajectory Trajectory, bool WaitForExecution)
        {
            this.Trajectory = Trajectory;
            this.WaitForExecution = WaitForExecution;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteKnownTrajectoryRequest(ref Buffer b)
        {
            Trajectory = new RobotTrajectory(ref b);
            WaitForExecution = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteKnownTrajectoryRequest(ref b);
        }
        
        ExecuteKnownTrajectoryRequest IDeserializable<ExecuteKnownTrajectoryRequest>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 1;
                size += Trajectory.RosMessageLength;
                return size;
            }
        }
    }

    [DataContract]
    public sealed class ExecuteKnownTrajectoryResponse : IResponse, IDeserializable<ExecuteKnownTrajectoryResponse>
    {
        // Error code - encodes the overall reason for failure
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteKnownTrajectoryResponse()
        {
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteKnownTrajectoryResponse(MoveItErrorCodes ErrorCode)
        {
            this.ErrorCode = ErrorCode;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteKnownTrajectoryResponse(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteKnownTrajectoryResponse(ref b);
        }
        
        ExecuteKnownTrajectoryResponse IDeserializable<ExecuteKnownTrajectoryResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    }
}
