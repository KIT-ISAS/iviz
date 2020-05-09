using System.Runtime.Serialization;

namespace Iviz.Msgs.actionlib_msgs
{
    public sealed class GoalStatus : IMessage
    {
        public GoalID goal_id { get; set; }
        public byte status { get; set; }
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
        public string text { get; set; }
        
    
        /// <summary> Constructor for empty message. </summary>
        public GoalStatus()
        {
            goal_id = new GoalID();
            text = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GoalStatus(GoalID goal_id, byte status, string text)
        {
            this.goal_id = goal_id ?? throw new System.ArgumentNullException(nameof(goal_id));
            this.status = status;
            this.text = text ?? throw new System.ArgumentNullException(nameof(text));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GoalStatus(Buffer b)
        {
            this.goal_id = new GoalID(b);
            this.status = BuiltIns.DeserializeStruct<byte>(b);
            this.text = BuiltIns.DeserializeString(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GoalStatus(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.goal_id.Serialize(b);
            BuiltIns.Serialize(this.status, b);
            BuiltIns.Serialize(this.text, b);
        }
        
        public void Validate()
        {
            if (goal_id is null) throw new System.NullReferenceException();
            if (text is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += goal_id.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(text);
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "actionlib_msgs/GoalStatus";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "d388f9b87b3c471f784434d671988d4a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTY/aMBC951eMtJdWquh3u63EgUKEqPZLC+115TgTcOs41B7D8u87Nkkgu9DlsGok" +
                "CETjN2/evJmMK6EnI5jz7U7liVeGzsGRIO/qPzfp1WhyNYbm6sMb/j6D2QLjMVgIBxskoAoyhKWtJDqH" +
                "OWQbII4RklRlwKFdoa0xB8PZ5GcKe5hvu5jKgfTWoiG9YVRl5qcB39ym6eXNLB21wO+6wBYlqhWDCJDC" +
                "SAxP/nh0BKIgtKAoVG+JI/AepSdOncA/rjP+CJNHFZxiRJBVudQYEBS5BoWJvpihLZVhElPWF1/WlKc/" +
                "hsM0He1Rft+lvGZkIRcKA23nZVCh8DoIc0CIY2kG365vd7qENB8OpMmqWHrubZB8x/1gptzjk9IEV7iq" +
                "RCiE0t7iMXq36fd0uMevDx8f07P4CyUdcQCsFS0qTw/t8uppjhlK4R1GzDaZN4JIMNNMI1TsDLMSWuXH" +
                "Cqid105KHz79B+e11jMVxSHcma9tXqvwcHBxsZvkPnw+lWCGRcWtO8TwFHW5J4+71SVtCmVLxqWFoF0b" +
                "whaITDDvFLFvk/NnKOI0mYMpOuO3TcDkjnni4no624fqw5cIODCNGFIr3nEBCXLuWgDBrQiilSCg9LhC" +
                "/unY4DqPumUnzJ4L2FVQO0i6Vlw+T44wD1ZncjbQuloD6xMDeRRsmFvhXCUV18NkHMWNEGYMxkxsGl8R" +
                "8UiOmZ/Pg4x1EOE9JUnSf+YruZyOv9bUtcruSjd3r8fx/ZVsHcCNLZeNSI5Cu0M9pHj/sKTrhZILfsJC" +
                "7q2U6A7Me4wxoaC3r98xD3Xi82iCf7hKdEGgpUUsl9wrrfl0wHTb5q2RU7fQjfXYkmjDSomMkkgrUk5q" +
                "/rxdeG2tVM7wgultul0oEPNMyN/BjXzCovOaoGQ/ijluW+OWKFWhZDMMkYHr1eh8qA5gUqWPQ8F7TnFU" +
                "r2keRyXJX0kwdDIWCAAA";
                
    }
}
