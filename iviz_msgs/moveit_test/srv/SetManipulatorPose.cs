using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitTest
{
    [DataContract (Name = "moveit_test/SetManipulatorPose")]
    public sealed class SetManipulatorPose : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SetManipulatorPoseRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SetManipulatorPoseResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SetManipulatorPose()
        {
            Request = new SetManipulatorPoseRequest();
            Response = new SetManipulatorPoseResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetManipulatorPose(SetManipulatorPoseRequest request)
        {
            Request = request;
            Response = new SetManipulatorPoseResponse();
        }
        
        IService IService.Create() => new SetManipulatorPose();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetManipulatorPoseRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetManipulatorPoseResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_test/SetManipulatorPose";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "5ecd0f22dc847b6218fa34863c837186";
    }

    [DataContract]
    public sealed class SetManipulatorPoseRequest : IRequest<SetManipulatorPose, SetManipulatorPoseResponse>, IDeserializable<SetManipulatorPoseRequest>
    {
        [DataMember (Name = "target_pose")] public GeometryMsgs.Pose TargetPose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SetManipulatorPoseRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetManipulatorPoseRequest(in GeometryMsgs.Pose TargetPose)
        {
            this.TargetPose = TargetPose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SetManipulatorPoseRequest(ref Buffer b)
        {
            TargetPose = new GeometryMsgs.Pose(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SetManipulatorPoseRequest(ref b);
        }
        
        SetManipulatorPoseRequest IDeserializable<SetManipulatorPoseRequest>.RosDeserialize(ref Buffer b)
        {
            return new SetManipulatorPoseRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            TargetPose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 56;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class SetManipulatorPoseResponse : IResponse, IDeserializable<SetManipulatorPoseResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
        [DataMember (Name = "trajectory")] public TrajectoryMsgs.JointTrajectory Trajectory { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SetManipulatorPoseResponse()
        {
            Trajectory = new TrajectoryMsgs.JointTrajectory();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetManipulatorPoseResponse(bool Success, TrajectoryMsgs.JointTrajectory Trajectory)
        {
            this.Success = Success;
            this.Trajectory = Trajectory;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SetManipulatorPoseResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Trajectory = new TrajectoryMsgs.JointTrajectory(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SetManipulatorPoseResponse(ref b);
        }
        
        SetManipulatorPoseResponse IDeserializable<SetManipulatorPoseResponse>.RosDeserialize(ref Buffer b)
        {
            return new SetManipulatorPoseResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            Trajectory.RosSerialize(ref b);
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
}
