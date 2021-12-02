/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GoalStatus : IDeserializable<GoalStatus>, IMessage
    {
        [DataMember (Name = "goal_id")] public GoalID GoalId;
        [DataMember (Name = "status")] public byte Status;
        public const byte PENDING = 0; // The goal has yet to be processed by the action server
        public const byte ACTIVE = 1; // The goal is currently being processed by the action server
        public const byte PREEMPTED = 2; // The goal received a cancel request after it started executing
        //   and has since completed its execution (Terminal State)
        public const byte SUCCEEDED = 3; // The goal was achieved successfully by the action server (Terminal State)
        public const byte ABORTED = 4; // The goal was aborted during execution by the action server due
        //    to some failure (Terminal State)
        public const byte REJECTED = 5; // The goal was rejected by the action server without being processed,
        //    because the goal was unattainable or invalid (Terminal State)
        public const byte PREEMPTING = 6; // The goal received a cancel request after it started executing
        //    and has not yet completed execution
        public const byte RECALLING = 7; // The goal received a cancel request before it started executing,
        //    but the action server has not yet confirmed that the goal is canceled
        public const byte RECALLED = 8; // The goal received a cancel request before it started executing
        //    and was successfully cancelled (Terminal State)
        public const byte LOST = 9; // An action client can determine that a goal is LOST. This should not be
        //    sent over the wire by an action server
        //Allow for the user to associate a string with GoalStatus for debugging
        [DataMember (Name = "text")] public string Text;
    
        /// Constructor for empty message.
        public GoalStatus()
        {
            GoalId = new GoalID();
            Text = string.Empty;
        }
        
        /// Explicit constructor.
        public GoalStatus(GoalID GoalId, byte Status, string Text)
        {
            this.GoalId = GoalId;
            this.Status = Status;
            this.Text = Text;
        }
        
        /// Constructor with buffer.
        internal GoalStatus(ref Buffer b)
        {
            GoalId = new GoalID(ref b);
            Status = b.Deserialize<byte>();
            Text = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GoalStatus(ref b);
        
        GoalStatus IDeserializable<GoalStatus>.RosDeserialize(ref Buffer b) => new GoalStatus(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            GoalId.RosSerialize(ref b);
            b.Serialize(Status);
            b.Serialize(Text);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Text is null) throw new System.NullReferenceException(nameof(Text));
        }
    
        public int RosMessageLength => 5 + GoalId.RosMessageLength + BuiltIns.GetStringSize(Text);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_msgs/GoalStatus";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d388f9b87b3c471f784434d671988d4a";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1V227aQBB991eslJdWqtJ7m1bigYKFqHJToH2N1usBtl176V4g+fucWYyBBBQeoloC" +
                "jDV75syZM+OBlWbYF1P83Ooyi7oOZ8IHGaJv/lznl/3h5UCsr454h+8TMZ5ROiZm0ot7CiJYUZCYO6vI" +
                "eypFcS8CYqQK2tbCk1uQazC7vfHwdy62MN/vYmovVHSO6mDugarr6XHA1zd5fnE9zvst8IddYEeK9AIg" +
                "UihZK+In/yL5IOQkkBM6cPUuIILuSMWA1NmG6NPrBB9Zl0kFr4EolK3mhhhBB79GAdFXY3KVrkFiBH3p" +
                "dUN59KvXy/P+FuWPu5SXQJZqpolp+6hYhUk0LMweIQ6l6f64utnowmk+7UlT2FR6GR1LvuG+N1MZ6Vlp" +
                "2BXeViQmUpvo6BC9m/xn3tvi1xGfn9Jz9IcU89tLZ6nDzMbw2C5vnudYkJLRU8Jsk8VahiDBtDAkLJxR" +
                "L6TR5aECGue1k9IRX/6D81rr1TakIdyYr21eq3Cve36+meSO+HoswYImFq3bx/AYddGTp93aJV1PtKuA" +
                "G2ZyFdxugcSEyp0itm1y9gJFHCczm2Jn/FYJQO6QJ86vRuNtqI74lgC79VoMZTR2HCOJEl1jEFqJIFsJ" +
                "GOUUFeLWw+CmTLoVR8yeZ2zLarOkS43yMTnItbs6s5OuMXYpoE8KxCjgxgrpvVUa9YCMD2kj8IyJAYhx" +
                "ndGnIyUVcTplGZugQHchy7LOC1/ZxWjwvaFudHFb+al/y1yG/WzlADS2mq9F8oHbzfUEjf0DSZczrWZ4" +
                "AiG3VkpyB5WnwBgG1js275jHOuE81ewfVEmeBZo7omqOXhmD04yJx9y8JSF1C722HixJjldKYpQlWoky" +
                "OpD4Y7tgbS10CXgJethy212YEJWFVH/ZjTjhyEcTRAU/yim3F63xc1J6otV6GBIDz+5hdBxqAkCqimko" +
                "sOc0ok7XzUNUlj0ASTB0MhYIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
