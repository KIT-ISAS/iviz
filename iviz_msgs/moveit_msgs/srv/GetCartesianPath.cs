using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/GetCartesianPath")]
    public sealed class GetCartesianPath : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetCartesianPathRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetCartesianPathResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetCartesianPath()
        {
            Request = new GetCartesianPathRequest();
            Response = new GetCartesianPathResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetCartesianPath(GetCartesianPathRequest request)
        {
            Request = request;
            Response = new GetCartesianPathResponse();
        }
        
        IService IService.Create() => new GetCartesianPath();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/GetCartesianPath";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "5c9a54219f0d91a804e7670bc0e118f1";
    }

    [DataContract]
    public sealed class GetCartesianPathRequest : IRequest<GetCartesianPath, GetCartesianPathResponse>, IDeserializable<GetCartesianPathRequest>
    {
        // Define the frame for the specified waypoints
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // The start at which to start the Cartesian path
        [DataMember (Name = "start_state")] public RobotState StartState { get; set; }
        // Mandatory name of group to compute the path for
        [DataMember (Name = "group_name")] public string GroupName { get; set; }
        // Optional name of IK link for which waypoints are specified.
        // If not specified, the tip of the group (which is assumed to be a chain)
        // is assumed to be the link
        [DataMember (Name = "link_name")] public string LinkName { get; set; }
        // A sequence of waypoints to be followed by the specified link, 
        // while moving the specified group, such that the group moves only
        // in a straight line between waypoints
        [DataMember (Name = "waypoints")] public GeometryMsgs.Pose[] Waypoints { get; set; }
        // The maximum distance (in Cartesian space) between consecutive points
        // in the returned path. This must always be specified and > 0
        [DataMember (Name = "max_step")] public double MaxStep { get; set; }
        // If above 0, this value is assumed to be the maximum allowed distance 
        // (L infinity) in configuration space, between consecutive points.
        // If this distance is found to be above the maximum threshold, the path 
        // computation fails.
        [DataMember (Name = "jump_threshold")] public double JumpThreshold { get; set; }
        // Set to true if collisions should be avoided when possible
        [DataMember (Name = "avoid_collisions")] public bool AvoidCollisions { get; set; }
        // Specify additional constraints to be met by the Cartesian path
        [DataMember (Name = "path_constraints")] public Constraints PathConstraints { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetCartesianPathRequest()
        {
            StartState = new RobotState();
            GroupName = "";
            LinkName = "";
            Waypoints = System.Array.Empty<GeometryMsgs.Pose>();
            PathConstraints = new Constraints();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetCartesianPathRequest(in StdMsgs.Header Header, RobotState StartState, string GroupName, string LinkName, GeometryMsgs.Pose[] Waypoints, double MaxStep, double JumpThreshold, bool AvoidCollisions, Constraints PathConstraints)
        {
            this.Header = Header;
            this.StartState = StartState;
            this.GroupName = GroupName;
            this.LinkName = LinkName;
            this.Waypoints = Waypoints;
            this.MaxStep = MaxStep;
            this.JumpThreshold = JumpThreshold;
            this.AvoidCollisions = AvoidCollisions;
            this.PathConstraints = PathConstraints;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetCartesianPathRequest(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            StartState = new RobotState(ref b);
            GroupName = b.DeserializeString();
            LinkName = b.DeserializeString();
            Waypoints = b.DeserializeStructArray<GeometryMsgs.Pose>();
            MaxStep = b.Deserialize<double>();
            JumpThreshold = b.Deserialize<double>();
            AvoidCollisions = b.Deserialize<bool>();
            PathConstraints = new Constraints(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetCartesianPathRequest(ref b);
        }
        
        GetCartesianPathRequest IDeserializable<GetCartesianPathRequest>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            StartState.RosSerialize(ref b);
            b.Serialize(GroupName);
            b.Serialize(LinkName);
            b.SerializeStructArray(Waypoints, 0);
            b.Serialize(MaxStep);
            b.Serialize(JumpThreshold);
            b.Serialize(AvoidCollisions);
            PathConstraints.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (StartState is null) throw new System.NullReferenceException(nameof(StartState));
            StartState.RosValidate();
            if (GroupName is null) throw new System.NullReferenceException(nameof(GroupName));
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
            if (Waypoints is null) throw new System.NullReferenceException(nameof(Waypoints));
            if (PathConstraints is null) throw new System.NullReferenceException(nameof(PathConstraints));
            PathConstraints.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 29;
                size += Header.RosMessageLength;
                size += StartState.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(GroupName);
                size += BuiltIns.UTF8.GetByteCount(LinkName);
                size += 56 * Waypoints.Length;
                size += PathConstraints.RosMessageLength;
                return size;
            }
        }
    }

    [DataContract]
    public sealed class GetCartesianPathResponse : IResponse, IDeserializable<GetCartesianPathResponse>
    {
        // The state at which the computed path starts
        [DataMember (Name = "start_state")] public RobotState StartState { get; set; }
        // The computed solution trajectory, for the desired group, in configuration space
        [DataMember (Name = "solution")] public RobotTrajectory Solution { get; set; }
        // If the computation was incomplete, this value indicates the fraction of the path
        // that was in fact computed (nr of waypoints traveled through)
        [DataMember (Name = "fraction")] public double Fraction { get; set; }
        // The error code of the computation
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetCartesianPathResponse()
        {
            StartState = new RobotState();
            Solution = new RobotTrajectory();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetCartesianPathResponse(RobotState StartState, RobotTrajectory Solution, double Fraction, MoveItErrorCodes ErrorCode)
        {
            this.StartState = StartState;
            this.Solution = Solution;
            this.Fraction = Fraction;
            this.ErrorCode = ErrorCode;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetCartesianPathResponse(ref Buffer b)
        {
            StartState = new RobotState(ref b);
            Solution = new RobotTrajectory(ref b);
            Fraction = b.Deserialize<double>();
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetCartesianPathResponse(ref b);
        }
        
        GetCartesianPathResponse IDeserializable<GetCartesianPathResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            StartState.RosSerialize(ref b);
            Solution.RosSerialize(ref b);
            b.Serialize(Fraction);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (StartState is null) throw new System.NullReferenceException(nameof(StartState));
            StartState.RosValidate();
            if (Solution is null) throw new System.NullReferenceException(nameof(Solution));
            Solution.RosValidate();
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += StartState.RosMessageLength;
                size += Solution.RosMessageLength;
                return size;
            }
        }
    }
}
