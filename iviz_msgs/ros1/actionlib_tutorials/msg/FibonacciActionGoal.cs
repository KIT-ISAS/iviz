/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciActionGoal : IDeserializable<FibonacciActionGoal>, IMessageRos1, IActionGoal<FibonacciGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public FibonacciGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public FibonacciActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new FibonacciGoal();
        }
        
        /// Explicit constructor.
        public FibonacciActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, FibonacciGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        public FibonacciActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new FibonacciGoal(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new FibonacciActionGoal(ref b);
        
        public FibonacciActionGoal RosDeserialize(ref ReadBuffer b) => new FibonacciActionGoal(ref b);
    
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
    
        public int RosMessageLength => 4 + Header.RosMessageLength + GoalId.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib_tutorials/FibonacciActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "006871c7fa1d0e3d5fe2226bf17b2a94";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUTWvbQBC961cM+JCkEAfam6G3kI9DoZDczXh3LA2VdtWdlV39+75d2W4CPfTQCIFY" +
                "aebNm3lv9CTsJVFXHw27rDH0utsO1trdY+T++Z5aPLbqmwfdxcDOaXlf3zZf//PVfHt53JBlvxB4Wmit" +
                "6CVz8Jw8DZLZc2baR7DWtpN028tBeiTxMIqn+jXPo9gaia+dGuFuJUjivp9pMgTlSC4OwxTUcRbKOsi7" +
                "fGRqIKaRU1Y39ZwQH5PXUML3iQcp6LhNfk4SnNDz/QYxwcRNWUFoBoJLwqahxUdqJg35y+eS0Kxej/EW" +
                "R2kx+0txyh3nQlZ+jUms8GTboManpbk1sDEcQRVvdF3fbXG0G0IRUJAxuo6uwfz7nLsYACh04KS866UA" +
                "O0wAqFcl6ermDXKo0IFDPMMviH9q/AtsuOCWnm47aNaX7m1qMUAEjike1CN0N1cQ16uETDBc4jQ3JWsp" +
                "2aweyowRhKyqCJ5sFp1CAE9HzV1jORX0qkbx5we58a9LUa11IkvWxan3OMQkta/aCLQ8dgpBahNlXejI" +
                "RqkYxtBEMdBz1btaEiPhcCoGkdMB1jh2EkgzoVGxYlr4QoYxEwaO7IJpi2uOgtIXaNrJvnBhcpIyQ7nC" +
                "6O18T/zVnzXBeEFvLkUuc6a9iN+x+wFmHhkw5dRn7KAZt1JFIBvF6V7d0uCJga1P6GVBlgCQGibLYEbY" +
                "OkStz/oV5T5cujxBHMW47t79xZpmWUqsNv4zvwGgtZZWDQUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
