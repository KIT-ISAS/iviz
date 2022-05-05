using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class GetCartesianPath : IService
    {
        /// Request message.
        [DataMember] public GetCartesianPathRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetCartesianPathResponse Response { get; set; }
        
        /// Empty constructor.
        public GetCartesianPath()
        {
            Request = new GetCartesianPathRequest();
            Response = new GetCartesianPathResponse();
        }
        
        /// Setter constructor.
        public GetCartesianPath(GetCartesianPathRequest request)
        {
            Request = request;
            Response = new GetCartesianPathResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetCartesianPathRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetCartesianPathResponse)value;
        }
        
        public const string ServiceType = "moveit_msgs/GetCartesianPath";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "5c9a54219f0d91a804e7670bc0e118f1";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetCartesianPathRequest : IRequest<GetCartesianPath, GetCartesianPathResponse>, IDeserializable<GetCartesianPathRequest>
    {
        // Define the frame for the specified waypoints
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The start at which to start the Cartesian path
        [DataMember (Name = "start_state")] public RobotState StartState;
        // Mandatory name of group to compute the path for
        [DataMember (Name = "group_name")] public string GroupName;
        // Optional name of IK link for which waypoints are specified.
        // If not specified, the tip of the group (which is assumed to be a chain)
        // is assumed to be the link
        [DataMember (Name = "link_name")] public string LinkName;
        // A sequence of waypoints to be followed by the specified link, 
        // while moving the specified group, such that the group moves only
        // in a straight line between waypoints
        [DataMember (Name = "waypoints")] public GeometryMsgs.Pose[] Waypoints;
        // The maximum distance (in Cartesian space) between consecutive points
        // in the returned path. This must always be specified and > 0
        [DataMember (Name = "max_step")] public double MaxStep;
        // If above 0, this value is assumed to be the maximum allowed distance 
        // (L infinity) in configuration space, between consecutive points.
        // If this distance is found to be above the maximum threshold, the path 
        // computation fails.
        [DataMember (Name = "jump_threshold")] public double JumpThreshold;
        // Set to true if collisions should be avoided when possible
        [DataMember (Name = "avoid_collisions")] public bool AvoidCollisions;
        // Specify additional constraints to be met by the Cartesian path
        [DataMember (Name = "path_constraints")] public Constraints PathConstraints;
    
        /// Constructor for empty message.
        public GetCartesianPathRequest()
        {
            StartState = new RobotState();
            GroupName = "";
            LinkName = "";
            Waypoints = System.Array.Empty<GeometryMsgs.Pose>();
            PathConstraints = new Constraints();
        }
        
        /// Constructor with buffer.
        public GetCartesianPathRequest(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            StartState = new RobotState(ref b);
            b.DeserializeString(out GroupName);
            b.DeserializeString(out LinkName);
            b.DeserializeStructArray(out Waypoints);
            b.Deserialize(out MaxStep);
            b.Deserialize(out JumpThreshold);
            b.Deserialize(out AvoidCollisions);
            PathConstraints = new Constraints(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetCartesianPathRequest(ref b);
        
        public GetCartesianPathRequest RosDeserialize(ref ReadBuffer b) => new GetCartesianPathRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            StartState.RosSerialize(ref b);
            b.Serialize(GroupName);
            b.Serialize(LinkName);
            b.SerializeStructArray(Waypoints);
            b.Serialize(MaxStep);
            b.Serialize(JumpThreshold);
            b.Serialize(AvoidCollisions);
            PathConstraints.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (StartState is null) BuiltIns.ThrowNullReference();
            StartState.RosValidate();
            if (GroupName is null) BuiltIns.ThrowNullReference();
            if (LinkName is null) BuiltIns.ThrowNullReference();
            if (Waypoints is null) BuiltIns.ThrowNullReference();
            if (PathConstraints is null) BuiltIns.ThrowNullReference();
            PathConstraints.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 29;
                size += Header.RosMessageLength;
                size += StartState.RosMessageLength;
                size += BuiltIns.GetStringSize(GroupName);
                size += BuiltIns.GetStringSize(LinkName);
                size += 56 * Waypoints.Length;
                size += PathConstraints.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetCartesianPathResponse : IResponse, IDeserializable<GetCartesianPathResponse>
    {
        // The state at which the computed path starts
        [DataMember (Name = "start_state")] public RobotState StartState;
        // The computed solution trajectory, for the desired group, in configuration space
        [DataMember (Name = "solution")] public RobotTrajectory Solution;
        // If the computation was incomplete, this value indicates the fraction of the path
        // that was in fact computed (nr of waypoints traveled through)
        [DataMember (Name = "fraction")] public double Fraction;
        // The error code of the computation
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
    
        /// Constructor for empty message.
        public GetCartesianPathResponse()
        {
            StartState = new RobotState();
            Solution = new RobotTrajectory();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// Explicit constructor.
        public GetCartesianPathResponse(RobotState StartState, RobotTrajectory Solution, double Fraction, MoveItErrorCodes ErrorCode)
        {
            this.StartState = StartState;
            this.Solution = Solution;
            this.Fraction = Fraction;
            this.ErrorCode = ErrorCode;
        }
        
        /// Constructor with buffer.
        public GetCartesianPathResponse(ref ReadBuffer b)
        {
            StartState = new RobotState(ref b);
            Solution = new RobotTrajectory(ref b);
            b.Deserialize(out Fraction);
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetCartesianPathResponse(ref b);
        
        public GetCartesianPathResponse RosDeserialize(ref ReadBuffer b) => new GetCartesianPathResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            StartState.RosSerialize(ref b);
            Solution.RosSerialize(ref b);
            b.Serialize(Fraction);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (StartState is null) BuiltIns.ThrowNullReference();
            StartState.RosValidate();
            if (Solution is null) BuiltIns.ThrowNullReference();
            Solution.RosValidate();
            if (ErrorCode is null) BuiltIns.ThrowNullReference();
            ErrorCode.RosValidate();
        }
    
        public int RosMessageLength => 12 + StartState.RosMessageLength + Solution.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
