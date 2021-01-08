/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestRequestActionGoal")]
    public sealed class TestRequestActionGoal : IDeserializable<TestRequestActionGoal>, IActionGoal<TestRequestGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public TestRequestGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestActionGoal()
        {
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TestRequestGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TestRequestGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestRequestActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TestRequestGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestActionGoal(ref b);
        }
        
        TestRequestActionGoal IDeserializable<TestRequestActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestActionGoal(ref b);
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
                int size = 0;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                size += Goal.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestRequestActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1889556d3fef88f821c7cb004e4251f3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUTW/bMAy9G8h/INBDtwFt93kpkEPXel2GrS2SDNjNYCTWFmZLniQ3y7/fk5y067LD" +
                "DmvgQLBIPj7ykf4orMVTk4+CVTTOtmZVdaEOJ5eO29kF1Tgqo4ulhDiXHwOOZMn3k2L6n3+T4svi8pRC" +
                "1COJj5napDigRWSr2WvqJLLmyHTrQN3UjfijVu6kRRR3vWjK1rjpJRynyGVjAuGpxYrntt3QEOAVHSnX" +
                "dYM1iqNQNJ08AkihxhJTzz4aNbTsEeC8Njb533ruJOOnf0idsUpodnEKLxtEDdGA1AYYygsHY2sY4TwY" +
                "G9+8ThEIXK7dEd6lhgz3DCg2HBNj+dl7CYksh9OU5sVY4zHg0SRBIh3oWb6r8BqeE/KAhfRONfQM9G82" +
                "sXEWiEJ37A2vWknICn0A7GEKOnz+O3SifkqWrdvhj5APSf4F1z4Ap7KOGojXphaEoUYf4dl7d2c0fFeb" +
                "jKJaIzYSxs+z30yKFDYmBciH1Gy4IS5rg5NDcMpACU1rE5tJEaJPCbIuGNhJ8WTT+ddFGSdtS5lC44ZW" +
                "48X5xHscL4Kq68ZAmVxJ2iBacyA/rpXoPE6zLH0eUbSG7TYd5PZ3mJJ1I5ZMJFQrIQ0xRkS6PhI6n8IT" +
                "ahgnaC1Ifg9OK8HGgAQp8ZGhYeL0uNG7Ioze6YNGgyMkcg8dp1sRvWL1HezQ5wPkCEMbsZkhcC1ZDwq9" +
                "KHNr1FjmlkU43sLnnRk9wKwbQgQ9wi7C7fhey1HFp9fx5I+P26QYd3RZzr/Mrs6WZbX4en5eLhbTl3uW" +
                "s/fX82V5MX21Z5mXn8rzZHq9Z/p8vSinb/auL+bXN9O3e9flt/PyZjm7vpq+29qi+C5/hioIF4dQrJxr" +
                "ydQWAleKsa/troejNlWUn3EX3Eg13m7DQhVM17eIzDtY6MFzHjktLW8qVkr6vdt7Cg+GnjG1W0aT4hfd" +
                "x3RKXQYAAA==";
                
    }
}
