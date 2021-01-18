/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/FibonacciActionGoal")]
    public sealed class FibonacciActionGoal : IDeserializable<FibonacciActionGoal>, IActionGoal<FibonacciGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public FibonacciGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public FibonacciActionGoal()
        {
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new FibonacciGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, FibonacciGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FibonacciActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new FibonacciGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionGoal(ref b);
        }
        
        FibonacciActionGoal IDeserializable<FibonacciActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionGoal(ref b);
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
                int size = 4;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "006871c7fa1d0e3d5fe2226bf17b2a94";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
    }
}
