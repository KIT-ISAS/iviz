/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestRequestActionFeedback : IDeserializable<TestRequestActionFeedback>, IActionFeedback<TestRequestFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TestRequestFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public TestRequestActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = TestRequestFeedback.Singleton;
        }
        
        /// Explicit constructor.
        public TestRequestActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestRequestFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal TestRequestActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TestRequestFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestRequestActionFeedback(ref b);
        
        TestRequestActionFeedback IDeserializable<TestRequestActionFeedback>.RosDeserialize(ref Buffer b) => new TestRequestActionFeedback(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TestRequestActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC981dgxofYndhpkzZNPaODKimOOk7isdVePSCxItGCoAqAkvXv+xb8EFVL" +
                "Ex2SaGxTkoG3D2/fLvYDSUVOFPGRyCzoyhqdPpY+969uKmkeggy1Fz4+kgX5cE//1ni8J1KpzP4Ry/ZN" +
                "MvrKr+Tjw801IquGzYeG45kAJaukU6KkIJUMUiwrHEHnBblLQ2syTLdckRLxv2G7In+FjYtCe4GfnCw5" +
                "acxW1B6LQiWyqixrqzMZSARd0t5+7NRWSLGSLuisNtJhfeWUtrx86WRJjI4fz9LYjMR8eo011lNWBw1C" +
                "WyBkjqTXNsc/RVJrG9685g3J2WJTXeIj5UhEH1yEQgYmS08rR555Sn+NGD80h7sCNsQhRFFenMfvHvHR" +
                "XwgEAQVaVVkhzsH8bhuKygKQxFo6LVNDDJxBAaC+4E0vLgbITPtaWGmrDr5B3MU4Bdb2uHymywI5M3x6" +
                "X+cQEAtXrlprhaXpNoJkRpMNAu5z0m0T3tWETM7es8ZYhF0xI3hK76tMIwFKbHQoEh8co8dsPGqVfCM3" +
                "Hq2QhN8iszkeHJ8T/K4rm+bD3ezTdP7pRnSvkfgRf9mWFLeJQnqxpcCGTIn1yZrEtwI1sZFzt0YdNJjj" +
                "yWL+10wMMH/ax+SM1M5BWZgwJdboJOC7+9ns491iNu2BX+8DO8oI1oYtkXLYg7+JjUHIZYCTdeDTO04Q" +
                "PcU6sHmyI/r8dYZfmCSq0BgOVbkyxAg6+A4FRM8X5EpUn+FWEOiipfzw52Qym00HlN/sU94AWWaFRotQ" +
                "8GHGKixr7gOHhDgWZvz75/udLhzm5wNh0ioeXdXRljvuByOpmr4oDbvCVyiDpdSmdnSM3v3sj9lkwG8k" +
                "fnlOz9HflDG/g3S4oKo6/N8uL7/MMaVMoqdGzD5YjT4ZJJhyh0Cn1nYtjVbHDtA6r6+UkXj7HZzXW89W" +
                "IRbhznx98nqFJ+Pb210lj8SvpxJMCVcVHWR4irrIyfNs7ZO2S+1KvtT4+ujTEPsyMyG1d4ihTd59hUOc" +
                "JjObYq/8mgB8bRzxxO3nh8UQaiR+i4Bj24nR3h5AEgpZYxBqRJC9BIxy1UwBHgY3KuqWnlB7nrErVpsl" +
                "3WgcH5WDWPutMzkbG1Nt4jzCC1EKeFPtLiuQaS8qrjExGLB4i6K0znOWsV0U6Ckk3/Eqm0/jlNTeu51I" +
                "PnC6+TzxToakm0Jjtoj38aClRHeQ4lloHkeXOF0d0An7ybJ/cEryLBBGHCpXyJUx2M2YvknehhC6h+6s" +
                "B0uS45YSGQ1HhaThj+7SjhdoxaCHLjfMQjeyshuxA/NVbQLGSe9lzulFavyKMr3UWVcMkYFn9zA6NrUL" +
                "QKqsY1Ggz2msuuqSx0PIt07dqwMDeZL8B69nW1jVCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
