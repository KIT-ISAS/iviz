/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
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
        public TestRequestActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TestRequestGoal(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestRequestActionGoal(ref b);
        
        public TestRequestActionGoal RosDeserialize(ref ReadBuffer b) => new TestRequestActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib/TestRequestActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1889556d3fef88f821c7cb004e4251f3";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUwW7bMAy9+ysI9LB2QNOt3S4FcugSr82wNkXiAbsZjMTYwmTJk+Sk/vtRcpJ2yw47" +
                "rIYBwSL5+MhH+o5QkoM6HRmKoKzRalU2vvIXtxb1bAoVH6WSWUE+LOhnx0e0pPts/J+f7H55ew0+yIHC" +
                "3UDsBJYBjUQnoaGAEgPC2jJvVdXkzjVtSHMQNi1JSNbQt+RHHFjUygO/FRlyqHUPnWenYEHYpumMEhgI" +
                "gmrot3iOVAYQWnRBiU6jY3/rpDLRfe2woYjOr48tMYJgNr1mH+NJdEExoZ4RhCP0ylRshKxTJlxdxoDs" +
                "pNjac/6kirt/SA6hxhDJ0lPryEee6K85x9uhuBFjc3OIs0gPp+mu5E9/BpyEKVBrRQ2nzPyxD7U1DEiw" +
                "QadwpSkCC+4Ao76JQW/OXiCbBG3Q2D38gPic419gzQE31nRes2Y6Vu+7ihvIjq2zGyXZddUnEKEVmQA8" +
                "cg5dn8WoIWV28jn2mJ04KinCJ3pvhWIBJGxVqDMfXERPasQJfaVp/OtapNHakQVf205L/rCOUl2pENZy" +
                "WysWJBUR1wW26MENO0QyDtAs6Z1GkluCZpeMRXYbHo1tTQZUAC6UfBxangtq2gDccI6OmH6Ymi1x6gM0" +
                "rGgduSAIcgFZucjoZX93/JXca8LtZXp9THLoM6yJ5ArFD2YmOYKHstOBd9B7rCiJAL4lodZKDAXuGPjR" +
                "Dj0uyODApJrOB2YGvHXsNdrrF5V7beku/vh7ZcMuFvnifvZwU+Tl8ttkki+X43dHlptP80WRT8fvjyyL" +
                "/Es+iabLI9PX+TIfXx1dTxfzx/GHo+v8+yR/LGbzh/HHnS2Qa9K/pmS5QuezlbUaVGVY1VIgr6bet28Q" +
                "pQz0FPbBNZXD7S7Ml141rebItG+Z7BymMZOksS9RCGqPbg8Ung0t8qTuGf0COLBiMzwGAAA=";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
