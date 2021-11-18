/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = "turtle_actionlib/ShapeActionGoal")]
    public sealed class ShapeActionGoal : IDeserializable<ShapeActionGoal>, IActionGoal<ShapeGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public ShapeGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ShapeActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new ShapeGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ShapeActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, ShapeGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ShapeActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ShapeGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ShapeActionGoal(ref b);
        }
        
        ShapeActionGoal IDeserializable<ShapeActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new ShapeActionGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength => 8 + Header.RosMessageLength + GoalId.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "dbfccd187f2ec9c593916447ffd6cc77";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwWobMRC9C/wPAz4kKdiB9mboLTTxoVCw72YsjXdFtNJWo7Xrv+/Tru2k0EMPjVlY" +
                "pJ1582beG78IO8nUji/DtvgUg9/vOm308TlxWD9Rg9fOO7NpuZd6N97MzNf//JuZ75vnFWlxU/mXkdTM" +
                "zGlTODrOjjop7LgwHRJI+6aVvAhylIAs7npxNH4t5150icRt65XwNBIlcwhnGhRBJZFNXTdEb7kIFd/J" +
                "H/nI9JGYes7F2yFwRnzKzscafsjcSUXHo/JzkGiF1k8rxEQVOxQPQmcg2CysPjb4SGbwsXz5XBPMfHtK" +
                "Cxylwehvxam0XCpZ+dVn0cqTdYUan6bmlsDGdARVnNL9eLfDUR8IRUBB+mRbugfzH+fSpghAoSNnz/sg" +
                "FdhiAkC9q0l3D++QK+0VRY7pCj8hvtX4F9h4w609LVpoFmr3OjQYIAL7nI7eIXR/HkFs8BILwW+Z89nU" +
                "rKmkmX+rM0YQskZF8GbVZD0EcHTypTVackUf1aj2/DBD/nUrqi236GGSTts0BIdDypX1ZCmCnKfWQ5Ox" +
                "j7o0dGKlXD2j6KN6aD1KProSU+F4qQad8xHuOLUSyRdCr6LVt7CGdH0hzBzZFVMn45wEpW/QtBesCCiQ" +
                "lVwY4lVG70d84e/dVRZMGPSgTHobNR1E3J7tK5g5ZMCXQyhYQ1VuZNSBtBfrD95ODV4Y6PKCXndkCgCp" +
                "btACZoTFQ9TyKiGiPk69MuQSZHcT8fH2NzYzZtpJcY2oOYTE9ZTZ+UFn5jee4dLAGwUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
