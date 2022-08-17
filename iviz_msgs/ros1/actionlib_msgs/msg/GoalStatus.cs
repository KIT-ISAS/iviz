/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibMsgs
{
    [DataContract]
    public sealed class GoalStatus : IDeserializable<GoalStatus>, IMessage
    {
        [DataMember (Name = "goal_id")] public GoalID GoalId;
        [DataMember (Name = "status")] public byte Status;
        /// <summary> The goal has yet to be processed by the action server </summary>
        public const byte PENDING = 0;
        /// <summary> The goal is currently being processed by the action server </summary>
        public const byte ACTIVE = 1;
        /// <summary> The goal received a cancel request after it started executing </summary>
        public const byte PREEMPTED = 2;
        //   and has since completed its execution (Terminal State)
        /// <summary> The goal was achieved successfully by the action server (Terminal State) </summary>
        public const byte SUCCEEDED = 3;
        /// <summary> The goal was aborted during execution by the action server due </summary>
        public const byte ABORTED = 4;
        //    to some failure (Terminal State)
        /// <summary> The goal was rejected by the action server without being processed, </summary>
        public const byte REJECTED = 5;
        //    because the goal was unattainable or invalid (Terminal State)
        /// <summary> The goal received a cancel request after it started executing </summary>
        public const byte PREEMPTING = 6;
        //    and has not yet completed execution
        /// <summary> The goal received a cancel request before it started executing, </summary>
        public const byte RECALLING = 7;
        //    but the action server has not yet confirmed that the goal is canceled
        /// <summary> The goal received a cancel request before it started executing </summary>
        public const byte RECALLED = 8;
        //    and was successfully cancelled (Terminal State)
        /// <summary> An action client can determine that a goal is LOST. This should not be </summary>
        public const byte LOST = 9;
        //    sent over the wire by an action server
        //Allow for the user to associate a string with GoalStatus for debugging
        [DataMember (Name = "text")] public string Text;
    
        public GoalStatus()
        {
            GoalId = new GoalID();
            Text = "";
        }
        
        public GoalStatus(GoalID GoalId, byte Status, string Text)
        {
            this.GoalId = GoalId;
            this.Status = Status;
            this.Text = Text;
        }
        
        public GoalStatus(ref ReadBuffer b)
        {
            GoalId = new GoalID(ref b);
            b.Deserialize(out Status);
            b.DeserializeString(out Text);
        }
        
        public GoalStatus(ref ReadBuffer2 b)
        {
            GoalId = new GoalID(ref b);
            b.Deserialize(out Status);
            b.DeserializeString(out Text);
        }
        
        public GoalStatus RosDeserialize(ref ReadBuffer b) => new GoalStatus(ref b);
        
        public GoalStatus RosDeserialize(ref ReadBuffer2 b) => new GoalStatus(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            GoalId.RosSerialize(ref b);
            b.Serialize(Status);
            b.Serialize(Text);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            GoalId.RosSerialize(ref b);
            b.Serialize(Status);
            b.Serialize(Text);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) BuiltIns.ThrowNullReference();
            GoalId.RosValidate();
            if (Text is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + GoalId.RosMessageLength + WriteBuffer.GetStringSize(Text);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = GoalId.AddRos2MessageLength(c);
            c += 1; // Status
            c = WriteBuffer2.AddLength(c, Text);
            return c;
        }
    
        public const string MessageType = "actionlib_msgs/GoalStatus";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d388f9b87b3c471f784434d671988d4a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
