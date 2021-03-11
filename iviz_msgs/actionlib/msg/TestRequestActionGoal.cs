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
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TestRequestGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TestRequestGoal Goal)
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
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
                "H4sIAAAAAAAACr1UwU7bQBC9W8o/jMQBWgloob0g5UDBhVSFoCSVerMmu4O9qr3r7q4J+fu+tROgTQ89" +
                "tLUsrbwz8+bNvBlfC2vxVPVHxioaZ2uzLJpQhuMrx/XkkkochdHZQkKcyfcOR7L096Ns/JefUXYzvzqj" +
                "EPVA4rqnNsr2aB7ZavaaGomsOTLdO1A3ZSX+sJYHqRHFTSuaemtctxKOELioTCC8pVjxXNdr6gKcoiPl" +
                "mqazRnEUiqaRn+IRaSwxteyjUV3NHv7Oa2OT+73nRhI63pC6YpXQ5PIMPjaI6qIBoTUQlBcOxpYwUtYZ" +
                "G09PUkC2t1i5Q3xKCQGeklOsOCay8th6CYknhzPkeD0UdwRsdEeQRQc66O8KfIZXhCSgIK1TFR2A+d06" +
                "Vs4CUOiBveFlLQlYoQNA3U9B+69eICfaZ2TZui38gPic409g7RNuqumwgmZ1qj50JRoIx9a7B6Phulz3" +
                "IKo2YiNh6jz7dZaihpTZ3sfUYzghqlcEJ4fglIEAmlYmVlmIPqH3aqQh/WcD+dvdSGO5QA2DdKFyXa3x" +
                "4XxiPYwUQc5VZaBJX0daGlpxID9skug0Q5Ne8n4q0RW2m2zQ2T9gOlaVWDKRUKuENLcYDWnaSOg5ohNm" +
                "GAZnJUj9BE1LwYqAAinxkSFeYvSyxRv+Rm9lQYdBD8q451bTvYhesvoGZhoRmMuujljDELiUXgcKrShz" +
                "b9RQ4IZBONqgpx0ZHECq6UIEM8LiwetoKyG8/oN6x7/8xUbZsJGLfHYzuT1f5MX8y8VFPp+P3+xYzj9M" +
                "Z4v8cvx2xzLLP+UXyXSyY/o8nefj053ry9n0bvxu5zr/epHfLSbT2/H7jS2Kb/o/TgHFYheypXM1mdJC" +
                "2EIxFrTednDQpYjyGLfBlRTD7SYsFME0bY3Ifusy3XnuJ01LzeuClZJ25/aJwrOhZQzrhtEo+wEpu+v1" +
                "RgYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
