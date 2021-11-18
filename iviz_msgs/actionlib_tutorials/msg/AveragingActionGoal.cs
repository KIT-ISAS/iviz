/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/AveragingActionGoal")]
    public sealed class AveragingActionGoal : IDeserializable<AveragingActionGoal>, IActionGoal<AveragingGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public AveragingGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new AveragingGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, AveragingGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AveragingActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new AveragingGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingActionGoal(ref b);
        }
        
        AveragingActionGoal IDeserializable<AveragingActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new AveragingActionGoal(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "dbfccd187f2ec9c593916447ffd6cc77";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWsbMRC9C/wfBnJIWogD7c3QQyE0yaFQSO5hLI13RbXSVqO163/fN7vOF/TQQ1uz" +
                "sEg78+bNezO+FQ5SqZ9fjn2LJae4fRy006ubwunumjq8HmNwn/dSuYu5s/v5duU+/eXfyn29v9mQtrBQ" +
                "uJ2JrdwZ3TfOgWugQRoHbky7AuKx66VeJtlLQhYPowSav7bjKLpG4kMflfB0ksE/pSNNiqBWyJdhmHL0" +
                "3IRaHORNPjJjJqaRa4t+SlwRX2qI2cJ3lQcxdDwqPybJXujueoOYrOKnFkHoCARfhRWa4SO5Keb28YMl" +
                "uLOHQ7nEUTrI/1ycWs/NyMrPsYoaT9YNarxfmlsDG+oIqgSli/nuEUd9RygCCjIW39MFmH87tr5kAArt" +
                "uUbeJjFgDwWAem5J5+9eIRvtDWXO5Ql+QXyp8Sew+RnXerrs4Vmy7nXqICACx1r2MSB0e5xBfIqSG2Hm" +
                "Ktejs6ylpDv7YhojCFmzI3izavERBgQ6xNY7bdXQZzdsRP/ZQP52M2wsH9DDYp32ZUoBh1KN9TJSBDsP" +
                "fYQncx+2NHRgpWozo+jDZuhutnyeSqjC+VQNPlesHPIlU2yEXkVtbjEaMoyNoDmyDVOXwTkISj9D01aw" +
                "IqBAXmpjmGeMXkt84h/Dky1QGPTgTHmRmnYiYcv+O5gFZGAup9SwhqrcyewD6Sg+7qJfGjwx0PUJ3XZk" +
                "CQCpYdIGZoTFQ9T6yUJE/Q/32gR7IgS7evNvtnJuWU0JnajbpcJ2qhzipCv3C+Bi/nkmBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
