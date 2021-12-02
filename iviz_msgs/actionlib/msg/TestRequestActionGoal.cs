/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestRequestActionGoal : IDeserializable<TestRequestActionGoal>, IActionGoal<TestRequestGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public TestRequestGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public TestRequestActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TestRequestGoal();
        }
        
        /// Explicit constructor.
        public TestRequestActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TestRequestGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal TestRequestActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TestRequestGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestRequestActionGoal(ref b);
        
        TestRequestActionGoal IDeserializable<TestRequestActionGoal>.RosDeserialize(ref Buffer b) => new TestRequestActionGoal(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TestRequestActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1889556d3fef88f821c7cb004e4251f3";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUTW/bMAy9+1cQ6GHtgLZbu10K5NA1Xpth/UCSAbsZjMTYwmzJk+Sm+fd7spO0W3bY" +
                "YQ0CGBbJxye+R98Ia/FU9Y+MVTTO1mZRNKEMp9eO68mYSjwKo7O5hDiVnx0eKdKfZ6P//MtuZ9cXFKIe" +
                "KNwMxA5oFtlq9poaiaw5Mi0deJuyEn9cy6PUKOKmFU19NK5bCSconFcmEP6lWPFc12vqApKiI+WaprNG" +
                "cRSKppHf6lFpLDG17KNRXc0e+c5rY1P60nMjCR3/kEZildBkfIEcG0R10YDQGgjKCwdjSwQp64yN52ep" +
                "IDuYr9wxXqXE9HfNKVYcE1l5ar2ExJPDBXq8HS53AmwMR9BFBzrszwq8hiNCE1CQ1qmKDsH8YR0rZwEo" +
                "9Mje8KKWBKwwAaC+SUVvjl4gJ9oXZNm6LfyA+NzjX2DtDjfd6biCZnW6fehKDBCJrXePRiN1se5BVG3E" +
                "RoLlPPt1lqqGltnB5zRjJKGqVwRPDsEpAwE0rUysshB9Qu/VSA59JTf+dS16a23IUqhcV2u8OJ8oD34i" +
                "aLmqDATpL5HWhVYcyA87JDoZaNLr3VsSI2G7aQaR/SOssarEkomEi0pIpoUvpGkjYeCoTphhcM1K0HoH" +
                "TQvBfoACKfGRoVxi9HK+G/5GbzXBeEEPsrjnOdNSRC9Y/QAzjQqYsqsjdjAELqUXgUIryiyNGi64YRBO" +
                "NuhpQYYEkGq6EMGMsHXIOtnql5R7belO//h6ZcMuzvPp7eTucp4Xs29XV/lsNnq3F7n8dD+d5+PR+73I" +
                "NP+SX6XQ2V7o6/0sH53vHY+n9w+jD3vH+fer/GE+ub8bfdzEovim/9YUkCt2IVs4V5MpLVQtFGM16+34" +
                "BlGKKE9xW1xJMZxuykIRTNPWqOz3LdOd595mWmpeF6yUtHunOwrPgZbh1C2jXziwYjM8BgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
