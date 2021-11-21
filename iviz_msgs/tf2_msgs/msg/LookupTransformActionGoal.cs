/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = "tf2_msgs/LookupTransformActionGoal")]
    public sealed class LookupTransformActionGoal : IDeserializable<LookupTransformActionGoal>, IActionGoal<LookupTransformGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public LookupTransformGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public LookupTransformActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new LookupTransformGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LookupTransformActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, LookupTransformGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal LookupTransformActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new LookupTransformGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionGoal(ref b);
        }
        
        LookupTransformActionGoal IDeserializable<LookupTransformActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionGoal(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f2e7bcdb75c847978d0351a13e699da5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwWobMRC9C/wPAzkkKcSF9GboLTQJtFCI72YsjXdFdqWtRrLjv++T1k5SyKGH1iwI" +
                "STNv3sx78oOwk0R9Wwzb7GMY/HYzaqef7yMPj3fUYdl4Z77H+FymdeKgu5jGetvuFubrP/4tzI+n+xVp" +
                "djORh0ZvYS7oKXNwnByNktlxZgIV6n3XS7oZZC8DsnicxFG7zcdJdInEde+V8HUSJPEwHKkognIkG8ex" +
                "BG85C2U/yh/5yPSBmCZO2dsycEJ8TM6HGr5LPEpFx6fyq0iwQo93K8QEFVuyB6EjEGwSVh86XJIpPuQv" +
                "tzXBXKwP8QZb6SDCa3HKPedKVl6mJFp5sq5Q49Pc3BLYmI6gilO6amcbbPWaUAQUZIq2pysw/3nMfQwA" +
                "FNpz8rwdpAJbTAColzXp8vodcqW9osAhnuFnxLcafwMbXnFrTzc9NBtq91o6DBCBU4p77xC6PTYQO3gJ" +
                "meC8xOloatZc0lx8qzNGELKaIlhZNVoPARwdfO6N5lTRmxrVqP/NkB++j2rLNXqYpdM+lsFhE1NlPVuK" +
                "IOeh99Ck9VEfDR1YKVXPKPqoHnpskjdXYiocTtWgc9rDHYdeAvlM6FW0+hbWkHHKhJkju2LqbJyDoPQr" +
                "NG0FTwQUyErKDPEqo/cjPvH37iwLJgx6UCa+jZp2Im7L9hnMHDLgyzJkPENV7qTpQDqJ9Ttv5wZPDHR5" +
                "Qq9vZA4AqbFoBjPCw0PU8iwhov6fenl3O+v2wR/ZwpwpZE6d5E0z0/lMY0lWTmfz5OaTNkpXEjel6i6W" +
                "bMwcc0JqMWeL+hdxJxxjtjEOxG7PeGHVtb8B8roBXI0FAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
