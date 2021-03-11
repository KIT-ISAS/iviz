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
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new FibonacciGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, FibonacciGoal Goal)
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
                "H4sIAAAAAAAACr1UTWvcQAy9G/Y/CPaQpJANtLeF3kI+DoVCcg/yjNYWtWfc0Xjd/fd9Y+9uEuihh6bG" +
                "YMaWnp70nvwg7CVROz8qdllj6LR+6a2xm/vI3eMtNXi8qK/utI6BndPyfn67qr7+42tVfXu635Jlv1B4" +
                "mImtqjU9ZQ6ek6deMnvOTLsI4tq0kq472UuHLO4H8TR/zYdBbIPE51aNcDcSJHHXHWg0BOVILvb9GNRx" +
                "Fsray7t8ZGogpoFTVjd2nBAfk9dQwneJeynouE1+jhKc0OPtFjHBxI1ZQegABJeETUODj1SNGvKXzyWh" +
                "Wj9P8RpHaTD+c3HKLedCVn4NSazwZNuixqeluQ2wMR1BFW90Ob97wdGuCEVAQYboWroE8++H3MYAQKE9" +
                "J+W6kwLsMAGgXpSki6s3yIX2lgKHeIJfEF9r/A1sOOOWnq5baNaV7m1sMEAEDinu1SO0PswgrlMJmeC5" +
                "xOlQlaylZLW+KzNGELJmRfBks+gUAniaNLeV5VTQZzWKRT/MkH/cjGLLZ/SwSGdtHDuPQ0yF9WIpgpxT" +
                "q9Bk7qMsDU1slIpnDH0UDz3Oks+uxFQ4HKtB57SHO6ZWAmkm9CpWfAtrSD9kwsyRXTBtMc4kKH2Gplqw" +
                "IqBATlJmiFcYvR3xkb/6kyyYME0MZeLrqGkn4mt2P8DMIwO+HLuMNTTjRmYdyAZxulO3NHhkYJsjetmR" +
                "JQCk+tEymBEWD1Gbk4SI+h/q5RHyKAZ28+5vtqqqZTWx4OV38xvitqXgFwUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
