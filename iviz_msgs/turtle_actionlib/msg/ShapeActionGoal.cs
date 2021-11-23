/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ShapeActionGoal : IDeserializable<ShapeActionGoal>, IActionGoal<ShapeGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public ShapeGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public ShapeActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new ShapeGoal();
        }
        
        /// Explicit constructor.
        public ShapeActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, ShapeGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal ShapeActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ShapeGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ShapeActionGoal(ref b);
        
        ShapeActionGoal IDeserializable<ShapeActionGoal>.RosDeserialize(ref Buffer b) => new ShapeActionGoal(ref b);
    
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
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "dbfccd187f2ec9c593916447ffd6cc77";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VUwWobMRC96ysGfEhSiAPtzdBbaJJDoeDczVga74pqpa1Ga3f/vk9a202hhx4as7BI" +
                "O/Pmzbw3fhZ2kqlvL8O2+BSD3+8G7fThKXF4eaQOr513ZtvzKPWu3ZjP//lnvm6fNqTFLcWfF0or2haO" +
                "jrOjQQo7LkyHBMa+6yXfBzlKQBIPozhqX8s8iq6R+Np7JTydRMkcwkyTIqgksmkYpugtF6HiB/kjH5k+" +
                "EtPIuXg7Bc6IT9n5WMMPmQep6HhUfkwSrdDL4wYxUcVOxYPQDASbhdXHDh/JTD6WTx9rglm9ntI9jtJh" +
                "7tfiVHoulaz8HLNo5cm6QY0PS3NrYGM4gipO6bbd7XDUO0IRUJAx2Z5uwfzbXPoUASh05Ox5H6QCW0wA" +
                "qDc16ebuDXJs0JFjusAviL9r/AtsvOLWnu57aBZq9zp1GCACx5yO3iF0PzcQG7zEQjBb5jybmrWUNKsv" +
                "dcYIQlZTBG9WTdZDAEcnX3qjJVf0pkb15ju58a8L0ax1Jkvapyk4HFKW1ldrBFqeeg9BWhN1XejESrka" +
                "RtFENdBL07tZEiPheC4GkfMR1jj1EskXQqOi1bTwhQxjIQwc2RVTF9ecBKWv0LSXQ+XCZCUXhnKV0dv5" +
                "nvl7d9EE4wW9uRa5zpkOIm7P9juYOWTAlFMo2EFV7qSJQDqK9QdvlwbPDHR9Rq8LsgSA1DBpATPC1iFq" +
                "fdGvKvdO0pUplyC7q4IP178vY5ZtFNeJmkNIXE+ZnZ/U/ALCRbiJEQUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
