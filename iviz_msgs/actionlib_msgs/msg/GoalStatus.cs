/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibMsgs
{
    [Preserve, DataContract (Name = "actionlib_msgs/GoalStatus")]
    public sealed class GoalStatus : IDeserializable<GoalStatus>, IMessage
    {
        [DataMember (Name = "goal_id")] public GoalID GoalId;
        [DataMember (Name = "status")] public byte Status;
        public const byte PENDING = 0; // The goal has yet to be processed by the action server
        public const byte ACTIVE = 1; // The goal is currently being processed by the action server
        public const byte PREEMPTED = 2; // The goal received a cancel request after it started executing
        //   and has since completed its execution (Terminal State)
        public const byte SUCCEEDED = 3; // The goal was achieved successfully by the action server (Terminal State)
        public const byte ABORTED = 4; // The goal was aborted during execution by the action server due
        //    to some failure (Terminal State)
        public const byte REJECTED = 5; // The goal was rejected by the action server without being processed,
        //    because the goal was unattainable or invalid (Terminal State)
        public const byte PREEMPTING = 6; // The goal received a cancel request after it started executing
        //    and has not yet completed execution
        public const byte RECALLING = 7; // The goal received a cancel request before it started executing,
        //    but the action server has not yet confirmed that the goal is canceled
        public const byte RECALLED = 8; // The goal received a cancel request before it started executing
        //    and was successfully cancelled (Terminal State)
        public const byte LOST = 9; // An action client can determine that a goal is LOST. This should not be
        //    sent over the wire by an action server
        //Allow for the user to associate a string with GoalStatus for debugging
        [DataMember (Name = "text")] public string Text;
    
        /// <summary> Constructor for empty message. </summary>
        public GoalStatus()
        {
            GoalId = new GoalID();
            Text = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GoalStatus(GoalID GoalId, byte Status, string Text)
        {
            this.GoalId = GoalId;
            this.Status = Status;
            this.Text = Text;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GoalStatus(ref Buffer b)
        {
            GoalId = new GoalID(ref b);
            Status = b.Deserialize<byte>();
            Text = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GoalStatus(ref b);
        }
        
        GoalStatus IDeserializable<GoalStatus>.RosDeserialize(ref Buffer b)
        {
            return new GoalStatus(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            GoalId.RosSerialize(ref b);
            b.Serialize(Status);
            b.Serialize(Text);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Text is null) throw new System.NullReferenceException(nameof(Text));
        }
    
        public int RosMessageLength => 5 + GoalId.RosMessageLength + BuiltIns.GetStringSize(Text);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_msgs/GoalStatus";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d388f9b87b3c471f784434d671988d4a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/aQBC9W8p/WCmXVqrS7zatxIGChajSJAq012i9HmDbtZfuByT/vm8WYyABhUNU" +
                "S4CxZt+8efNmPLDSDPtiip9bXWZR1+Fc+CBD9M2f6/yyP7wciPXVEW/wfSrGM0rHxEx6cU9BBCsKEnNn" +
                "FXlPpSjuRUCMVEHbWnhyC3INZrc3Hv7KxRbm211M7YWKzlEdzD1QdT09Dvj6Js9/XI/zfgv8bhfYkSK9" +
                "AIgUStaK+MnfSD4IOQnkhA5cvQuIoDtSMSB1tiH6+DrFR9ZlUsFrIAplq7khRtDBr1FA9MWYXKVrkBhB" +
                "X3rZUB797PXyvL9F+f0u5SWQpZppYto+KlZhEg0Ls0eIQ2m6365uNrpwmg970hQ2lV5Gx5JvuO/NVEZ6" +
                "Uhp2hbcViYnUJjo6RO8m/573tvh1xMfH9Bz9JsX89tJZ6jCzMTy0y6unORakZPSUMNtksZYhSDAtDAkL" +
                "Z9QLaXR5qIDGee2kdMSn/+C81nq1DWkIN+Zrm9cq3OteXGwmuSM+H0uwoIlF6/YxPEZd9ORxt3ZJ1xPt" +
                "KuCGmVwFt1sgMaFyp4htm5w/QxHHycym2Bm/VQKQO+SJi6vReBuqI74kwG69FkMZjR3HSKJE1xiEViLI" +
                "VgJGOUOFuPUwuCmTbsURs+cZ27LaLOlSo3xMDnLtrs7stGuMXQrokwIxCrixQnpvlUY9IOND2gg8Y2IA" +
                "Ylxn9OlISUWcTlnGJijQXciyk6zzzNdJ9mM0+NqQN7q4rfzUv2Y2w/5JtjIBelvN1zr5wB3nkoLGCoKq" +
                "y5lWMzyBlltbJRmEyjNgDANLHpvXzEOpcJ5qthAKJc8azR1RNUe7jMFpxsRj7t+SkLqFXrsPriTHWyUx" +
                "yhKtRBlNSPyxYLC5FroEvAQ9LLrtRkyIykKqP2xInHDkowmigiXllDuM7vg5KT3Raj0PiYFnAzE6DjUB" +
                "IFXFNBdYdRpRZ+v+IQrd+wd3QLUfGggAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
