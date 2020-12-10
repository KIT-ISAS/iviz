/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract (Name = "actionlib_tutorials/AveragingActionGoal")]
    public sealed class AveragingActionGoal : IDeserializable<AveragingActionGoal>, IActionGoal<AveragingGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public AveragingGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingActionGoal()
        {
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new AveragingGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, AveragingGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AveragingActionGoal(ref Buffer b)
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
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "dbfccd187f2ec9c593916447ffd6cc77";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VUwWrcMBC96ysG9pCkkA20t0APhdAkh0IhuYdZaWyLypKrkXfrv++TvLtNoIcemsVg" +
                "JM+8efPezD4IO8k0tJdhW3yKwe9eRu315j5xeLyjHq8X78yXvWTufezrfbs1n//zz3x7ur8lLW4l8LDS" +
                "2tBT4eg4OxqlsOPC1CWw9v0g+TrIXgKSeJzEUftalkl0i8TnwSvh6SWCfAgLzYqgksimcZyjt1yEih/l" +
                "TT4yfSSmiXPxdg6cEZ+y87GGd5lHqeh4VH7OEq3Q490tYqKKnYsHoQUINgsrBMNHMrOP5dPHmmA2z4d0" +
                "jaP00P5cnMrApZKVX1MWrTxZb1Hjw9rcFtgQR1DFKV22uxcc9YpQBBRkSnagSzD/vpQhRQAK7Tl73gWp" +
                "wBYKAPWiJl1cvUKODTpyTCf4FfFPjX+BjWfc2tP1AM9C7V7nHgIicMpp7x1Cd0sDscFLLISBy5wXU7PW" +
                "kmbztWqMIGQ1R/Bm1WQ9DHB08GUwWnJFb27U+XynafzrUrTROpIlHdIcHA4pS+urNQIvD4OHIa2Jui50" +
                "YKVcB0bRRB2gx+Z3G0lIwvFYDCZnLBvyJZIvhEZF69BiLmScCkFwZFdMXafmICh9hqaddJULk5VcGM5V" +
                "Rq/1PfL37uQJ5AW9pRY560ydiNux/QFmDhkYyjkU7KAq99JMIJ3E+s7btcEjA90e0euCrAEgNc5awIyw" +
                "dYjanvyrzr27dWWGOR5y3bz5FzNmXUpxvajpQuJ6yuz8rOY3tc9zrxwFAAA=";
                
    }
}
