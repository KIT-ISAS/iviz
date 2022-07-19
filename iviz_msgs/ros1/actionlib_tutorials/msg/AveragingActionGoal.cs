/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingActionGoal : IDeserializableRos1<AveragingActionGoal>, IDeserializableRos2<AveragingActionGoal>, IMessageRos1, IMessageRos2, IActionGoal<AveragingGoal>
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
        public AveragingActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new AveragingGoal(ref b);
        }
        
        /// Constructor with buffer.
        public AveragingActionGoal(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new AveragingGoal(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new AveragingActionGoal(ref b);
        
        public AveragingActionGoal RosDeserialize(ref ReadBuffer b) => new AveragingActionGoal(ref b);
        
        public AveragingActionGoal RosDeserialize(ref ReadBuffer2 b) => new AveragingActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) BuiltIns.ThrowNullReference();
            GoalId.RosValidate();
            if (Goal is null) BuiltIns.ThrowNullReference();
            Goal.RosValidate();
        }
    
        public int RosMessageLength => 8 + Header.RosMessageLength + GoalId.RosMessageLength;
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            GoalId.AddRos2MessageLength(ref c);
            Goal.AddRos2MessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib_tutorials/AveragingActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "dbfccd187f2ec9c593916447ffd6cc77";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
