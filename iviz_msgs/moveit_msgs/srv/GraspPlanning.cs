using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GraspPlanning : IService
    {
        /// Request message.
        [DataMember] public GraspPlanningRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GraspPlanningResponse Response { get; set; }
        
        /// Empty constructor.
        public GraspPlanning()
        {
            Request = new GraspPlanningRequest();
            Response = new GraspPlanningResponse();
        }
        
        /// Setter constructor.
        public GraspPlanning(GraspPlanningRequest request)
        {
            Request = request;
            Response = new GraspPlanningResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GraspPlanningRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GraspPlanningResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/GraspPlanning";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "6c1eec2555db251f88e13e06d2a82f0f";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GraspPlanningRequest : IRequest<GraspPlanning, GraspPlanningResponse>, IDeserializable<GraspPlanningRequest>
    {
        // Requests that grasp planning be performed for the target object
        // returns a list of candidate grasps to be tested and executed
        // the planning group used
        [DataMember (Name = "group_name")] public string GroupName;
        // the object to be grasped
        [DataMember (Name = "target")] public CollisionObject Target;
        // the names of the relevant support surfaces (e.g. tables) in the collision map
        // can be left empty if no names are available
        [DataMember (Name = "support_surfaces")] public string[] SupportSurfaces;
        // an optional list of grasps to be evaluated by the planner
        [DataMember (Name = "candidate_grasps")] public Grasp[] CandidateGrasps;
        // an optional list of obstacles that we have semantic information about
        // and that can be moved in the course of grasping
        [DataMember (Name = "movable_obstacles")] public CollisionObject[] MovableObstacles;
    
        /// Constructor for empty message.
        public GraspPlanningRequest()
        {
            GroupName = "";
            Target = new CollisionObject();
            SupportSurfaces = System.Array.Empty<string>();
            CandidateGrasps = System.Array.Empty<Grasp>();
            MovableObstacles = System.Array.Empty<CollisionObject>();
        }
        
        /// Explicit constructor.
        public GraspPlanningRequest(string GroupName, CollisionObject Target, string[] SupportSurfaces, Grasp[] CandidateGrasps, CollisionObject[] MovableObstacles)
        {
            this.GroupName = GroupName;
            this.Target = Target;
            this.SupportSurfaces = SupportSurfaces;
            this.CandidateGrasps = CandidateGrasps;
            this.MovableObstacles = MovableObstacles;
        }
        
        /// Constructor with buffer.
        public GraspPlanningRequest(ref ReadBuffer b)
        {
            GroupName = b.DeserializeString();
            Target = new CollisionObject(ref b);
            SupportSurfaces = b.DeserializeStringArray();
            CandidateGrasps = b.DeserializeArray<Grasp>();
            for (int i = 0; i < CandidateGrasps.Length; i++)
            {
                CandidateGrasps[i] = new Grasp(ref b);
            }
            MovableObstacles = b.DeserializeArray<CollisionObject>();
            for (int i = 0; i < MovableObstacles.Length; i++)
            {
                MovableObstacles[i] = new CollisionObject(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GraspPlanningRequest(ref b);
        
        public GraspPlanningRequest RosDeserialize(ref ReadBuffer b) => new GraspPlanningRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(GroupName);
            Target.RosSerialize(ref b);
            b.SerializeArray(SupportSurfaces);
            b.SerializeArray(CandidateGrasps);
            b.SerializeArray(MovableObstacles);
        }
        
        public void RosValidate()
        {
            if (GroupName is null) throw new System.NullReferenceException(nameof(GroupName));
            if (Target is null) throw new System.NullReferenceException(nameof(Target));
            Target.RosValidate();
            if (SupportSurfaces is null) throw new System.NullReferenceException(nameof(SupportSurfaces));
            for (int i = 0; i < SupportSurfaces.Length; i++)
            {
                if (SupportSurfaces[i] is null) throw new System.NullReferenceException($"{nameof(SupportSurfaces)}[{i}]");
            }
            if (CandidateGrasps is null) throw new System.NullReferenceException(nameof(CandidateGrasps));
            for (int i = 0; i < CandidateGrasps.Length; i++)
            {
                if (CandidateGrasps[i] is null) throw new System.NullReferenceException($"{nameof(CandidateGrasps)}[{i}]");
                CandidateGrasps[i].RosValidate();
            }
            if (MovableObstacles is null) throw new System.NullReferenceException(nameof(MovableObstacles));
            for (int i = 0; i < MovableObstacles.Length; i++)
            {
                if (MovableObstacles[i] is null) throw new System.NullReferenceException($"{nameof(MovableObstacles)}[{i}]");
                MovableObstacles[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.GetStringSize(GroupName);
                size += Target.RosMessageLength;
                size += BuiltIns.GetArraySize(SupportSurfaces);
                size += BuiltIns.GetArraySize(CandidateGrasps);
                size += BuiltIns.GetArraySize(MovableObstacles);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GraspPlanningResponse : IResponse, IDeserializable<GraspPlanningResponse>
    {
        // the list of planned grasps
        [DataMember (Name = "grasps")] public Grasp[] Grasps;
        // whether an error occurred
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
    
        /// Constructor for empty message.
        public GraspPlanningResponse()
        {
            Grasps = System.Array.Empty<Grasp>();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// Explicit constructor.
        public GraspPlanningResponse(Grasp[] Grasps, MoveItErrorCodes ErrorCode)
        {
            this.Grasps = Grasps;
            this.ErrorCode = ErrorCode;
        }
        
        /// Constructor with buffer.
        public GraspPlanningResponse(ref ReadBuffer b)
        {
            Grasps = b.DeserializeArray<Grasp>();
            for (int i = 0; i < Grasps.Length; i++)
            {
                Grasps[i] = new Grasp(ref b);
            }
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GraspPlanningResponse(ref b);
        
        public GraspPlanningResponse RosDeserialize(ref ReadBuffer b) => new GraspPlanningResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Grasps);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Grasps is null) throw new System.NullReferenceException(nameof(Grasps));
            for (int i = 0; i < Grasps.Length; i++)
            {
                if (Grasps[i] is null) throw new System.NullReferenceException($"{nameof(Grasps)}[{i}]");
                Grasps[i].RosValidate();
            }
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetArraySize(Grasps);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
