using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/GraspPlanning")]
    public sealed class GraspPlanning : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GraspPlanningRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GraspPlanningResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GraspPlanning()
        {
            Request = new GraspPlanningRequest();
            Response = new GraspPlanningResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GraspPlanning(GraspPlanningRequest request)
        {
            Request = request;
            Response = new GraspPlanningResponse();
        }
        
        IService IService.Create() => new GraspPlanning();
        
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/GraspPlanning";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "6c1eec2555db251f88e13e06d2a82f0f";
    }

    [DataContract]
    public sealed class GraspPlanningRequest : IRequest<GraspPlanning, GraspPlanningResponse>, IDeserializable<GraspPlanningRequest>
    {
        // Requests that grasp planning be performed for the target object
        // returns a list of candidate grasps to be tested and executed
        // the planning group used
        [DataMember (Name = "group_name")] public string GroupName { get; set; }
        // the object to be grasped
        [DataMember (Name = "target")] public CollisionObject Target { get; set; }
        // the names of the relevant support surfaces (e.g. tables) in the collision map
        // can be left empty if no names are available
        [DataMember (Name = "support_surfaces")] public string[] SupportSurfaces { get; set; }
        // an optional list of grasps to be evaluated by the planner
        [DataMember (Name = "candidate_grasps")] public Grasp[] CandidateGrasps { get; set; }
        // an optional list of obstacles that we have semantic information about
        // and that can be moved in the course of grasping
        [DataMember (Name = "movable_obstacles")] public CollisionObject[] MovableObstacles { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GraspPlanningRequest()
        {
            GroupName = string.Empty;
            Target = new CollisionObject();
            SupportSurfaces = System.Array.Empty<string>();
            CandidateGrasps = System.Array.Empty<Grasp>();
            MovableObstacles = System.Array.Empty<CollisionObject>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GraspPlanningRequest(string GroupName, CollisionObject Target, string[] SupportSurfaces, Grasp[] CandidateGrasps, CollisionObject[] MovableObstacles)
        {
            this.GroupName = GroupName;
            this.Target = Target;
            this.SupportSurfaces = SupportSurfaces;
            this.CandidateGrasps = CandidateGrasps;
            this.MovableObstacles = MovableObstacles;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GraspPlanningRequest(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GraspPlanningRequest(ref b);
        }
        
        GraspPlanningRequest IDeserializable<GraspPlanningRequest>.RosDeserialize(ref Buffer b)
        {
            return new GraspPlanningRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(GroupName);
            Target.RosSerialize(ref b);
            b.SerializeArray(SupportSurfaces, 0);
            b.SerializeArray(CandidateGrasps, 0);
            b.SerializeArray(MovableObstacles, 0);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(GroupName);
                size += Target.RosMessageLength;
                size += 4 * SupportSurfaces.Length;
                foreach (string s in SupportSurfaces)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                foreach (var i in CandidateGrasps)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in MovableObstacles)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    }

    [DataContract]
    public sealed class GraspPlanningResponse : IResponse, IDeserializable<GraspPlanningResponse>
    {
        // the list of planned grasps
        [DataMember (Name = "grasps")] public Grasp[] Grasps { get; set; }
        // whether an error occurred
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GraspPlanningResponse()
        {
            Grasps = System.Array.Empty<Grasp>();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GraspPlanningResponse(Grasp[] Grasps, MoveItErrorCodes ErrorCode)
        {
            this.Grasps = Grasps;
            this.ErrorCode = ErrorCode;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GraspPlanningResponse(ref Buffer b)
        {
            Grasps = b.DeserializeArray<Grasp>();
            for (int i = 0; i < Grasps.Length; i++)
            {
                Grasps[i] = new Grasp(ref b);
            }
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GraspPlanningResponse(ref b);
        }
        
        GraspPlanningResponse IDeserializable<GraspPlanningResponse>.RosDeserialize(ref Buffer b)
        {
            return new GraspPlanningResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Grasps, 0);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                foreach (var i in Grasps)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    }
}
