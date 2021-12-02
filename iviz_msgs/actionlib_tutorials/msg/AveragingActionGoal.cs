/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AveragingActionGoal : IDeserializable<AveragingActionGoal>, IActionGoal<AveragingGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public AveragingGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public AveragingActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new AveragingGoal();
        }
        
        /// Explicit constructor.
        public AveragingActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, AveragingGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal AveragingActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new AveragingGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AveragingActionGoal(ref b);
        
        AveragingActionGoal IDeserializable<AveragingActionGoal>.RosDeserialize(ref Buffer b) => new AveragingActionGoal(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "dbfccd187f2ec9c593916447ffd6cc77";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUTWsbMRC961cM5JCkYAfam6GHQmiSQ6GQ3MNYGu8O0Upbjdau/32fdp0v6KGHxiws" +
                "0s68efPejG+FgxTq55djXzWnqNvHwTq7uskc766pw+tRg/u2l8Kdpq7dz7fu63/+uR/3NxuyGhYCtwut" +
                "M7qvnAKXQINUDlyZdhmsteulrKLsJSKJh1ECzV/rcRRbI/GhVyM8nSSQj/FIkyGoZvJ5GKaknqtQ1UHe" +
                "5SNTEzGNXKr6KXJBfC5BUwvfFR6koeMx+TVJ8kJ31xvEJBM/VQWhIxB8ETYIho/kJk31y+eW4M4eDnmF" +
                "o3TQ/qU41Z5rIyu/xyLWeLJtUOPT0twa2BBHUCUYXcx3jzjaJaEIKMiYfU8XYP7zWPucACi056K8jdKA" +
                "PRQA6nlLOr98g9xobyhxys/wC+JrjX+BTS+4radVD89i696mDgIicCx5rwGh2+MM4qNKqoSBK1yOrmUt" +
                "Jd3Z96YxgpA1O4I3m2WvMCDQQWvvrJaGPrvR5vODpvGvSzGP1oksWZ+nGHDIpVFe5ong5aFXGDI30daF" +
                "DmxU2sAYmmgDdDf7PY8kJOF0KgaTC5YN+ZJIK6FRsTa0mAsZxkoQHNkN05apOQhKv0DTVrAfoEBeSmU4" +
                "1xi91ffEX8OzJ5AX9GBLftWZdiJhy/4JzAIyMJRTrNhBM+5kNoFsFK879UuDJwa2PqG3BVkCQGqYrIIZ" +
                "YesQtX72rzn34dbVCeYo5Lp69y/m3LKUEjoxt4uZ26lw0MncH7XPc68cBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
