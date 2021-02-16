using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/GetPositionFK")]
    public sealed class GetPositionFK : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetPositionFKRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetPositionFKResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetPositionFK()
        {
            Request = new GetPositionFKRequest();
            Response = new GetPositionFKResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetPositionFK(GetPositionFKRequest request)
        {
            Request = request;
            Response = new GetPositionFKResponse();
        }
        
        IService IService.Create() => new GetPositionFK();
        
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/GetPositionFK";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "03d4858215085d70e74807025d68dc4c";
    }

    [DataContract]
    public sealed class GetPositionFKRequest : IRequest<GetPositionFK, GetPositionFKResponse>, IDeserializable<GetPositionFKRequest>
    {
        // A service definition for a standard forward kinematics service
        // The frame_id in the header message is the frame in which 
        // the forward kinematics poses will be returned
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // A vector of link name for which forward kinematics must be computed
        [DataMember (Name = "fk_link_names")] public string[] FkLinkNames { get; set; }
        // A robot state consisting of joint names and joint positions to be used for forward kinematics
        [DataMember (Name = "robot_state")] public RobotState RobotState { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetPositionFKRequest()
        {
            FkLinkNames = System.Array.Empty<string>();
            RobotState = new RobotState();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetPositionFKRequest(in StdMsgs.Header Header, string[] FkLinkNames, RobotState RobotState)
        {
            this.Header = Header;
            this.FkLinkNames = FkLinkNames;
            this.RobotState = RobotState;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetPositionFKRequest(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            FkLinkNames = b.DeserializeStringArray();
            RobotState = new RobotState(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetPositionFKRequest(ref b);
        }
        
        GetPositionFKRequest IDeserializable<GetPositionFKRequest>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(FkLinkNames, 0);
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
                size += 4 * FkLinkNames.Length;
                foreach (string s in FkLinkNames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += RobotState.RosMessageLength;
                return size;
            }
        }
    }

    [DataContract]
    public sealed class GetPositionFKResponse : IResponse, IDeserializable<GetPositionFKResponse>
    {
        // The resultant vector of PoseStamped messages that contains the (stamped) poses of the requested links
        [DataMember (Name = "pose_stamped")] public GeometryMsgs.PoseStamped[] PoseStamped { get; set; }
        // The list of link names corresponding to the poses
        [DataMember (Name = "fk_link_names")] public string[] FkLinkNames { get; set; }
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetPositionFKResponse()
        {
            PoseStamped = System.Array.Empty<GeometryMsgs.PoseStamped>();
            FkLinkNames = System.Array.Empty<string>();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetPositionFKResponse(GeometryMsgs.PoseStamped[] PoseStamped, string[] FkLinkNames, MoveItErrorCodes ErrorCode)
        {
            this.PoseStamped = PoseStamped;
            this.FkLinkNames = FkLinkNames;
            this.ErrorCode = ErrorCode;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetPositionFKResponse(ref Buffer b)
        {
            PoseStamped = b.DeserializeArray<GeometryMsgs.PoseStamped>();
            for (int i = 0; i < PoseStamped.Length; i++)
            {
                PoseStamped[i] = new GeometryMsgs.PoseStamped(ref b);
            }
            FkLinkNames = b.DeserializeStringArray();
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetPositionFKResponse(ref b);
        }
        
        GetPositionFKResponse IDeserializable<GetPositionFKResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(PoseStamped, 0);
            b.SerializeArray(FkLinkNames, 0);
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
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                foreach (var i in PoseStamped)
                {
                    size += i.RosMessageLength;
                }
                size += 4 * FkLinkNames.Length;
                foreach (string s in FkLinkNames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    }
}
