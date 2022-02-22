using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetPositionFK : IService
    {
        /// Request message.
        [DataMember] public GetPositionFKRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetPositionFKResponse Response { get; set; }
        
        /// Empty constructor.
        public GetPositionFK()
        {
            Request = new GetPositionFKRequest();
            Response = new GetPositionFKResponse();
        }
        
        /// Setter constructor.
        public GetPositionFK(GetPositionFKRequest request)
        {
            Request = request;
            Response = new GetPositionFKResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetPositionFKRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetPositionFKResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/GetPositionFK";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "03d4858215085d70e74807025d68dc4c";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetPositionFKRequest : IRequest<GetPositionFK, GetPositionFKResponse>, IDeserializable<GetPositionFKRequest>
    {
        // A service definition for a standard forward kinematics service
        // The frame_id in the header message is the frame in which 
        // the forward kinematics poses will be returned
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A vector of link name for which forward kinematics must be computed
        [DataMember (Name = "fk_link_names")] public string[] FkLinkNames;
        // A robot state consisting of joint names and joint positions to be used for forward kinematics
        [DataMember (Name = "robot_state")] public RobotState RobotState;
    
        /// Constructor for empty message.
        public GetPositionFKRequest()
        {
            FkLinkNames = System.Array.Empty<string>();
            RobotState = new RobotState();
        }
        
        /// Explicit constructor.
        public GetPositionFKRequest(in StdMsgs.Header Header, string[] FkLinkNames, RobotState RobotState)
        {
            this.Header = Header;
            this.FkLinkNames = FkLinkNames;
            this.RobotState = RobotState;
        }
        
        /// Constructor with buffer.
        public GetPositionFKRequest(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            FkLinkNames = b.DeserializeStringArray();
            RobotState = new RobotState(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetPositionFKRequest(ref b);
        
        public GetPositionFKRequest RosDeserialize(ref ReadBuffer b) => new GetPositionFKRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(FkLinkNames);
            RobotState.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (FkLinkNames is null) throw new System.NullReferenceException(nameof(FkLinkNames));
            for (int i = 0; i < FkLinkNames.Length; i++)
            {
                if (FkLinkNames[i] is null) throw new System.NullReferenceException($"{nameof(FkLinkNames)}[{i}]");
            }
            if (RobotState is null) throw new System.NullReferenceException(nameof(RobotState));
            RobotState.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(FkLinkNames);
                size += RobotState.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetPositionFKResponse : IResponse, IDeserializable<GetPositionFKResponse>
    {
        // The resultant vector of PoseStamped messages that contains the (stamped) poses of the requested links
        [DataMember (Name = "pose_stamped")] public GeometryMsgs.PoseStamped[] PoseStamped;
        // The list of link names corresponding to the poses
        [DataMember (Name = "fk_link_names")] public string[] FkLinkNames;
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
    
        /// Constructor for empty message.
        public GetPositionFKResponse()
        {
            PoseStamped = System.Array.Empty<GeometryMsgs.PoseStamped>();
            FkLinkNames = System.Array.Empty<string>();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// Explicit constructor.
        public GetPositionFKResponse(GeometryMsgs.PoseStamped[] PoseStamped, string[] FkLinkNames, MoveItErrorCodes ErrorCode)
        {
            this.PoseStamped = PoseStamped;
            this.FkLinkNames = FkLinkNames;
            this.ErrorCode = ErrorCode;
        }
        
        /// Constructor with buffer.
        public GetPositionFKResponse(ref ReadBuffer b)
        {
            PoseStamped = b.DeserializeArray<GeometryMsgs.PoseStamped>();
            for (int i = 0; i < PoseStamped.Length; i++)
            {
                PoseStamped[i] = new GeometryMsgs.PoseStamped(ref b);
            }
            FkLinkNames = b.DeserializeStringArray();
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetPositionFKResponse(ref b);
        
        public GetPositionFKResponse RosDeserialize(ref ReadBuffer b) => new GetPositionFKResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(PoseStamped);
            b.SerializeArray(FkLinkNames);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (PoseStamped is null) throw new System.NullReferenceException(nameof(PoseStamped));
            for (int i = 0; i < PoseStamped.Length; i++)
            {
                if (PoseStamped[i] is null) throw new System.NullReferenceException($"{nameof(PoseStamped)}[{i}]");
                PoseStamped[i].RosValidate();
            }
            if (FkLinkNames is null) throw new System.NullReferenceException(nameof(FkLinkNames));
            for (int i = 0; i < FkLinkNames.Length; i++)
            {
                if (FkLinkNames[i] is null) throw new System.NullReferenceException($"{nameof(FkLinkNames)}[{i}]");
            }
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetArraySize(PoseStamped) + BuiltIns.GetArraySize(FkLinkNames);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
